#nullable disable

namespace BlazorApp1.Server.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class IdentitySeed : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.InsertData(
			table: "AspNetUsers",
			columns: new[]
			{
				"Id",
				"AccessFailedCount",
				"ConcurrencyStamp",
				"Email",
				"EmailConfirmed",
				"LockoutEnabled",
				"LockoutEnd",
				"NormalizedEmail",
				"NormalizedUserName",
				"PasswordHash",
				"PhoneNumber",
				"PhoneNumberConfirmed",
				"SecurityStamp",
				"TwoFactorEnabled",
				"UserName"
			},
			values: new object[]
			{
				"3a6b2a7f-6376-4865-b655-f9b9787d026f",
				0,
				"cf764f15-6a4b-4cd4-a18c-b674476b7910",
				"info@fake.com",
				false,
				false,
				null,
				"INFO@FAKE.COM",
				"INFO@FAKE.COM",
				"AQAAAAEAACcQAAAAEDpy7j+IAaIm7M0CbBbA7RkHz9jquH0Thpx1hNnpqzDKlT+Dmk0f88JRu0wEFoj3SQ==",
				null,
				false,
				"583a83a1-0560-4647-849a-92a17facb03d",
				false,
				"info@fake.com"
			});

		migrationBuilder.InsertData(
			table: "OpenIddictApplications",
			columns: new[]
			{
				"Id",
				"ClientId",
				"ClientSecret",
				"ConcurrencyToken",
				"ConsentType",
				"DisplayName",
				"DisplayNames",
				"Permissions",
				"PostLogoutRedirectUris",
				"Properties",
				"RedirectUris",
				"Requirements",
				"Type"
			},
			values: new object[,]
			{
				{
					"59d50e89-c579-4cc4-9cd1-a8db1399fe4e",
					"blazor-client",
					null,
					"611ff896-aa59-4181-b6c5-a70a34026f0b",
					"explicit",
					"Blazor client application",
					null,
					"[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
					"[\"https://localhost:7290/authentication/logout-callback\"]",
					null,
					"[\"https://localhost:7290/authentication/login-callback\"]",
					"[\"ft:pkce\"]",
					"public"
				},
				{
					"5cf57099-7162-425c-b2cd-11d7c02610d5",
					"swagger-client",
					null,
					"43dff1f3-ae23-46d7-8c6b-dfeb434cffee",
					"explicit",
					"Swagger client application",
					null,
					"[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
					null,
					null,
					"[\"https://localhost:7289/swagger/oauth2-redirect.html\"]",
					"[\"ft:pkce\"]",
					"public"
				},
				{
					"d2557a9b-404d-42ab-89ab-7019929e96a4",
					"postman-client",
					null,
					"23446b6e-6621-48ae-878c-ef6c371baa60",
					"explicit",
					"Postman client application",
					null,
					"[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]",
					null,
					null,
					"[\"https://oauth.pstmn.io/v1/callback\"]",
					"[\"ft:pkce\"]",
					"public"
				}
			});
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: "3a6b2a7f-6376-4865-b655-f9b9787d026f");

		migrationBuilder.DeleteData(
			table: "OpenIddictApplications",
			keyColumn: "Id",
			keyValue: "59d50e89-c579-4cc4-9cd1-a8db1399fe4e");

		migrationBuilder.DeleteData(
			table: "OpenIddictApplications",
			keyColumn: "Id",
			keyValue: "5cf57099-7162-425c-b2cd-11d7c02610d5");

		migrationBuilder.DeleteData(
			table: "OpenIddictApplications",
			keyColumn: "Id",
			keyValue: "d2557a9b-404d-42ab-89ab-7019929e96a4");
	}
}
