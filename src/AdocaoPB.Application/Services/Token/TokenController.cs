using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdocaoPB.Communication.Requests;
using AdocaoPB.Communication.Responses;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using AdocaoPB.Exceptions;
using AdocaoPB.Exceptions.ExceptionsBase;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace AdocaoPB.Application.Services.Token;

public class TokenController {

    private readonly string _chaveDeSeguranca;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenWriteOnlyRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenController(
        string chaveDeSeguranca,
        IRefreshTokenWriteOnlyRepository refreshTokenWriteOnlyRepository,
        TokenValidationParameters tokenValidationParameters,
        IUnitOfWork unitOfWork,
        UserManager<Domain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor

    )
    {
        _chaveDeSeguranca = chaveDeSeguranca;
        _refreshTokenWriteOnlyRepository = refreshTokenWriteOnlyRepository;
        _tokenValidationParameters = tokenValidationParameters;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<JsonRegisteredUserResponse> GenerateJwtToken(Domain.Entities.User user) {

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var claims = await GetAllValidClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {

            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Convert.FromBase64String(_chaveDeSeguranca)),
                SecurityAlgorithms.HmacSha256
            )
        };       

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        var userRefreshtToken = await _refreshTokenWriteOnlyRepository
            .RefreshTokenUserExists(user.Id);

        if (userRefreshtToken is null) {

            var refreshToken = new RequestAddRefreshToken() {
                //JwtId = token.Id,
                Token = RandomStringGeneration(60),
                CreationDate = NormalizeDatetimeToSouthAmerica(),
                ExpiryDate = NormalizeDatetimeToSouthAmerica().AddSeconds(300),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id
            };

            var refreshTokenMapper = _mapper.Map<RefreshToken>(refreshToken);

            await _refreshTokenWriteOnlyRepository.Add(refreshTokenMapper);
            await _unitOfWork.Commit();

            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("X-Refresh-Token", refreshToken.Token,
                    new CookieOptions {
                        Expires = NormalizeDatetimeToSouthAmerica().AddSeconds(300),
                        HttpOnly = true,
                        Secure = false,
                        IsEssential = true,
                        SameSite = SameSiteMode.Lax,
                        Domain = "localhost",
                        Path = "/",
                    }
                );

            return new JsonRegisteredUserResponse {
                Token = jwtToken,
                RefreshToken = "",
            };

        } else {
            var newRefreshToken = RandomStringGeneration(60);
            userRefreshtToken.Token = newRefreshToken;

            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("X-Refresh-Token", newRefreshToken);

            _refreshTokenWriteOnlyRepository.Update(userRefreshtToken);
            await _unitOfWork.Commit();

            return new JsonRegisteredUserResponse {
                Token = jwtToken,
                RefreshToken = "",
            };
        }     

    }


    private async Task<List<Claim>> GetAllValidClaims(Domain.Entities.User user) {

        var claims = new List<Claim> {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString()),
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var userRole in userRoles)
        {

            var role = await _roleManager.FindByNameAsync(userRole);

            if (role != null)
            {

                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }

            }
        }

        return claims;

    }


    private static string RandomStringGeneration(int length)
    {

        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
        return new string(
            Enumerable.Repeat(chars, length)
            .Select(
                s => s[random.Next(s.Length)]
            ).ToArray()
        );

    }


    public static DateTime NormalizeDatetimeToSouthAmerica() {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
    }


    
    public async Task<JsonRegisteredUserResponse> RefreshToken() {
        var result = await VerifyAndGenerateToken();

        if (result is null) {
            throw new ForbidenException(ResourceErrorMessages.TOKEN_INVALIDO); 
        }

        return result;
    }

    private async Task<JsonRegisteredUserResponse> VerifyAndGenerateToken() {

        //var jwtTokenHandler = new JwtSecurityTokenHandler();

        //var tokenValidationParameters = new TokenValidationParameters() {
        //    RequireExpirationTime = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(
        //        Convert.FromBase64String(_chaveDeSeguranca)
        //    ),
        //    ClockSkew = new TimeSpan(0),
        //    ValidateIssuer = false,
        //    ValidateAudience = false,
        //};

        //var tokenInVerification = jwtTokenHandler.ValidateToken(
        //    tokenRequest.Token, tokenValidationParameters, out var validatedToken
        //);

        //if (validatedToken is JwtSecurityToken jwtSecurityToken) {
        //    var result = jwtSecurityToken.Header.Alg.Equals(
        //        SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase
        //    );
        //    // verifica se foi o sistema mesmo que gerou com base no alg

        //    if (result == false)
        //    {
        //        return null;
        //    }
        //}

        //var utcExpiryDate = long.Parse(tokenInVerification.Claims
        //    .FirstOrDefault(
        //        x => x.Type == JwtRegisteredClaimNames.Exp
        //    ).Value
        //);
        //var dataConvertida = DateTimeOffset.FromUnixTimeSeconds(utcExpiryDate);

        //var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
        //Console.WriteLine(expiryDate);

        //if (expiryDate > DateTime.Now) {
        //    throw new ForbidenException(ResourceErrorMessages.TOKEN_EXPIRADO);
        //}
        var teste = _httpContextAccessor.
            HttpContext.Request;

        var refreshTokenCookie = _httpContextAccessor.
            HttpContext.Request.Cookies["X-Refresh-Token"];

        //if (refreshTokenCookie is null) {
        //    throw new NotFoundException(ResourceErrorMessages.REFRESH_TOKEN_NOT_FOUND);
        //}

        var storedToken = await _refreshTokenWriteOnlyRepository.RefreshTokenGetById(refreshTokenCookie);

        if (storedToken is null) {
            throw new NotFoundException(ResourceErrorMessages.REFRESH_TOKEN_NOT_FOUND);
        }

        //if (storedToken.IsUsed) {
        //    throw new ForbidenException(ResourceErrorMessages.REFRESH_TOKEN_USED);
        //}

        //if(!storedToken.Token.Equals(refreshTokenCookie)) {
        //    throw new ForbidenException(ResourceErrorMessages.REFRESH_TOKEN_REVOKED);
        //}

        if (storedToken.IsRevoked) {
            throw new ForbidenException(ResourceErrorMessages.REFRESH_TOKEN_REVOKED);
        }

        //var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //if (storedToken.JwtId != jti) {
        //    throw new ForbidenException(ResourceErrorMessages.TOKEN_INVALIDO);
        //}


        if (storedToken.ExpiryDate < NormalizeDatetimeToSouthAmerica()) {
            await _refreshTokenWriteOnlyRepository
                .Delete(storedToken.Id);

            //_httpContextAccessor.HttpContext.Response.Cookies
            //    .Delete("X-Refresh-Token"); // Remove o cookie chamado "X-Refresh-Token"

            await _unitOfWork.Commit();
            throw new ForbidenException(ResourceErrorMessages.TOKEN_EXPIRADO);
        }


        //storedToken.IsUsed = true;
        //_refreshTokenWriteOnlyRepository.Update(storedToken);
        //await _unitOfWork.Commit();

        var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

        return await GenerateJwtToken(dbUser);     

    }

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp) {

        var dateTimeVal = new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }

}
