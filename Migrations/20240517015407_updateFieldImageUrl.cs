using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sh_rt.Migrations
{
    public partial class updateFieldImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Shirts",
                newName: "FrontShirtImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BehindShirtImageUrl",
                table: "Shirts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BehindShirtImageUrl",
                table: "Shirts");

            migrationBuilder.RenameColumn(
                name: "FrontShirtImageUrl",
                table: "Shirts",
                newName: "Image");
        }
    }
}
