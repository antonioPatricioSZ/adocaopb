using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AdocaoPB.Api.Filters;
using AdocaoPB.Application;
using AdocaoPB.Application.Services.AutoMapper;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Exceptions.ExceptionsBase;
using AdocaoPB.Infrastructure;
using AdocaoPB.Infrastructure.Migrations;
using AdocaoPB.Infrastructure.RepositoryAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRouting(config => config.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMvc(
    options => options.Filters.Add(
        typeof(ExceptionsFilter)
    )
);

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var tokenValidationParameters = new TokenValidationParameters() {
    RequireExpirationTime = true,
    IssuerSigningKey = new SymmetricSecurityKey(
        Convert.FromBase64String(builder.Configuration.GetSection("JwtConfig:Secret").Value)
    ),
    ClockSkew = new TimeSpan(0),
    ValidateIssuer = false,
    ValidateAudience = false,
};

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;

    //.AddCookie(cookie => {
    //     cookie.Cookie.Name = "X-Refresh-Token";
    // })

    //jwt.Events = new JwtBearerEvents {
    //    OnMessageReceived = context => {
    //        context.Token = context.Request.Cookies["Token"];
    //        return Task.CompletedTask;
    //    }
    //};
});

builder.Services.AddSingleton(tokenValidationParameters);
// final config token


builder.Services.AddIdentity<User, IdentityRole>(
    options => options.SignIn.RequireConfirmedEmail = false
).AddEntityFrameworkStores<AdocaoPBContext>()
.AddDefaultTokenProviders();

builder.Services.AddCors(options => {
    options.AddPolicy(
        name: "PermitirApiRequest",
        build => build.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<TokenExpirationMiddleware>();
app.UseCors("PermitirApiRequest");

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();

app.MigrateBancoDeDados();

app.Run();
