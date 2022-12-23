using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SehirRehberiWebApi.Data;
using SehirRehberiWebApi.Dtos;
using SehirRehberiWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SehirRehberiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            //register olmaya çalısan kullanıcı sistemde var mı ?
            if (await _authRepository.UserExist(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already exist");

            }
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                Username = userForRegisterDto.UserName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201,createdUser);
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            //Kullanıcı adı ve şifre yollanacak biz de veritabanında var mı diye kontrol edeceğiz.
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (user==null)
            {
                return Unauthorized();
            }
            //eğer kullanıcı varsa o kullanıcıya token göndericem ki işlemlerini o token ile yapsın.
            //öncelikle TokenHandler yazacagım
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").Value);
            //App settings deki token ın value unu ver dedim.
            //Peki token neler tutacak ?
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                //Bu token ne kadar geçerli ? 1 gün
                Expires = DateTime.Now.AddDays(1),
                //Token ı üretmek için hangi algoritmayı da kullandıgımı belirteyim:
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };
            //Artık token ı üretebilirim.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);

        }
    }
}
