using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberiWebApi.Data;
using SehirRehberiWebApi.Dtos;
using SehirRehberiWebApi.Models;

namespace SehirRehberiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController(IAppRepository appRepository,IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getcities")]
        public IActionResult GetCities()
        {
            var cities=_appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult GetCityById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }
        [HttpGet("getphotos")]
        [Route("photos")]
        public IActionResult GetPhotosByCity(int cityId)
        {
            var photos = _appRepository.GetPhotosByCityId(cityId);
            return Ok(photos);
        }
    }
}
