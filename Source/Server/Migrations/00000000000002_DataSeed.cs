using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Server.Migrations;

public partial class DataSeed : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.InsertData(
			table: "Characters",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("02c07b82-1b85-8d01-17a3-ad6059be2a1c"), "Augustine Deckow" },
				{ new Guid("040f8b33-7816-2809-d3d4-27bb909cae9f"), "Verla Rogahn" },
				{ new Guid("0662da6a-2c32-5b96-1629-6e9e2990dd1d"), "Imani Lynch" },
				{ new Guid("0a67ae12-6f2a-6632-d014-34b906d460d6"), "Lexus Armstrong" },
				{ new Guid("0c756ca3-186c-e45e-7c0d-1a60e00c695f"), "Ryleigh Rogahn" },
				{ new Guid("0cf39ad6-f699-4100-451f-b0891ed4668d"), "Arielle Davis" },
				{ new Guid("0e5ccfa2-c6ea-b71e-1fd1-51f8e6b03c5c"), "Kaia Fritsch" },
				{ new Guid("0f0c534c-0cec-22b6-3689-d38d1aa57563"), "Adella Hansen" },
				{ new Guid("2078c40e-26f2-1378-4694-6842dfc18fb8"), "Leon Orn" },
				{ new Guid("219e7c22-4120-6af1-3988-8f6d39f7866f"), "Thelma Hettinger" },
				{ new Guid("26d22fec-3b3e-a9f9-0194-1c608db03afd"), "Carole Stroman" },
				{ new Guid("2e660191-d559-4073-dced-d31fd4dbfb77"), "Woodrow Johns" },
				{ new Guid("2e679897-a57e-1b33-aa5b-fb6017fb7ed8"), "Waylon Greenholt" },
				{ new Guid("339fc1d9-a93c-1289-871a-d799d60f435b"), "Lambert Balistreri" },
				{ new Guid("39574422-45a0-a101-f864-411580b9b274"), "Glennie Marvin" },
				{ new Guid("395e9b08-56f3-05ac-ff5b-1732c4bd86c6"), "Adrien Heathcote" },
				{ new Guid("3e9b3abc-71df-a93b-5fc3-3fd2938bcac6"), "Fermin Hartmann" },
				{ new Guid("407835f0-e26a-b0e2-62b4-073dc7949733"), "Dennis Nitzsche" },
				{ new Guid("409dd28e-d732-df43-f293-b1afa91fcffd"), "Murl Tillman" },
				{ new Guid("43ca9579-7bb6-1771-7518-d898709f7343"), "Jackie Wyman" },
				{ new Guid("445b309c-0b76-ba5e-7645-a97c8c21d4ad"), "Nathanial Crist" },
				{ new Guid("4465c57b-9dc9-a37b-5b44-c6092a1aa41b"), "Jeanette Emmerich" },
				{ new Guid("468e238c-4638-518f-958e-138cc4a9ede6"), "Will Watsica" },
				{ new Guid("46b94046-653e-ba68-d729-b7c170f79da9"), "Lonie Runte" },
				{ new Guid("47a2b377-2c15-fa89-e428-2dcdd3e971e1"), "Rosemarie Carroll" },
				{ new Guid("49d6923b-f032-8caf-86a3-ab2856c99ef7"), "Summer Bergstrom" },
				{ new Guid("4a00a997-8589-b8fa-c85a-2c83cab2c991"), "Samson Lesch" },
				{ new Guid("4c253150-dab3-2cbd-f603-e6d32a59fb32"), "Carolyn Stark" },
				{ new Guid("5204341d-19e5-dfae-ba28-6c42384444a2"), "Allan Howe" },
				{ new Guid("52ba42a1-1588-fed7-9030-b3c81df7dafd"), "Unique Herman" },
				{ new Guid("54bb7089-f2f2-d59f-adf1-4caeedcaebd0"), "Lyda Legros" },
				{ new Guid("5ae98d2d-890e-8c7c-9cc3-71bea94ed32d"), "Providenci O'Kon" },
				{ new Guid("6045bd2f-6372-8ad2-d4bb-8782d9d23bf1"), "Grant Ferry" },
				{ new Guid("611a627c-9e01-04ce-57ae-431d57b9a3a3"), "Willow DuBuque" },
				{ new Guid("614bcfcb-9bb1-328c-444f-6ef318f636dc"), "Xavier Doyle" },
				{ new Guid("61e5c296-fd60-4fbd-9f55-8ebfdb52b769"), "Abraham Tromp" },
				{ new Guid("66c25304-84ee-919d-bb0a-d724bc291c67"), "Mckenna Buckridge" },
				{ new Guid("68011563-cd65-8124-e48c-63c9ca5c0e3d"), "Orie Beer" },
				{ new Guid("6abed62e-f20a-9b62-390e-cbc87afda984"), "Rebecca Hirthe" },
				{ new Guid("6fbe01ae-aff9-7f7b-e7a3-1048cf1757e7"), "Iliana Herman" },
				{ new Guid("70405667-547f-7e67-38bf-dd272dcd1c73"), "Scarlett Keebler" },
				{ new Guid("7a85ee12-52fd-44ec-5145-d26ff1d666f8"), "Jammie Auer" },
				{ new Guid("7ccdbc2d-4703-9afb-b7da-fce5848f3266"), "Jazmin Koelpin" },
				{ new Guid("7d8a492d-74fc-6b65-1682-ec6b67341cc5"), "Devan Berge" },
				{ new Guid("827498dc-e5b3-a662-bbba-4d5d3a81f7ce"), "Danial Deckow" },
				{ new Guid("8abe03dd-90d5-f3fd-191a-6fdb79d5392b"), "Shaylee O'Kon" },
				{ new Guid("8d629047-f174-6932-7eb0-344228d36fbf"), "London Rempel" },
				{ new Guid("8ee1c607-8bee-f4a2-e646-9d7a25b7d1a2"), "Christelle Klein" },
				{ new Guid("92d7e066-2654-05c3-3dd8-f268b01a61bd"), "Annabelle Jacobi" },
				{ new Guid("92e302a2-0024-e3fb-617d-3573fb9a40bf"), "Lamar Hartmann" },
				{ new Guid("93035114-7e75-0bb6-868f-70ed6afa4874"), "Sandrine Murray" },
				{ new Guid("9451839f-762b-7017-e6f3-b61b341f8d4e"), "Hosea Powlowski" },
				{ new Guid("95f329de-97a2-d27e-51df-d56124162f3d"), "Chris Harris" },
				{ new Guid("9a83a195-58f7-9ec3-b320-4e2cd3eca896"), "Amparo Gaylord" },
				{ new Guid("9bb3c3c0-c409-4f01-4bc5-25fc911b298d"), "Chelsey Schaefer" },
				{ new Guid("9ce72319-67c8-3283-195a-a77e75604dd3"), "Myrtice Shanahan" },
				{ new Guid("9f3898d1-93b3-571b-9aad-1f1faf6354ed"), "Elissa Jacobson" },
				{ new Guid("9f6412af-c38e-54fc-f788-594d28a7faa0"), "Arnoldo Fisher" },
				{ new Guid("a08c97f1-064e-efae-af54-028a2bc998b8"), "Darren Wunsch" },
				{ new Guid("a0a55cb9-e727-b2a2-2a01-95d0fbc7ac7f"), "Darrick Rodriguez" },
				{ new Guid("a30c6124-3c80-c7b6-b8cb-76fcbb6003a4"), "Darren Kovacek" },
				{ new Guid("a320752e-964b-47aa-2632-c92ee273dcb5"), "Devonte Quigley" },
				{ new Guid("a3dea530-55b9-c368-80a6-5310993b5f0a"), "Antonietta Beatty" },
				{ new Guid("a9badbe2-1cb6-5964-5df8-31556b01bcc5"), "Elmore Dietrich" },
				{ new Guid("a9bfc692-700d-5421-1ddd-c3daacdd4cba"), "Charlie Pagac" },
				{ new Guid("afc21a80-d7e4-805b-e154-5789c2517856"), "Ida Yundt" },
				{ new Guid("b2b4e2f8-4700-d1e8-9eb9-27b8f22dc7cf"), "Abdullah Cartwright" },
				{ new Guid("b4626565-68b9-cf12-5277-064aeaa8716c"), "Adalberto Rohan" },
				{ new Guid("b5d7c833-315c-22ac-2979-b2eee68af468"), "Estelle Weimann" },
				{ new Guid("b5db03b8-457a-88e4-6b6c-f51ff54b136c"), "Lourdes Jast" },
				{ new Guid("b6dcaa1f-9317-b646-bad0-b33b7aad0b7d"), "Jena Mraz" },
				{ new Guid("b6df84c0-0028-51d4-272a-7ed0d648468b"), "Brain Doyle" },
				{ new Guid("b79cf1de-c2bc-e136-4f7c-1b46ae5bb33d"), "Enrique Stehr" },
				{ new Guid("b89e7add-1c12-ae81-1096-51651b82ad8e"), "Leonie Lang" },
				{ new Guid("bb479a6b-2415-faa5-a7fe-2b4152eb9941"), "Dexter Langworth" },
				{ new Guid("bb9d2c37-8b9a-b70c-3536-709f1f59e2d1"), "Candice Heaney" },
				{ new Guid("c011a956-b1ae-3442-2871-4e5cf68bdebe"), "Roger Tillman" },
				{ new Guid("c18934da-bb70-9420-eefa-51a97e9ed873"), "Maynard Bartell" },
				{ new Guid("c23f6761-c4b1-1eea-40cb-d97b8aef36e3"), "Haven Friesen" },
				{ new Guid("c60a3c91-4a4c-8ecb-4f4d-5866fd90afb7"), "Efrain Cremin" },
				{ new Guid("c671e502-0f6a-05e8-d4fa-8bca29522f0a"), "Rae Bechtelar" },
				{ new Guid("c94a8e03-e1e8-5171-ccbd-cc38162a85e6"), "Beaulah Jones" },
				{ new Guid("cb86d966-092b-2dc5-061c-bbfbe30c4cd2"), "Norene Russel" },
				{ new Guid("ceb48326-e397-ce68-0e37-5e6746e443ab"), "Mariam Auer" },
				{ new Guid("cfe900fe-a2c6-a57d-0dbd-0381a64f2189"), "Veda Mante" },
				{ new Guid("d027fd21-131c-8d0d-8832-479ad74c4da5"), "Jakayla Rutherford" },
				{ new Guid("d1aded39-08dc-d721-6d83-8d38df3c2a03"), "Josue Von" },
				{ new Guid("d357a261-55ac-056b-81de-c9be5d5ca66d"), "Rae Schaden" },
				{ new Guid("d91c6346-bc4e-d3f9-70e7-0a9bc861685a"), "Trinity Hoeger" },
				{ new Guid("d978e583-8ef7-b76b-a692-64cd68b742fb"), "Dallas Wolff" },
				{ new Guid("ddbb5e37-3bdb-1351-2fce-78159a70e36a"), "Keshaun Crona" },
				{ new Guid("e46336b6-1a6a-3e2b-ee0f-ad30c4ef76c6"), "Woodrow Swaniawski" },
				{ new Guid("ea0568ce-a36d-8975-0897-cee90fc5b54f"), "Elody Denesik" },
				{ new Guid("ead75642-6a4d-714b-f9d9-25a6ecc7c707"), "Oleta Hand" },
				{ new Guid("ec113066-4faf-a494-77ee-6ce1caff9f41"), "Wellington Lehner" },
				{ new Guid("f0f197a8-e7fe-0c42-c92b-db9184801228"), "Martina Okuneva" },
				{ new Guid("fa827689-c45b-e5c8-254f-ce42a0f763dd"), "Christy Gusikowski" },
				{ new Guid("fea857ad-024f-13ab-2f1a-052380d3d651"), "Madge Durgan" },
				{ new Guid("ff601cfd-2b7e-f617-c8a5-82124b5c328f"), "Delphine Hegmann" },
				{ new Guid("ffc4233d-1070-33ac-2e47-9c19acf5b7ad"), "Lottie Schaefer" }
			});

		migrationBuilder.InsertData(
			table: "Features",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("333bad4f-b10d-4a30-971b-f449e4469952"), "Dwarf" },
				{ new Guid("7ec8239f-4e59-4b43-89f4-fb0ddb4557ac"), "Elf" }
			});

		migrationBuilder.InsertData(
			table: "Effects",
			columns: new[] { "Id", "Name", "Path", "Rule", "FeatureId" },
			values: new object[,]
			{
				{
					new Guid("a5e7765d-b863-4d97-8c59-f8a989f2fb2d"),
					"Strength",
					"/Strength",
					"{\"+\":[{\"var\":\"Strength\"},2]}",
					new Guid("333bad4f-b10d-4a30-971b-f449e4469952")
				},
				{
					new Guid("0094a7b3-6256-4ef0-8320-966757a96b17"),
					"Constitution",
					"/Constitution",
					"{\"+\":[{\"var\":\"Constitution\"},1]}",
					new Guid("333bad4f-b10d-4a30-971b-f449e4469952")
				},
				{
					new Guid("ffd2fff2-df67-4eb5-a3df-00eff2217a5d"),
					"Dexterity",
					"/Dexterity",
					"{\"+\":[{\"var\":\"Dexterity\"},2]}",
					new Guid("7ec8239f-4e59-4b43-89f4-fb0ddb4557ac")
				},
				{
					new Guid("b62241a8-f35c-469b-96fc-54698ab1c0fd"),
					"Intelligence",
					"/Intelligence",
					"{\"+\":[{\"var\":\"Intelligence\"},1]}",
					new Guid("7ec8239f-4e59-4b43-89f4-fb0ddb4557ac")
				}
			});
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			table: "Effects",
			keyColumn: "Id",
			keyValue: new Guid("a5e7765d-b863-4d97-8c59-f8a989f2fb2d"));

		migrationBuilder.DeleteData(
			table: "Effects",
			keyColumn: "Id",
			keyValue: new Guid("0094a7b3-6256-4ef0-8320-966757a96b17"));

		migrationBuilder.DeleteData(
			table: "Effects",
			keyColumn: "Id",
			keyValue: new Guid("ffd2fff2-df67-4eb5-a3df-00eff2217a5d"));

		migrationBuilder.DeleteData(
			table: "Effects",
			keyColumn: "Id",
			keyValue: new Guid("b62241a8-f35c-469b-96fc-54698ab1c0fd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("02c07b82-1b85-8d01-17a3-ad6059be2a1c"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("040f8b33-7816-2809-d3d4-27bb909cae9f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0662da6a-2c32-5b96-1629-6e9e2990dd1d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0a67ae12-6f2a-6632-d014-34b906d460d6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0c756ca3-186c-e45e-7c0d-1a60e00c695f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0cf39ad6-f699-4100-451f-b0891ed4668d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0e5ccfa2-c6ea-b71e-1fd1-51f8e6b03c5c"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("0f0c534c-0cec-22b6-3689-d38d1aa57563"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("2078c40e-26f2-1378-4694-6842dfc18fb8"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("219e7c22-4120-6af1-3988-8f6d39f7866f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("26d22fec-3b3e-a9f9-0194-1c608db03afd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("2e660191-d559-4073-dced-d31fd4dbfb77"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("2e679897-a57e-1b33-aa5b-fb6017fb7ed8"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("339fc1d9-a93c-1289-871a-d799d60f435b"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("39574422-45a0-a101-f864-411580b9b274"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("395e9b08-56f3-05ac-ff5b-1732c4bd86c6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("3e9b3abc-71df-a93b-5fc3-3fd2938bcac6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("407835f0-e26a-b0e2-62b4-073dc7949733"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("409dd28e-d732-df43-f293-b1afa91fcffd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("43ca9579-7bb6-1771-7518-d898709f7343"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("445b309c-0b76-ba5e-7645-a97c8c21d4ad"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("4465c57b-9dc9-a37b-5b44-c6092a1aa41b"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("468e238c-4638-518f-958e-138cc4a9ede6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("46b94046-653e-ba68-d729-b7c170f79da9"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("47a2b377-2c15-fa89-e428-2dcdd3e971e1"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("49d6923b-f032-8caf-86a3-ab2856c99ef7"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("4a00a997-8589-b8fa-c85a-2c83cab2c991"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("4c253150-dab3-2cbd-f603-e6d32a59fb32"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("5204341d-19e5-dfae-ba28-6c42384444a2"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("52ba42a1-1588-fed7-9030-b3c81df7dafd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("54bb7089-f2f2-d59f-adf1-4caeedcaebd0"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("5ae98d2d-890e-8c7c-9cc3-71bea94ed32d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("6045bd2f-6372-8ad2-d4bb-8782d9d23bf1"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("611a627c-9e01-04ce-57ae-431d57b9a3a3"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("614bcfcb-9bb1-328c-444f-6ef318f636dc"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("61e5c296-fd60-4fbd-9f55-8ebfdb52b769"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("66c25304-84ee-919d-bb0a-d724bc291c67"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("68011563-cd65-8124-e48c-63c9ca5c0e3d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("6abed62e-f20a-9b62-390e-cbc87afda984"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("6fbe01ae-aff9-7f7b-e7a3-1048cf1757e7"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("70405667-547f-7e67-38bf-dd272dcd1c73"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("7a85ee12-52fd-44ec-5145-d26ff1d666f8"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("7ccdbc2d-4703-9afb-b7da-fce5848f3266"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("7d8a492d-74fc-6b65-1682-ec6b67341cc5"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("827498dc-e5b3-a662-bbba-4d5d3a81f7ce"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("8abe03dd-90d5-f3fd-191a-6fdb79d5392b"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("8d629047-f174-6932-7eb0-344228d36fbf"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("8ee1c607-8bee-f4a2-e646-9d7a25b7d1a2"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("92d7e066-2654-05c3-3dd8-f268b01a61bd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("92e302a2-0024-e3fb-617d-3573fb9a40bf"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("93035114-7e75-0bb6-868f-70ed6afa4874"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9451839f-762b-7017-e6f3-b61b341f8d4e"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("95f329de-97a2-d27e-51df-d56124162f3d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9a83a195-58f7-9ec3-b320-4e2cd3eca896"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9bb3c3c0-c409-4f01-4bc5-25fc911b298d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9ce72319-67c8-3283-195a-a77e75604dd3"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9f3898d1-93b3-571b-9aad-1f1faf6354ed"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("9f6412af-c38e-54fc-f788-594d28a7faa0"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a08c97f1-064e-efae-af54-028a2bc998b8"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a0a55cb9-e727-b2a2-2a01-95d0fbc7ac7f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a30c6124-3c80-c7b6-b8cb-76fcbb6003a4"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a320752e-964b-47aa-2632-c92ee273dcb5"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a3dea530-55b9-c368-80a6-5310993b5f0a"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a9badbe2-1cb6-5964-5df8-31556b01bcc5"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("a9bfc692-700d-5421-1ddd-c3daacdd4cba"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("afc21a80-d7e4-805b-e154-5789c2517856"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b2b4e2f8-4700-d1e8-9eb9-27b8f22dc7cf"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b4626565-68b9-cf12-5277-064aeaa8716c"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b5d7c833-315c-22ac-2979-b2eee68af468"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b5db03b8-457a-88e4-6b6c-f51ff54b136c"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b6dcaa1f-9317-b646-bad0-b33b7aad0b7d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b6df84c0-0028-51d4-272a-7ed0d648468b"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b79cf1de-c2bc-e136-4f7c-1b46ae5bb33d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("b89e7add-1c12-ae81-1096-51651b82ad8e"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("bb479a6b-2415-faa5-a7fe-2b4152eb9941"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("bb9d2c37-8b9a-b70c-3536-709f1f59e2d1"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c011a956-b1ae-3442-2871-4e5cf68bdebe"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c18934da-bb70-9420-eefa-51a97e9ed873"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c23f6761-c4b1-1eea-40cb-d97b8aef36e3"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c60a3c91-4a4c-8ecb-4f4d-5866fd90afb7"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c671e502-0f6a-05e8-d4fa-8bca29522f0a"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("c94a8e03-e1e8-5171-ccbd-cc38162a85e6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("cb86d966-092b-2dc5-061c-bbfbe30c4cd2"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ceb48326-e397-ce68-0e37-5e6746e443ab"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("cfe900fe-a2c6-a57d-0dbd-0381a64f2189"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("d027fd21-131c-8d0d-8832-479ad74c4da5"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("d1aded39-08dc-d721-6d83-8d38df3c2a03"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("d357a261-55ac-056b-81de-c9be5d5ca66d"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("d91c6346-bc4e-d3f9-70e7-0a9bc861685a"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("d978e583-8ef7-b76b-a692-64cd68b742fb"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ddbb5e37-3bdb-1351-2fce-78159a70e36a"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("e46336b6-1a6a-3e2b-ee0f-ad30c4ef76c6"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ea0568ce-a36d-8975-0897-cee90fc5b54f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ead75642-6a4d-714b-f9d9-25a6ecc7c707"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ec113066-4faf-a494-77ee-6ce1caff9f41"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("f0f197a8-e7fe-0c42-c92b-db9184801228"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("fa827689-c45b-e5c8-254f-ce42a0f763dd"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("fea857ad-024f-13ab-2f1a-052380d3d651"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ff601cfd-2b7e-f617-c8a5-82124b5c328f"));

		migrationBuilder.DeleteData(
			table: "Characters",
			keyColumn: "Id",
			keyValue: new Guid("ffc4233d-1070-33ac-2e47-9c19acf5b7ad"));

		migrationBuilder.DeleteData(
			table: "Features",
			keyColumn: "Id",
			keyValue: new Guid("333bad4f-b10d-4a30-971b-f449e4469952"));

		migrationBuilder.DeleteData(
			table: "Features",
			keyColumn: "Id",
			keyValue: new Guid("7ec8239f-4e59-4b43-89f4-fb0ddb4557ac"));
	}
}
