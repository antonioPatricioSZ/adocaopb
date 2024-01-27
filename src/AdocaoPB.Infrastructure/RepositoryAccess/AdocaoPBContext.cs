using AdocaoPB.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdocaoPB.Infrastructure.RepositoryAccess;

public class AdocaoPBContext : IdentityDbContext<User> {

    public AdocaoPBContext(
        DbContextOptions<AdocaoPBContext> options
    ) : base(options) {}


    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<EnderecoUsers> EnderecoUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdocaoPBContext).Assembly);
        
        modelBuilder.Entity<User>().ToTable("AspNetUsers");

        base.OnModelCreating(modelBuilder);
    }
}
