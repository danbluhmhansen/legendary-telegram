#nullable disable

namespace BlazorApp1.Server.Migrations;

using BlazorApp1.Server.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("00000000000001_IdentitySeed")]
partial class IdentitySeed
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.0")
			.HasAnnotation("Relational:MaxIdentifierLength", 63);

		NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

		modelBuilder.Entity("BlazorApp1.Server.Entities.ApplicationUser", b =>
			{
				b.Property<string>("Id")
					.HasColumnType("text");

				b.Property<int>("AccessFailedCount")
					.HasColumnType("integer");

				b.Property<string>("ConcurrencyStamp")
					.IsConcurrencyToken()
					.HasColumnType("text");

				b.Property<string>("Email")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.Property<bool>("EmailConfirmed")
					.HasColumnType("boolean");

				b.Property<bool>("LockoutEnabled")
					.HasColumnType("boolean");

				b.Property<DateTimeOffset?>("LockoutEnd")
					.HasColumnType("timestamp with time zone");

				b.Property<string>("NormalizedEmail")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.Property<string>("NormalizedUserName")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.Property<string>("PasswordHash")
					.HasColumnType("text");

				b.Property<string>("PhoneNumber")
					.HasColumnType("text");

				b.Property<bool>("PhoneNumberConfirmed")
					.HasColumnType("boolean");

				b.Property<string>("SecurityStamp")
					.HasColumnType("text");

				b.Property<bool>("TwoFactorEnabled")
					.HasColumnType("boolean");

				b.Property<string>("UserName")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.HasKey("Id");

				b.HasIndex("NormalizedEmail")
					.HasDatabaseName("EmailIndex");

				b.HasIndex("NormalizedUserName")
					.IsUnique()
					.HasDatabaseName("UserNameIndex");

				b.ToTable("AspNetUsers", (string)null);

				b.HasData(
					new
					{
						Id = "3a6b2a7f-6376-4865-b655-f9b9787d026f",
						AccessFailedCount = 0,
						ConcurrencyStamp = "cf764f15-6a4b-4cd4-a18c-b674476b7910",
						Email = "info@fake.com",
						EmailConfirmed = false,
						LockoutEnabled = false,
						NormalizedEmail = "INFO@FAKE.COM",
						NormalizedUserName = "INFO@FAKE.COM",
						PasswordHash = "AQAAAAEAACcQAAAAEDpy7j+IAaIm7M0CbBbA7RkHz9jquH0Thpx1hNnpqzDKlT+Dmk0f88JRu0wEFoj3SQ==",
						PhoneNumberConfirmed = false,
						SecurityStamp = "583a83a1-0560-4647-849a-92a17facb03d",
						TwoFactorEnabled = false,
						UserName = "info@fake.com"
					});
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
			{
				b.Property<string>("Id")
					.HasColumnType("text");

				b.Property<string>("ConcurrencyStamp")
					.IsConcurrencyToken()
					.HasColumnType("text");

				b.Property<string>("Name")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.Property<string>("NormalizedName")
					.HasMaxLength(256)
					.HasColumnType("character varying(256)");

				b.HasKey("Id");

				b.HasIndex("NormalizedName")
					.IsUnique()
					.HasDatabaseName("RoleNameIndex");

				b.ToTable("AspNetRoles", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("integer");

				NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

				b.Property<string>("ClaimType")
					.HasColumnType("text");

				b.Property<string>("ClaimValue")
					.HasColumnType("text");

				b.Property<string>("RoleId")
					.IsRequired()
					.HasColumnType("text");

				b.HasKey("Id");

				b.HasIndex("RoleId");

				b.ToTable("AspNetRoleClaims", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("integer");

				NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

				b.Property<string>("ClaimType")
					.HasColumnType("text");

				b.Property<string>("ClaimValue")
					.HasColumnType("text");

				b.Property<string>("UserId")
					.IsRequired()
					.HasColumnType("text");

				b.HasKey("Id");

				b.HasIndex("UserId");

				b.ToTable("AspNetUserClaims", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
			{
				b.Property<string>("LoginProvider")
					.HasColumnType("text");

				b.Property<string>("ProviderKey")
					.HasColumnType("text");

				b.Property<string>("ProviderDisplayName")
					.HasColumnType("text");

				b.Property<string>("UserId")
					.IsRequired()
					.HasColumnType("text");

				b.HasKey("LoginProvider", "ProviderKey");

				b.HasIndex("UserId");

				b.ToTable("AspNetUserLogins", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
			{
				b.Property<string>("UserId")
					.HasColumnType("text");

				b.Property<string>("RoleId")
					.HasColumnType("text");

				b.HasKey("UserId", "RoleId");

				b.HasIndex("RoleId");

				b.ToTable("AspNetUserRoles", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
			{
				b.Property<string>("UserId")
					.HasColumnType("text");

				b.Property<string>("LoginProvider")
					.HasColumnType("text");

				b.Property<string>("Name")
					.HasColumnType("text");

				b.Property<string>("Value")
					.HasColumnType("text");

				b.HasKey("UserId", "LoginProvider", "Name");

				b.ToTable("AspNetUserTokens", (string)null);
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", b =>
			{
				b.Property<string>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("text");

				b.Property<string>("ClientId")
					.HasMaxLength(100)
					.HasColumnType("character varying(100)");

				b.Property<string>("ClientSecret")
					.HasColumnType("text");

				b.Property<string>("ConcurrencyToken")
					.IsConcurrencyToken()
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<string>("ConsentType")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<string>("DisplayName")
					.HasColumnType("text");

				b.Property<string>("DisplayNames")
					.HasColumnType("text");

				b.Property<string>("Permissions")
					.HasColumnType("text");

				b.Property<string>("PostLogoutRedirectUris")
					.HasColumnType("text");

				b.Property<string>("Properties")
					.HasColumnType("text");

				b.Property<string>("RedirectUris")
					.HasColumnType("text");

				b.Property<string>("Requirements")
					.HasColumnType("text");

				b.Property<string>("Type")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.HasKey("Id");

				b.HasIndex("ClientId")
					.IsUnique();

				b.ToTable("OpenIddictApplications", (string)null);

				b.HasData(
					new
					{
						Id = "59d50e89-c579-4cc4-9cd1-a8db1399fe4e",
						ClientId = "blazor-client",
						ConcurrencyToken = "611ff896-aa59-4181-b6c5-a70a34026f0b",
						ConsentType = "explicit",
						DisplayName = "Blazor client application",
						Permissions = "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
						PostLogoutRedirectUris = "[\"https://localhost:7290/authentication/logout-callback\"]",
						RedirectUris = "[\"https://localhost:7290/authentication/login-callback\"]",
						Requirements = "[\"ft:pkce\"]",
						Type = "public"
					},
					new
					{
						Id = "5cf57099-7162-425c-b2cd-11d7c02610d5",
						ClientId = "swagger-client",
						ConcurrencyToken = "43dff1f3-ae23-46d7-8c6b-dfeb434cffee",
						ConsentType = "explicit",
						DisplayName = "Swagger client application",
						Permissions = "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
						RedirectUris = "[\"https://localhost:7289/swagger/oauth2-redirect.html\"]",
						Requirements = "[\"ft:pkce\"]",
						Type = "public"
					},
					new
					{
						Id = "d2557a9b-404d-42ab-89ab-7019929e96a4",
						ClientId = "postman-client",
						ConcurrencyToken = "23446b6e-6621-48ae-878c-ef6c371baa60",
						ConsentType = "explicit",
						DisplayName = "Postman client application",
						Permissions = "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
						RedirectUris = "[\"https://oauth.pstmn.io/v1/callback\"]",
						Requirements = "[\"ft:pkce\"]",
						Type = "public"
					});
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
			{
				b.Property<string>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("text");

				b.Property<string>("ApplicationId")
					.HasColumnType("text");

				b.Property<string>("ConcurrencyToken")
					.IsConcurrencyToken()
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<DateTime?>("CreationDate")
					.HasColumnType("timestamp with time zone");

				b.Property<string>("Properties")
					.HasColumnType("text");

				b.Property<string>("Scopes")
					.HasColumnType("text");

				b.Property<string>("Status")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<string>("Subject")
					.HasMaxLength(400)
					.HasColumnType("character varying(400)");

				b.Property<string>("Type")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.HasKey("Id");

				b.HasIndex("ApplicationId", "Status", "Subject", "Type");

				b.ToTable("OpenIddictAuthorizations", (string)null);
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreScope", b =>
			{
				b.Property<string>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("text");

				b.Property<string>("ConcurrencyToken")
					.IsConcurrencyToken()
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<string>("Description")
					.HasColumnType("text");

				b.Property<string>("Descriptions")
					.HasColumnType("text");

				b.Property<string>("DisplayName")
					.HasColumnType("text");

				b.Property<string>("DisplayNames")
					.HasColumnType("text");

				b.Property<string>("Name")
					.HasMaxLength(200)
					.HasColumnType("character varying(200)");

				b.Property<string>("Properties")
					.HasColumnType("text");

				b.Property<string>("Resources")
					.HasColumnType("text");

				b.HasKey("Id");

				b.HasIndex("Name")
					.IsUnique();

				b.ToTable("OpenIddictScopes", (string)null);
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreToken", b =>
			{
				b.Property<string>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("text");

				b.Property<string>("ApplicationId")
					.HasColumnType("text");

				b.Property<string>("AuthorizationId")
					.HasColumnType("text");

				b.Property<string>("ConcurrencyToken")
					.IsConcurrencyToken()
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<DateTime?>("CreationDate")
					.HasColumnType("timestamp with time zone");

				b.Property<DateTime?>("ExpirationDate")
					.HasColumnType("timestamp with time zone");

				b.Property<string>("Payload")
					.HasColumnType("text");

				b.Property<string>("Properties")
					.HasColumnType("text");

				b.Property<DateTime?>("RedemptionDate")
					.HasColumnType("timestamp with time zone");

				b.Property<string>("ReferenceId")
					.HasMaxLength(100)
					.HasColumnType("character varying(100)");

				b.Property<string>("Status")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.Property<string>("Subject")
					.HasMaxLength(400)
					.HasColumnType("character varying(400)");

				b.Property<string>("Type")
					.HasMaxLength(50)
					.HasColumnType("character varying(50)");

				b.HasKey("Id");

				b.HasIndex("AuthorizationId");

				b.HasIndex("ReferenceId")
					.IsUnique();

				b.HasIndex("ApplicationId", "Status", "Subject", "Type");

				b.ToTable("OpenIddictTokens", (string)null);
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
			{
				b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
					.WithMany()
					.HasForeignKey("RoleId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.ApplicationUser", null)
					.WithMany()
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.ApplicationUser", null)
					.WithMany()
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
			{
				b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
					.WithMany()
					.HasForeignKey("RoleId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("BlazorApp1.Server.Entities.ApplicationUser", null)
					.WithMany()
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.ApplicationUser", null)
					.WithMany()
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
			{
				b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", "Application")
					.WithMany("Authorizations")
					.HasForeignKey("ApplicationId");

				b.Navigation("Application");
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreToken", b =>
			{
				b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", "Application")
					.WithMany("Tokens")
					.HasForeignKey("ApplicationId");

				b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", "Authorization")
					.WithMany("Tokens")
					.HasForeignKey("AuthorizationId");

				b.Navigation("Application");

				b.Navigation("Authorization");
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", b =>
			{
				b.Navigation("Authorizations");

				b.Navigation("Tokens");
			});

		modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
			{
				b.Navigation("Tokens");
			});
#pragma warning restore 612, 618
	}
}
