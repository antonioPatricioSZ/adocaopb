using System.Reflection;
using AdocaoPB.Domain.Repositories;
using AdocaoPB.Domain.Repositories.RepositoryEmail.SendEmail;
using AdocaoPB.Domain.Repositories.RepositoryEnderecoUser;
using AdocaoPB.Domain.Repositories.RepositoryPet;
using AdocaoPB.Domain.Repositories.RepositoryRefreshToken;
using AdocaoPB.Domain.Repositories.RepositoryUser;
using AdocaoPB.Infrastructure.RepositoryAccess;
using AdocaoPB.Infrastructure.RepositoryAccess.Repositories;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AdocaoPB.Infrastructure;

public static class Initializer {


    public static void AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
    ){
        AddFluentMigrator(services, configuration);

        AddContext(services, configuration);

        AddUnitOfWork(services);

        AddRepositories(services);
    }


    private static void AddContext(
        IServiceCollection services,
        IConfiguration configuration
    ){
        var connectionString = configuration.GetConnectionString("Conexao");

        services.AddDbContext<AdocaoPBContext>(
            dbContextOptions => {
                dbContextOptions.UseSqlServer(connectionString, action => {
                    //action.CommandTimeout(30); // tempo máxima de uma solicitação ao banco de dados
                    action.MigrationsAssembly("AdocaoPB.Infrastructure");
                });
                dbContextOptions.EnableDetailedErrors();
                dbContextOptions.EnableSensitiveDataLogging();
                // essas duas devem ser usadas apenas em ambiente de desenvolvimento
            }
        );
    }


    private static void AddRepositories(IServiceCollection services) {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
            .AddScoped<IUserReadOnlyRepository, UserRepository>()
            .AddScoped<IRefreshTokenWriteOnlyRepository, RefreshTokenRepository>()
            .AddScoped<IPetReadOnlyRepository, PetRepository>()
            .AddScoped<IPetWriteOnlyRepository, PetRepository>()
            .AddScoped<IPetUpdateOnlyRepository, PetRepository>()
            .AddScoped<ISendEmail, SendEmailRepository>()
            .AddScoped<IEnderecoUserReadOnlyRepository, EnderecoUserRepository>()
            .AddScoped<IEnderecoUserWriteOnlyRepository, EnderecoUserRepository>()
            .AddScoped<IEnderecoUserUpdateOnlyRepository, EnderecoUserRepository>();
    }


    private static void AddUnitOfWork(IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }


    private static void AddFluentMigrator(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var conexao = configuration.GetConnectionString("Conexao");
        services.AddFluentMigratorCore()
            .ConfigureRunner(c =>
                c.AddSqlServer().WithGlobalConnectionString(conexao)
                .ScanIn(Assembly.Load("AdocaoPB.Infrastructure")).For.All()
            );

        // .ScanIn(Assembly.Load("AdocaoPB.Infrastructure"))
        // .For.All());
        // isso vai dizer onde que deve escanear as versoes
        // onde procurar, preciso informar onde que é
    }

}
