using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedUserAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_AnimalModel_AnimalModelId",
                table: "UserAnimals");

            migrationBuilder.RenameColumn(
                name: "AnimalModelId",
                table: "UserAnimals",
                newName: "DogId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimals_AnimalModelId",
                table: "UserAnimals",
                newName: "IX_UserAnimals_DogId");

            migrationBuilder.AddColumn<Guid>(
                name: "BirdId",
                table: "UserAnimals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CatId",
                table: "UserAnimals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimals_BirdId",
                table: "UserAnimals",
                column: "BirdId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimals_CatId",
                table: "UserAnimals",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_AnimalModel_BirdId",
                table: "UserAnimals",
                column: "BirdId",
                principalTable: "AnimalModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_AnimalModel_CatId",
                table: "UserAnimals",
                column: "CatId",
                principalTable: "AnimalModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_AnimalModel_DogId",
                table: "UserAnimals",
                column: "DogId",
                principalTable: "AnimalModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_AnimalModel_BirdId",
                table: "UserAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_AnimalModel_CatId",
                table: "UserAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_AnimalModel_DogId",
                table: "UserAnimals");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimals_BirdId",
                table: "UserAnimals");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimals_CatId",
                table: "UserAnimals");

            migrationBuilder.DropColumn(
                name: "BirdId",
                table: "UserAnimals");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "UserAnimals");

            migrationBuilder.RenameColumn(
                name: "DogId",
                table: "UserAnimals",
                newName: "AnimalModelId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimals_DogId",
                table: "UserAnimals",
                newName: "IX_UserAnimals_AnimalModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_AnimalModel_AnimalModelId",
                table: "UserAnimals",
                column: "AnimalModelId",
                principalTable: "AnimalModel",
                principalColumn: "Id");
        }
    }
}
