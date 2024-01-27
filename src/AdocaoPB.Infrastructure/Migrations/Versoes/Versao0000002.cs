using System.Data;
using FluentMigrator;

namespace AdocaoPB.Infrastructure.Migrations.Versoes;

[Migration((long)NumeroVersoes.CreateTablePets, "Criar tabela pets")]

public class Versao0000002 : Migration
{
    public override void Down() { }

    public override void Up()
    {

        var tabela = VersionBase.InserirColunasPadrao(Create.Table("Pets"));

        tabela
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("OwnerId").AsString(450).NotNullable()
                .ForeignKey("FK_Pets_AspNetUsers_OwnerId", "AspNetUsers", "Id")
                .OnDeleteOrUpdate(Rule.Cascade)
            .WithColumn("AdopterId").AsString(450).Nullable()
                .ForeignKey("FK_Pets_AspNetUsers_AdopterId", "AspNetUsers", "Id")
            .WithColumn("Weight").AsDecimal(5, 2).Nullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Color").AsString().Nullable()
            .WithColumn("Breed").AsString().Nullable()
            .WithColumn("Observations").AsString()
            .WithColumn("IsAvailable").AsBoolean().NotNullable();

    }


}



////Create.Table("AspNetUserTokens")
////            .WithColumn("UserId").AsGuid().NotNullable()
////                .PrimaryKey("PK_AspNetUserTokens")
////                .Indexed("IX_AspNetUserTokens_UserId")
////                .ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUsers", "Id");

////Create.Column("LoginProvider").AsString().NotNullable()
////            .PrimaryKey("PK_AspNetUserTokens")
////            .Clustered()
////            .Indexed("IX_AspNetUserTokens_LoginProvider");

////Create.Column("Name").AsString().NotNullable()
////            .PrimaryKey("PK_AspNetUserTokens")
////            .Clustered();

////Create.Column("Value").AsString().Nullable();

////Create.UniqueConstraint("UC_AspNetUserTokens_UserId_LoginProvider_Name")
////            .OnTable("AspNetUserTokens")
////            .Columns("UserId", "LoginProvider", "Name")




////     Create.Table("AspNetRoleClaims")
////            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey("PK_AspNetRoleClaims").Identity()
////            .WithColumn("RoleId").AsGuid().NotNullable()
////                .Indexed("IX_AspNetRoleClaims_RoleId")
////                .ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoles", "Id");

////Create.Column("ClaimType").AsString().Nullable();
////Create.Column("ClaimValue").AsString().Nullable();








////Create.Table("AspNetRoles")
////               .WithColumn("Id").AsString().PrimaryKey("PK_AspNetRoles").NotNullable()
////               .WithColumn("ConcurrencyStamp").AsString().Nullable()
////               .WithColumn("Name").AsString(256).NotNullable()
////               .WithColumn("NormalizedName").AsString(256).Nullable()
////                                            .Indexed("RoleNameIndex");

////Create.Table("AspNetUsers")
////               .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetUsers")
////               .WithColumn("AccessFailedCount").AsInt32().NotNullable()
////               .WithColumn("Email").AsString(256).Nullable()
////               .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
////               .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
////               .WithColumn("LockoutEndDateUtc").AsDateTime().Nullable()
////               .WithColumn("PasswordHash").AsString().Nullable()
////               .WithColumn("PhoneNumber").AsString().Nullable()
////               .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
////               .WithColumn("SecurityStamp").AsString().Nullable()
////               .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
////               .WithColumn("UserName").AsString(256).Nullable();

////Create.Table("AspNetUserClaims")
////              .WithColumn("Id").AsInt32().PrimaryKey("PK_AspNetUserClaims").Identity()
////              .WithColumn("ClaimType").AsString().Nullable()
////              .WithColumn("ClaimValue").AsString().Nullable()
////              .WithColumn("UserId").AsString().NotNullable().Indexed("IX_AspNetUserClaims_UserId")
////                                   .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUsers", "Id")
////                                   .OnDelete(System.Data.Rule.Cascade);

////Create.Table("AspNetUserLogins")
////              .WithColumn("LoginProvider").AsString().NotNullable().PrimaryKey("PK_AspNetUserLogins")
////              .WithColumn("ProviderKey").AsString().NotNullable().PrimaryKey("PK_AspNetUserLogins")
////              .WithColumn("ProviderDisplayName").AsString().Nullable()
////              .WithColumn("UserId").AsString()
////                                   .NotNullable()
////                                   .Indexed("IX_AspNetUserLogins_UserId")
////                                   .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUsers", "Id")
////                                   .OnDelete(System.Data.Rule.Cascade);


////Create.Table("AspNetUserRoles")
////              .WithColumn("UserId").AsString()
////                                   .PrimaryKey("PK_AspNetUserRoles")
////                                   .Indexed("IX_AspNetUserRoles_UserId")
////                                   .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUsers", "Id")

////              .WithColumn("RoleId").AsString()
////                                   .PrimaryKey("PK_AspNetUserRoles")
////                                   .Indexed("IX_AspNetUserRoles_RoleId")
////                                   .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetRoles", "Id")
////                                   .OnDelete(System.Data.Rule.Cascade);
