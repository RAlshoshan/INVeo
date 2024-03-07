using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVeo.Data.Migrations
{
    public partial class editIdeaModelFixedObjectError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Idea",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Brief",
                table: "Idea",
                newName: "SmartContractAddress");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Idea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdeaId",
                table: "Idea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Idea",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Idea",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Idea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Idea");

            migrationBuilder.DropColumn(
                name: "IdeaId",
                table: "Idea");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Idea");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Idea");

            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Idea");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Idea",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SmartContractAddress",
                table: "Idea",
                newName: "Brief");
        }
    }
}
