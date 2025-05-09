﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestWithASPNETErudio.Configurations;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Repository;
using RestWithASPNETErudio.Services;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private TokenConfiguration _configuration;

        private IUserRepository _repository;

        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken(claims);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), accessToken, refreshToken);
        }

        TokenVO ILoginBusiness.ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;

            var user = _repository.ValidateCredentials(userName);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateAccessToken(principal.Claims);

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), accessToken, refreshToken);
        }

        bool ILoginBusiness.RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
