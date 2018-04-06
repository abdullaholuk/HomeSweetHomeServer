﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using HomeSweetHomeServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HomeSweetHomeServer.Services
{
    //Handles JWT token operations
    public class JwtTokenService : IJwtTokenService
    {
        private IConfiguration _config;
        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }
        
        public string CreateToken(UserModel user)
        {

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //Issuer
            string issuer = _config.GetValue<string>("JWT:Issuer");

            //Audience
            string audience = _config.GetValue<string>("JWT:Audience");

            //Claims
            ClaimsIdentity identities = new ClaimsIdentity();
            Claim claim_1 = new Claim("userId", user.UserId.ToString());
            Claim claim_2 = new Claim("username", user.Username);
            identities.AddClaim(claim_1);
            identities.AddClaim(claim_2);

            //LifeTime
            DateTime now = DateTime.Now;
            DateTime death = now.AddSeconds(50);

            //SigningCredientals
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credientials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //Create Token
            var tokenObj = handler.CreateJwtSecurityToken(
                issuer: issuer,
                audience: audience,
                signingCredentials: credientials,
                subject: identities,
                expires: death
            );
            
            return handler.WriteToken(tokenObj);
        }
        
        public bool VerifyToken(string token)
        {
            //Parts of Token
            var partsOfToken = token.Split('.');
            string header = partsOfToken[0];
            string payload = partsOfToken[1];
            string signedSignature = partsOfToken[2];
            byte[] byteSign = Base64UrlEncoder.DecodeBytes(signedSignature);
            byte[] byteHeaderAndPayload = Encoding.UTF8.GetBytes(header + '.' + payload);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            SymmetricSignatureProvider provider = new SymmetricSignatureProvider(key, SecurityAlgorithms.HmacSha512);

            return provider.Verify(byteHeaderAndPayload, byteSign);
        }
    }
}
