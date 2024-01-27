//namespace AdocaoPB.Api.Filters;

//public class TokenExpirationMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TokenExpirationMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        // Verifica se o token está presente no cabeçalho Authorization
//        if (context.Request.Headers.ContainsKey("Authorization"))
//        {
//            var authHeader = context.Request.Headers["Authorization"].ToString();

//            // Remove "Bearer " do início do token JWT, se estiver presente
//            var token = authHeader.Replace("Bearer ", "");

//            // Validação manual da expiração do token
//            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
//            var jwtToken = tokenHandler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

//            if (jwtToken != null)
//            {
//                var exp = jwtToken.ValidTo;
//                if (exp < DateTime.UtcNow)
//                {
//                    context.Response.StatusCode = 401; // Token expirado
//                    return;
//                }
//            }
//        }

//        await _next(context);
//    }
//}
