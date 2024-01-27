using AdocaoPB.Application.Services.Token;
using AdocaoPB.Application.UseCases.Authorizations.AddRoleToUser;
using AdocaoPB.Application.UseCases.Authorizations.CreateRole;
using AdocaoPB.Application.UseCases.Endereco.Add;
using AdocaoPB.Application.UseCases.Pets.Add;
using AdocaoPB.Application.UseCases.Pets.AdoptionPet;
using AdocaoPB.Application.UseCases.Pets.ConcludeAdoption;
using AdocaoPB.Application.UseCases.Pets.DeletePet;
using AdocaoPB.Application.UseCases.Pets.GetAll;
using AdocaoPB.Application.UseCases.Pets.GetAllAdopter;
using AdocaoPB.Application.UseCases.Pets.GetAllUser;
using AdocaoPB.Application.UseCases.Pets.GetById;
using AdocaoPB.Application.UseCases.Pets.UpdatePet;
using AdocaoPB.Application.UseCases.User.GetById;
using AdocaoPB.Application.UseCases.User.Login;
using AdocaoPB.Application.UseCases.User.Register;
using AdocaoPB.Application.UseCases.User.SendEmail;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using AdocaoPB.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Application.UseCases.Authorizations.GetAllClaimsUser;
using AdocaoPB.Application.UseCases.Authorizations.GetAllRolesUser;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using AdocaoPB.Application.UseCases.User.GetAllUsers;

namespace AdocaoPB.Application;

public static class Initializer {

    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    ){
        AdicionarTokenJWT(services, configuration);
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services) {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<IAddPetUseCase, AddPetUseCase>()
            .AddScoped<IGetAllPetsUseCase, GetAllPetsUseCase>()
            .AddScoped<IGetAllPetsUserUseCase, GetAllPetsUserUseCase>()
            .AddScoped<IGetAllPetsAdopterUseCase, GetAllPetsAdopterUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>()
            .AddScoped<IAdoptionPetUseCase, AdoptionPetUseCase>()
            .AddScoped<IConcludeAdoptionPetUseCase, ConcludeAdoptionPetUseCase>()
            .AddScoped<IGetPetByIdUseCase, GetPetByIdUseCase>()
            .AddScoped<IAddEnderecoUseCase, AddEnderecoUseCase>()
            .AddScoped<IDeletePetUseCase, DeletePetUseCase>()
            .AddScoped<IUpdatePetUseCase, UpdatePetUseCase>()
            .AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>()
            .AddScoped<ISendEmailUseCase, SendEmailUseCase>()
            .AddScoped<ICreateRoleUseCase, CreateRoleUseCase>()
            .AddScoped<IAddRoleToUserUseCase, AddRoleToUserUseCase>()
            .AddScoped<IGetAllClaimsUserUseCase, GetAllClaimsUserUseCase>()
            .AddScoped<IGetAllRolesUserUseCase, GetAllRolesUserUseCase>()
            .AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
    }

    private static void AdicionarTokenJWT(
        IServiceCollection services,
        IConfiguration configuration
    ){

        services.AddScoped(provider => {

            
            
            var chaveDeSeguranca = configuration.GetSection("JwtConfig:Secret").Value;
            var refreshTokenWriteOnlyRepository = provider.GetService<IRefreshTokenWriteOnlyRepository>();
            var unitOfWork = provider.GetService<IUnitOfWork>();

            var tokenValidationParameters = new TokenValidationParameters(); // Instanciando seus TokenValidationParameters conforme necessário

            var userManager = provider.GetService<UserManager<User>>();
            var roleManager = provider.GetService<RoleManager<IdentityRole>>();

            var mapper = provider.GetService<IMapper>();
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>(); // Obtendo IHttpContextAccessor

            return new TokenController(chaveDeSeguranca, refreshTokenWriteOnlyRepository, tokenValidationParameters, unitOfWork, userManager, roleManager, mapper, httpContextAccessor);
        });

    }

}



// Em um contexto de desenvolvimento de aplicativos em C#,
// tanto IServiceCollection quanto IConfiguration são
// interfaces importantes usadas no ecossistema do
// ASP.NET Core para gerenciar a configuração e a injeção
// de dependência.

// IServiceCollection:

// É uma interface usada para registrar tipos de serviço
// durante a configuração da injeção de dependência em
// aplicativos ASP.NET Core.Ela fornece métodos como
// AddScoped, AddTransient, AddSingleton etc., que são
// usados para registrar implementações de serviços.
// Usar IServiceCollection permite que você registre
// tipos de serviços necessários para a funcionalidade
// do seu aplicativo. Depois que os serviços são registrados
// no contêiner de injeção de dependência, eles podem ser
// injetados em outras partes do aplicativo.


// IConfiguration:

// É uma interface que representa a hierarquia de
// configuração de aplicativos em ASP.NET Core.Ela
// é usada para acessar os valores de configuração definidos
// no aplicativo, como conexões de banco de dados, chaves
// de API, configurações de autenticação etc.
// IConfiguration é responsável por carregar e fornecer
// acesso aos valores definidos em diferentes fontes de
// configuração, como arquivos de configuração JSON, variáveis
// de ambiente, argumentos da linha de comando e muitos
// outros provedores de configuração.
// Quando você utiliza this IServiceCollection services
// em um método de extensão, por exemplo, dentro da
// classe Startup em um projeto ASP.NET Core, você está
// estendendo as funcionalidades do IServiceCollection,
// permitindo adicionar novos serviços ou configurações
// ao contêiner de injeção de dependência.

// Por exemplo, ao criar um método de extensão:

// public static class CustomServiceExtensions {
//    public static IServiceCollection AddCustomService(this IServiceCollection services) {
//        services.AddScoped<IMyCustomService, MyCustomService>();
//        // ... outros serviços adicionados

//        return services;
//    }
// }

// Quando você chama services.AddCustomService() no método
// ConfigureServices da classe Startup, está adicionando o
// serviço IMyCustomService como um serviço de escopo ao
// contêiner de injeção de dependência.Assim, este serviço
// estará disponível para ser injetado em outras partes
// do aplicativo.

// O uso de this IServiceCollection services em
// métodos de extensão permite encadear métodos de
// configuração do serviço (services) de forma mais
// legível e estruturada.