using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INVeo.Data.Migrations
{
    public partial class addToIdeaModelIdeaFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdeaFile",
                table: "Idea",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdeaFile",
                table: "Idea");
        }
    }
}
