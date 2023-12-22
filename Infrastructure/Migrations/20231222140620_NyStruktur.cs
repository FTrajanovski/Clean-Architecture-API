using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class NyStruktur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnimalModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CanFly = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LikesToPlay = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Breed = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAnimals",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AnimalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimals", x => new { x.UserId, x.AnimalId });
                    table.ForeignKey(
                        name: "FK_UserAnimals_AnimalModel_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-5678-1234-567812345612"), true, null, "Bird", "TestBirdForUnitTests" },
                    { new Guid("12345678-1234-5678-1234-567812345613"), true, null, "Bird", "TestDeleteBird" },
                    { new Guid("64074050-608f-44f2-acd8-c41474467eae"), true, null, "Bird", "Tweet" },
                    { new Guid("b42100ee-afde-47cf-bf0b-5e86b972c401"), true, null, "Bird", "Adam" },
                    { new Guid("ffb1249a-89b4-4c55-b9f1-9370d346ca32"), true, null, "Bird", "Perry" }
                });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Discriminator", "LikesToPlay", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-5678-1234-567812345610"), "Cat", true, "TestCatForUnitTests" },
                    { new Guid("12345678-1234-5678-1234-567812345611"), "Cat", true, "TestDeleteCat" },
                    { new Guid("1e442d7f-2de7-4aab-ba2b-d5d1320c67aa"), "Cat", false, "Avocado" },
                    { new Guid("937f58c4-4a03-4852-a0b7-c31b1f9fc0ad"), "Cat", true, "SmallMac" },
                    { new Guid("d9f49e68-6c56-4e77-8a44-9b907c2a9541"), "Cat", true, "Nugget" }
                });

            migrationBuilder.InsertData(
                table: "AnimalModel",
                columns: new[] { "Id", "Breed", "Discriminator", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("0cdc827a-8fb6-4de0-b400-de36d8a15d91"), null, "Dog", "Björn", 0 },
                    { new Guid("12345678-1234-5678-1234-567812345678"), null, "Dog", "TestDogForUnitTests", 0 },
                    { new Guid("12345678-1234-5678-1234-567812345679"), null, "Dog", "TestDeleteDog", 0 },
                    { new Guid("168bfd41-a5f7-41d3-a6fc-8d1a2701fed4"), null, "Dog", "Alfred", 0 },
                    { new Guid("3b4704f8-389a-4637-ab33-4b026500481f"), null, "Dog", "Patrik", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "UserName" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-5678-1234-567812345614"), "", "TestDeleteUser" },
                    { new Guid("19f578f5-1423-4b2a-8aa6-20901e39bc43"), "navjet123", "Navjet" },
                    { new Guid("33a905de-3f68-4bc3-a424-ac900859b8e1"), "Stefan123", "stefan" },
                    { new Guid("774c97d1-f98d-4e5c-940b-9569339024d5"), "Fil123", "rob" },
                    { new Guid("85856ae0-963f-41d1-ba2b-2ead54e58233"), "FindNemo123", "Nemm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimals_AnimalId",
                table: "UserAnimals",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnimals");

            migrationBuilder.DropTable(
                name: "AnimalModel");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
