﻿using Microsoft.EntityFrameworkCore;
using SehirRehberiWebApi.Models;

namespace SehirRehberiWebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private SehirRehberiContext _context;
        public AuthRepository(SehirRehberiContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user =  _context.Users.FirstOrDefault(u => u.Username == userName);
            if (user==null)
            {
                return null;

            }
            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        } 

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                    
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            //out kullanmamın sebebi hashleyip saltladıktan sonra değerleri out et yani returnle
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await _context.Users.AnyAsync(x=>x.Username==userName))
            {
                return true;
            }
            return false;
        }
    }
}
