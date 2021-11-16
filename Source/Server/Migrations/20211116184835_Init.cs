using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorApp1.Server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedirectUris = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Descriptions = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Resources = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<string>(type: "text", nullable: true),
                    AuthorizationId = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReferenceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("04128d58-8b66-36e5-32ba-f3385d6e8fd1"), "Felicia Crist" },
                    { new Guid("04a27a19-1eda-cf2b-952d-65d4a719997a"), "Mafalda Bode" },
                    { new Guid("0697830e-860a-2bf9-d291-7fed75c73c56"), "Roosevelt Howe" },
                    { new Guid("070ad65c-6822-c33a-47be-9393c03e5104"), "Christina Wolff" },
                    { new Guid("08cd153e-2f62-7cda-7bb1-7fe011808748"), "Derick Smitham" },
                    { new Guid("0ffc47db-9309-995f-dbce-b6e7c870cd46"), "Mitchel Becker" },
                    { new Guid("1144d8c5-420e-48a8-31b4-d8e51f567995"), "Kelsie Grimes" },
                    { new Guid("1242e60a-c0ba-70b3-c443-dab5b4043594"), "Lazaro Keebler" },
                    { new Guid("12851ba4-00de-8cbb-c584-e0dd897baae9"), "Lindsey Johns" },
                    { new Guid("154fe414-ff38-34b1-0f24-ced86943d893"), "Nasir Lehner" },
                    { new Guid("17d0f4e7-b1e0-ed23-baad-25b6ee89aab2"), "Cara Wyman" },
                    { new Guid("18a870d2-2e3b-cfc1-0382-8a580fe2f277"), "Vena Cronin" },
                    { new Guid("21e1b9bb-16ad-ad59-4099-c213f68f965d"), "Jimmie Reichert" },
                    { new Guid("25c7b04c-4d02-4511-6902-e6f3f1b27293"), "Nadia Hilll" },
                    { new Guid("284490d6-6c5a-593c-1621-327c74186039"), "Suzanne Bins" },
                    { new Guid("2948cabe-23e7-d644-840a-1ccfdac4425f"), "Tamara Robel" },
                    { new Guid("2dd0636c-ffa2-5eaa-4846-e02d19f79df3"), "Edmond Murray" },
                    { new Guid("31dd184c-d424-d7a5-aca2-a2994dea04a0"), "Hallie Yost" },
                    { new Guid("331efd3a-84f4-af30-1666-9a30f7d35f27"), "Oda Bednar" },
                    { new Guid("33df1e97-b5be-3252-a9b4-e91857337778"), "Bonita Bergnaum" },
                    { new Guid("35aa5c5f-157b-3c80-6187-06de6bf1deb6"), "Raheem Rice" },
                    { new Guid("3810c59b-c5c6-087c-e293-e051e42b4700"), "Sadye Schuppe" },
                    { new Guid("38adf40d-b6b1-de2c-b373-71fa98979acc"), "Doris Bergstrom" },
                    { new Guid("3989a325-3e34-1071-e12e-336fe01dc478"), "Duncan Ebert" },
                    { new Guid("3b0c1ef7-8125-0b6b-0a48-777443bf9cab"), "Rafaela Schinner" },
                    { new Guid("3d50fd4f-16fe-4842-7ca2-00ac0a70b9b1"), "Omer Schroeder" },
                    { new Guid("3ed0d166-69d6-9761-7db5-90ce3ffc0a45"), "Bryce Romaguera" },
                    { new Guid("3ed4d4a5-c451-f3f9-2f41-b0ba9037c7be"), "Quinten Stehr" },
                    { new Guid("42591922-665d-9fa3-7037-f10091a3a85f"), "Dandre Koepp" },
                    { new Guid("42edf63d-ec92-0ac9-9c26-287d8aac06ff"), "Dorcas Walker" },
                    { new Guid("4829f5d4-6f59-458b-5bc6-121103b6b712"), "Wilma McKenzie" },
                    { new Guid("4d5db2d8-71c0-654d-236f-484f2795021f"), "Laney Kozey" },
                    { new Guid("4e163b9a-e2b5-dfba-d694-9fa8a887296a"), "Adele Block" },
                    { new Guid("50897c5b-72b0-8897-a43f-79418fa66621"), "Josephine Lang" },
                    { new Guid("522d4cbd-1071-1285-85e2-0b819bd9d7d9"), "Roxanne Harris" },
                    { new Guid("52435f4d-1611-b3e3-a1d2-8ef579f02c36"), "Royce McDermott" },
                    { new Guid("5369a4ab-00a6-9179-4603-6c293a98e27c"), "Carmelo Rolfson" },
                    { new Guid("53af0e7a-c19a-0916-dcf7-b3fe80c61708"), "Elinor Adams" },
                    { new Guid("53e3be62-c5bc-4e9a-5768-1aa83686a194"), "Kenyatta Fadel" },
                    { new Guid("541b61c9-7d52-4273-ef1a-6442066cdf11"), "Nola Bogan" },
                    { new Guid("56d45b44-afd0-7049-7232-343d510d36cb"), "Roberta Lowe" },
                    { new Guid("586e6e54-1cfa-603a-be42-6c2cacbae9ea"), "Gerda Bode" },
                    { new Guid("5baf84b7-1c45-5ebf-7f1d-75347874f2be"), "Kraig Bergstrom" },
                    { new Guid("5d0dd41d-bda1-30cb-f028-aa9e57c7275e"), "Elaina Smith" },
                    { new Guid("5e666cd3-6fe2-6176-1262-7f38b966d001"), "Chad Moore" },
                    { new Guid("617dcf9b-7cfe-cbe9-59f3-23651c620053"), "Jannie Mitchell" },
                    { new Guid("643debab-83d7-51bf-b7b3-de511ec1228d"), "Alexandre Howe" },
                    { new Guid("66470b57-f42a-425b-a110-d4a0867e78d3"), "Jarod Schuppe" },
                    { new Guid("6d5ba154-4cb7-eb99-b8fa-26a4c7326686"), "Keagan Lebsack" },
                    { new Guid("6f55d415-3a8e-dc62-8a26-7d7d8fc93a02"), "Gay Ratke" },
                    { new Guid("713276c1-9454-09fe-ed14-926874f23317"), "Conner Hilpert" },
                    { new Guid("718c32f3-1fe7-4419-a6e1-b19d54f2a83d"), "Clare Waelchi" },
                    { new Guid("73714cdd-3e4e-cf0f-4e25-7d530595fdea"), "Camila Bradtke" },
                    { new Guid("73b48709-dea4-ca56-65ed-d93876dd2245"), "Vinnie Douglas" },
                    { new Guid("7d78ea43-abfb-b70f-c782-3df920466697"), "Lisette Kohler" },
                    { new Guid("804b34d3-b814-037c-f33d-7345806f11b2"), "Barbara Hermann" },
                    { new Guid("8232ed95-51d6-f998-4b5a-7ee632343b01"), "Hilton Tromp" },
                    { new Guid("889f4120-e9e4-6f49-27c3-2329f11aef6a"), "Jack Mraz" },
                    { new Guid("8c19d376-ed4a-1066-7489-cff2a1fcd96d"), "Ardith Upton" },
                    { new Guid("8cb8a6af-35f9-d366-cb71-d3ac1f388e50"), "Leatha Toy" },
                    { new Guid("8d46f9c5-f829-4aa4-d9c8-17b00d49269a"), "Nathanael Gusikowski" },
                    { new Guid("8fd16a2b-acec-174b-0b1f-cf026a5a7844"), "Geovany VonRueden" },
                    { new Guid("90a48a35-ff55-dd56-09f2-27f53d8b04e4"), "Nikita Bechtelar" },
                    { new Guid("9577c69b-9a07-0868-820f-f7e50a5afa49"), "Esteban Daugherty" },
                    { new Guid("9c04b7ae-00fd-69d1-42c8-feca6582474d"), "Mac Adams" },
                    { new Guid("9cf662e3-8fde-10aa-2dd8-806ef37e8401"), "Velva Mosciski" },
                    { new Guid("9f241499-8f31-4208-cde6-e485ac69d982"), "Reinhold Frami" },
                    { new Guid("a15222ac-2a13-8617-b461-ad87c6a6f3e1"), "Summer Lowe" },
                    { new Guid("a2b5139e-a125-79af-6bdb-f3afad0fe216"), "Etha Cummings" },
                    { new Guid("a5722cef-ba69-bc92-cb00-57be51f085cd"), "Camille Bailey" },
                    { new Guid("a5ff7238-9817-f0e5-11ab-fcb5bf0df8ef"), "Austyn Rohan" },
                    { new Guid("a98b80be-7f74-9562-5944-788049c88bf6"), "Mariam Bosco" },
                    { new Guid("b197126d-4286-9e8e-263c-8f61835a5c10"), "Donnie Reichert" },
                    { new Guid("b2851e06-8ade-c968-9dac-14db6834a2c1"), "Camila Gerhold" },
                    { new Guid("b4987ab3-e221-b572-0bcc-5e3daaa5d732"), "Carolina Rath" },
                    { new Guid("b53e07af-dc0e-e88b-93c3-9b0bde29909b"), "Edward Braun" },
                    { new Guid("b787dbb8-852b-ad53-e80f-dff87fe93172"), "Carrie Heidenreich" },
                    { new Guid("b8b6fb89-083d-895a-2264-691ac59c1675"), "Melyna Jacobs" },
                    { new Guid("bc8a68b7-2808-9d43-9783-f389932d4b26"), "Jarrod Hermiston" },
                    { new Guid("c57940c0-9bec-b9ed-510d-021fe0343a54"), "Alden Bauch" },
                    { new Guid("c6f9546f-0667-f48b-d382-4b425755ea5f"), "Florence Bednar" },
                    { new Guid("c7fff098-f439-1f11-06c4-5778f6a9976f"), "Jada Schmidt" },
                    { new Guid("caca4043-f3a5-849d-13be-ecd1229801db"), "Aiden Fisher" },
                    { new Guid("cb6a0e73-e5e1-1752-1a54-8db6f086a35b"), "Rico Russel" },
                    { new Guid("cb8a8b6d-cab6-a288-4a68-2071284b10dd"), "Reba Boehm" },
                    { new Guid("d1afe6c9-af2a-28b6-a906-353e528d9a57"), "Howard Hermann" },
                    { new Guid("d276a01b-00bf-c2a1-dd7b-15f022d5cd7e"), "Joyce Stroman" },
                    { new Guid("d415b398-5a98-e4c9-091a-40d049d2ca3d"), "Elda Huel" },
                    { new Guid("dd3e5e43-0b1e-925f-3417-3c908054d9cd"), "Gustave Fritsch" },
                    { new Guid("e35e689b-68ff-9161-9fde-1873da597915"), "Tyree Sanford" },
                    { new Guid("ece41b3e-4b5f-d049-0694-53c0780db331"), "Toy Heller" },
                    { new Guid("f17e7d69-9913-c5e1-e84e-796e0f7c565b"), "Jordy Bins" },
                    { new Guid("f359910d-0d56-a756-7587-6c7932af46f5"), "Susanna Kuphal" },
                    { new Guid("f394d1d5-9fa2-562f-5d54-b52c919544ea"), "Natalie Mayert" },
                    { new Guid("f60a7466-dd8c-804d-4708-ed4cbbb82e57"), "Lenora VonRueden" },
                    { new Guid("f74cc591-70c0-db3d-13ee-32f84f7dd765"), "Audrey Orn" },
                    { new Guid("f8c7e299-5eb5-5e4a-21d0-6cf0aff0aa48"), "Loraine Streich" },
                    { new Guid("fa3f0f78-c566-d7c7-581f-1ca548dda970"), "Coralie Moore" },
                    { new Guid("fb9b7812-f0b1-66ea-0291-ba060b9467c6"), "William Lang" },
                    { new Guid("feb4db06-bab0-22b2-945d-5aaac23f71b1"), "Cordie Morar" }
                });

            migrationBuilder.InsertData(
                table: "OpenIddictApplications",
                columns: new[] { "Id", "ClientId", "ClientSecret", "ConcurrencyToken", "ConsentType", "DisplayName", "DisplayNames", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Requirements", "Type" },
                values: new object[,]
                {
                    { "59d50e89-c579-4cc4-9cd1-a8db1399fe4e", "blazor-client", null, "75c09b06-4950-4ff4-afda-da1b17762cab", "explicit", "Blazor client application", null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]", "[\"https://localhost:7290/authentication/logout-callback\"]", null, "[\"https://localhost:7290/authentication/login-callback\"]", "[\"ft:pkce\"]", "public" },
                    { "5cf57099-7162-425c-b2cd-11d7c02610d5", "swagger-client", null, "6ddd4624-33c5-45e1-b47e-cf3ec42d3ab8", "explicit", "Swagger client application", null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]", null, null, "[\"https://localhost:7289/swagger/oauth2-redirect.html\"]", "[\"ft:pkce\"]", "public" },
                    { "d2557a9b-404d-42ab-89ab-7019929e96a4", "postman-client", null, "31d63ffc-8677-4b24-8815-2e423014c1c1", "explicit", "Postman client application", null, "[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\"]", null, null, "[\"https://oauth.pstmn.io/v1/callback\"]", "[\"ft:pkce\"]", "public" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");
        }
    }
}
