#nullable disable

namespace BlazorApp1.Server.Migrations;

using System;
using System.Text.Json;

using BlazorApp1.Server.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("00000000000002_DataSeed")]
partial class DataSeed
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.0")
			.HasAnnotation("Relational:MaxIdentifierLength", 63);

		NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

		modelBuilder.Entity("BlazorApp1.Server.Entities.Character", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("uuid");

				b.Property<string>("Name")
					.HasColumnType("text");

				b.HasKey("Id");

				b.ToTable("Characters");

				b.HasData(
					new
					{
						Id = new Guid("9ce72319-67c8-3283-195a-a77e75604dd3"),
						Name = "Myrtice Shanahan"
					},
					new
					{
						Id = new Guid("9f3898d1-93b3-571b-9aad-1f1faf6354ed"),
						Name = "Elissa Jacobson"
					},
					new
					{
						Id = new Guid("b79cf1de-c2bc-e136-4f7c-1b46ae5bb33d"),
						Name = "Enrique Stehr"
					},
					new
					{
						Id = new Guid("c671e502-0f6a-05e8-d4fa-8bca29522f0a"),
						Name = "Rae Bechtelar"
					},
					new
					{
						Id = new Guid("4465c57b-9dc9-a37b-5b44-c6092a1aa41b"),
						Name = "Jeanette Emmerich"
					},
					new
					{
						Id = new Guid("ffc4233d-1070-33ac-2e47-9c19acf5b7ad"),
						Name = "Lottie Schaefer"
					},
					new
					{
						Id = new Guid("0f0c534c-0cec-22b6-3689-d38d1aa57563"),
						Name = "Adella Hansen"
					},
					new
					{
						Id = new Guid("b4626565-68b9-cf12-5277-064aeaa8716c"),
						Name = "Adalberto Rohan"
					},
					new
					{
						Id = new Guid("6abed62e-f20a-9b62-390e-cbc87afda984"),
						Name = "Rebecca Hirthe"
					},
					new
					{
						Id = new Guid("4a00a997-8589-b8fa-c85a-2c83cab2c991"),
						Name = "Samson Lesch"
					},
					new
					{
						Id = new Guid("bb479a6b-2415-faa5-a7fe-2b4152eb9941"),
						Name = "Dexter Langworth"
					},
					new
					{
						Id = new Guid("2078c40e-26f2-1378-4694-6842dfc18fb8"),
						Name = "Leon Orn"
					},
					new
					{
						Id = new Guid("cfe900fe-a2c6-a57d-0dbd-0381a64f2189"),
						Name = "Veda Mante"
					},
					new
					{
						Id = new Guid("a0a55cb9-e727-b2a2-2a01-95d0fbc7ac7f"),
						Name = "Darrick Rodriguez"
					},
					new
					{
						Id = new Guid("c011a956-b1ae-3442-2871-4e5cf68bdebe"),
						Name = "Roger Tillman"
					},
					new
					{
						Id = new Guid("7a85ee12-52fd-44ec-5145-d26ff1d666f8"),
						Name = "Jammie Auer"
					},
					new
					{
						Id = new Guid("7d8a492d-74fc-6b65-1682-ec6b67341cc5"),
						Name = "Devan Berge"
					},
					new
					{
						Id = new Guid("4c253150-dab3-2cbd-f603-e6d32a59fb32"),
						Name = "Carolyn Stark"
					},
					new
					{
						Id = new Guid("c23f6761-c4b1-1eea-40cb-d97b8aef36e3"),
						Name = "Haven Friesen"
					},
					new
					{
						Id = new Guid("0a67ae12-6f2a-6632-d014-34b906d460d6"),
						Name = "Lexus Armstrong"
					},
					new
					{
						Id = new Guid("39574422-45a0-a101-f864-411580b9b274"),
						Name = "Glennie Marvin"
					},
					new
					{
						Id = new Guid("f0f197a8-e7fe-0c42-c92b-db9184801228"),
						Name = "Martina Okuneva"
					},
					new
					{
						Id = new Guid("b2b4e2f8-4700-d1e8-9eb9-27b8f22dc7cf"),
						Name = "Abdullah Cartwright"
					},
					new
					{
						Id = new Guid("a9badbe2-1cb6-5964-5df8-31556b01bcc5"),
						Name = "Elmore Dietrich"
					},
					new
					{
						Id = new Guid("26d22fec-3b3e-a9f9-0194-1c608db03afd"),
						Name = "Carole Stroman"
					},
					new
					{
						Id = new Guid("46b94046-653e-ba68-d729-b7c170f79da9"),
						Name = "Lonie Runte"
					},
					new
					{
						Id = new Guid("5204341d-19e5-dfae-ba28-6c42384444a2"),
						Name = "Allan Howe"
					},
					new
					{
						Id = new Guid("9f6412af-c38e-54fc-f788-594d28a7faa0"),
						Name = "Arnoldo Fisher"
					},
					new
					{
						Id = new Guid("d91c6346-bc4e-d3f9-70e7-0a9bc861685a"),
						Name = "Trinity Hoeger"
					},
					new
					{
						Id = new Guid("0662da6a-2c32-5b96-1629-6e9e2990dd1d"),
						Name = "Imani Lynch"
					},
					new
					{
						Id = new Guid("a9bfc692-700d-5421-1ddd-c3daacdd4cba"),
						Name = "Charlie Pagac"
					},
					new
					{
						Id = new Guid("ceb48326-e397-ce68-0e37-5e6746e443ab"),
						Name = "Mariam Auer"
					},
					new
					{
						Id = new Guid("ddbb5e37-3bdb-1351-2fce-78159a70e36a"),
						Name = "Keshaun Crona"
					},
					new
					{
						Id = new Guid("2e679897-a57e-1b33-aa5b-fb6017fb7ed8"),
						Name = "Waylon Greenholt"
					},
					new
					{
						Id = new Guid("468e238c-4638-518f-958e-138cc4a9ede6"),
						Name = "Will Watsica"
					},
					new
					{
						Id = new Guid("d1aded39-08dc-d721-6d83-8d38df3c2a03"),
						Name = "Josue Von"
					},
					new
					{
						Id = new Guid("7ccdbc2d-4703-9afb-b7da-fce5848f3266"),
						Name = "Jazmin Koelpin"
					},
					new
					{
						Id = new Guid("fea857ad-024f-13ab-2f1a-052380d3d651"),
						Name = "Madge Durgan"
					},
					new
					{
						Id = new Guid("8abe03dd-90d5-f3fd-191a-6fdb79d5392b"),
						Name = "Shaylee O'Kon"
					},
					new
					{
						Id = new Guid("445b309c-0b76-ba5e-7645-a97c8c21d4ad"),
						Name = "Nathanial Crist"
					},
					new
					{
						Id = new Guid("a320752e-964b-47aa-2632-c92ee273dcb5"),
						Name = "Devonte Quigley"
					},
					new
					{
						Id = new Guid("68011563-cd65-8124-e48c-63c9ca5c0e3d"),
						Name = "Orie Beer"
					},
					new
					{
						Id = new Guid("a08c97f1-064e-efae-af54-028a2bc998b8"),
						Name = "Darren Wunsch"
					},
					new
					{
						Id = new Guid("ea0568ce-a36d-8975-0897-cee90fc5b54f"),
						Name = "Elody Denesik"
					},
					new
					{
						Id = new Guid("2e660191-d559-4073-dced-d31fd4dbfb77"),
						Name = "Woodrow Johns"
					},
					new
					{
						Id = new Guid("61e5c296-fd60-4fbd-9f55-8ebfdb52b769"),
						Name = "Abraham Tromp"
					},
					new
					{
						Id = new Guid("b89e7add-1c12-ae81-1096-51651b82ad8e"),
						Name = "Leonie Lang"
					},
					new
					{
						Id = new Guid("92e302a2-0024-e3fb-617d-3573fb9a40bf"),
						Name = "Lamar Hartmann"
					},
					new
					{
						Id = new Guid("54bb7089-f2f2-d59f-adf1-4caeedcaebd0"),
						Name = "Lyda Legros"
					},
					new
					{
						Id = new Guid("b5d7c833-315c-22ac-2979-b2eee68af468"),
						Name = "Estelle Weimann"
					},
					new
					{
						Id = new Guid("407835f0-e26a-b0e2-62b4-073dc7949733"),
						Name = "Dennis Nitzsche"
					},
					new
					{
						Id = new Guid("c94a8e03-e1e8-5171-ccbd-cc38162a85e6"),
						Name = "Beaulah Jones"
					},
					new
					{
						Id = new Guid("ec113066-4faf-a494-77ee-6ce1caff9f41"),
						Name = "Wellington Lehner"
					},
					new
					{
						Id = new Guid("c60a3c91-4a4c-8ecb-4f4d-5866fd90afb7"),
						Name = "Efrain Cremin"
					},
					new
					{
						Id = new Guid("a30c6124-3c80-c7b6-b8cb-76fcbb6003a4"),
						Name = "Darren Kovacek"
					},
					new
					{
						Id = new Guid("afc21a80-d7e4-805b-e154-5789c2517856"),
						Name = "Ida Yundt"
					},
					new
					{
						Id = new Guid("8d629047-f174-6932-7eb0-344228d36fbf"),
						Name = "London Rempel"
					},
					new
					{
						Id = new Guid("93035114-7e75-0bb6-868f-70ed6afa4874"),
						Name = "Sandrine Murray"
					},
					new
					{
						Id = new Guid("d978e583-8ef7-b76b-a692-64cd68b742fb"),
						Name = "Dallas Wolff"
					},
					new
					{
						Id = new Guid("5ae98d2d-890e-8c7c-9cc3-71bea94ed32d"),
						Name = "Providenci O'Kon"
					},
					new
					{
						Id = new Guid("a3dea530-55b9-c368-80a6-5310993b5f0a"),
						Name = "Antonietta Beatty"
					},
					new
					{
						Id = new Guid("b6df84c0-0028-51d4-272a-7ed0d648468b"),
						Name = "Brain Doyle"
					},
					new
					{
						Id = new Guid("339fc1d9-a93c-1289-871a-d799d60f435b"),
						Name = "Lambert Balistreri"
					},
					new
					{
						Id = new Guid("8ee1c607-8bee-f4a2-e646-9d7a25b7d1a2"),
						Name = "Christelle Klein"
					},
					new
					{
						Id = new Guid("0cf39ad6-f699-4100-451f-b0891ed4668d"),
						Name = "Arielle Davis"
					},
					new
					{
						Id = new Guid("70405667-547f-7e67-38bf-dd272dcd1c73"),
						Name = "Scarlett Keebler"
					},
					new
					{
						Id = new Guid("ead75642-6a4d-714b-f9d9-25a6ecc7c707"),
						Name = "Oleta Hand"
					},
					new
					{
						Id = new Guid("0e5ccfa2-c6ea-b71e-1fd1-51f8e6b03c5c"),
						Name = "Kaia Fritsch"
					},
					new
					{
						Id = new Guid("fa827689-c45b-e5c8-254f-ce42a0f763dd"),
						Name = "Christy Gusikowski"
					},
					new
					{
						Id = new Guid("409dd28e-d732-df43-f293-b1afa91fcffd"),
						Name = "Murl Tillman"
					},
					new
					{
						Id = new Guid("6fbe01ae-aff9-7f7b-e7a3-1048cf1757e7"),
						Name = "Iliana Herman"
					},
					new
					{
						Id = new Guid("9bb3c3c0-c409-4f01-4bc5-25fc911b298d"),
						Name = "Chelsey Schaefer"
					},
					new
					{
						Id = new Guid("040f8b33-7816-2809-d3d4-27bb909cae9f"),
						Name = "Verla Rogahn"
					},
					new
					{
						Id = new Guid("49d6923b-f032-8caf-86a3-ab2856c99ef7"),
						Name = "Summer Bergstrom"
					},
					new
					{
						Id = new Guid("b6dcaa1f-9317-b646-bad0-b33b7aad0b7d"),
						Name = "Jena Mraz"
					},
					new
					{
						Id = new Guid("219e7c22-4120-6af1-3988-8f6d39f7866f"),
						Name = "Thelma Hettinger"
					},
					new
					{
						Id = new Guid("9a83a195-58f7-9ec3-b320-4e2cd3eca896"),
						Name = "Amparo Gaylord"
					},
					new
					{
						Id = new Guid("43ca9579-7bb6-1771-7518-d898709f7343"),
						Name = "Jackie Wyman"
					},
					new
					{
						Id = new Guid("614bcfcb-9bb1-328c-444f-6ef318f636dc"),
						Name = "Xavier Doyle"
					},
					new
					{
						Id = new Guid("d027fd21-131c-8d0d-8832-479ad74c4da5"),
						Name = "Jakayla Rutherford"
					},
					new
					{
						Id = new Guid("b5db03b8-457a-88e4-6b6c-f51ff54b136c"),
						Name = "Lourdes Jast"
					},
					new
					{
						Id = new Guid("92d7e066-2654-05c3-3dd8-f268b01a61bd"),
						Name = "Annabelle Jacobi"
					},
					new
					{
						Id = new Guid("ff601cfd-2b7e-f617-c8a5-82124b5c328f"),
						Name = "Delphine Hegmann"
					},
					new
					{
						Id = new Guid("c18934da-bb70-9420-eefa-51a97e9ed873"),
						Name = "Maynard Bartell"
					},
					new
					{
						Id = new Guid("02c07b82-1b85-8d01-17a3-ad6059be2a1c"),
						Name = "Augustine Deckow"
					},
					new
					{
						Id = new Guid("0c756ca3-186c-e45e-7c0d-1a60e00c695f"),
						Name = "Ryleigh Rogahn"
					},
					new
					{
						Id = new Guid("66c25304-84ee-919d-bb0a-d724bc291c67"),
						Name = "Mckenna Buckridge"
					},
					new
					{
						Id = new Guid("827498dc-e5b3-a662-bbba-4d5d3a81f7ce"),
						Name = "Danial Deckow"
					},
					new
					{
						Id = new Guid("cb86d966-092b-2dc5-061c-bbfbe30c4cd2"),
						Name = "Norene Russel"
					},
					new
					{
						Id = new Guid("52ba42a1-1588-fed7-9030-b3c81df7dafd"),
						Name = "Unique Herman"
					},
					new
					{
						Id = new Guid("395e9b08-56f3-05ac-ff5b-1732c4bd86c6"),
						Name = "Adrien Heathcote"
					},
					new
					{
						Id = new Guid("3e9b3abc-71df-a93b-5fc3-3fd2938bcac6"),
						Name = "Fermin Hartmann"
					},
					new
					{
						Id = new Guid("e46336b6-1a6a-3e2b-ee0f-ad30c4ef76c6"),
						Name = "Woodrow Swaniawski"
					},
					new
					{
						Id = new Guid("bb9d2c37-8b9a-b70c-3536-709f1f59e2d1"),
						Name = "Candice Heaney"
					},
					new
					{
						Id = new Guid("95f329de-97a2-d27e-51df-d56124162f3d"),
						Name = "Chris Harris"
					},
					new
					{
						Id = new Guid("9451839f-762b-7017-e6f3-b61b341f8d4e"),
						Name = "Hosea Powlowski"
					},
					new
					{
						Id = new Guid("611a627c-9e01-04ce-57ae-431d57b9a3a3"),
						Name = "Willow DuBuque"
					},
					new
					{
						Id = new Guid("d357a261-55ac-056b-81de-c9be5d5ca66d"),
						Name = "Rae Schaden"
					},
					new
					{
						Id = new Guid("47a2b377-2c15-fa89-e428-2dcdd3e971e1"),
						Name = "Rosemarie Carroll"
					},
					new
					{
						Id = new Guid("6045bd2f-6372-8ad2-d4bb-8782d9d23bf1"),
						Name = "Grant Ferry"
					});
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.CoreEffect", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("uuid");

				b.Property<Guid>("CharacterId")
					.HasColumnType("uuid");

				b.Property<string>("Path")
					.HasColumnType("text");

				b.Property<JsonElement>("Rule")
					.HasColumnType("jsonb");

				b.HasKey("Id");

				b.HasIndex("CharacterId");

				b.ToTable("CoreEffects");
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.Effect", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("uuid");

				b.Property<Guid>("FeatureId")
					.HasColumnType("uuid");

				b.Property<string>("Name")
					.HasColumnType("text");

				b.Property<string>("Path")
					.HasColumnType("text");

				b.Property<JsonElement>("Rule")
					.HasColumnType("jsonb");

				b.HasKey("Id");

				b.HasIndex("FeatureId");

				b.ToTable("Effects");
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.Feature", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("uuid");

				b.Property<string>("Name")
					.HasColumnType("text");

				b.HasKey("Id");

				b.ToTable("Features");

				b.HasData(
					new
					{
						Id = new Guid("333bad4f-b10d-4a30-971b-f449e4469952"),
						Name = "Dwarf"
					},
					new
					{
						Id = new Guid("7ec8239f-4e59-4b43-89f4-fb0ddb4557ac"),
						Name = "Elf"
					});
			});

		modelBuilder.Entity("CharacterFeature", b =>
			{
				b.Property<Guid>("CharactersId")
					.HasColumnType("uuid");

				b.Property<Guid>("FeaturesId")
					.HasColumnType("uuid");

				b.HasKey("CharactersId", "FeaturesId");

				b.HasIndex("FeaturesId");

				b.ToTable("CharacterFeature");
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.CoreEffect", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.Character", "Character")
					.WithMany("Effects")
					.HasForeignKey("CharacterId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Character");
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.Effect", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.Feature", "Feature")
					.WithMany("Effects")
					.HasForeignKey("FeatureId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Feature");
			});

		modelBuilder.Entity("CharacterFeature", b =>
			{
				b.HasOne("BlazorApp1.Server.Entities.Character", null)
					.WithMany()
					.HasForeignKey("CharactersId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("BlazorApp1.Server.Entities.Feature", null)
					.WithMany()
					.HasForeignKey("FeaturesId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.Character", b =>
			{
				b.Navigation("Effects");
			});

		modelBuilder.Entity("BlazorApp1.Server.Entities.Feature", b =>
			{
				b.Navigation("Effects");
			});
#pragma warning restore 612, 618
	}
}
