using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sh_rt.Migrations
{
    public partial class AlterarNomeDoCampoDeDataDeCriacaoDoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveredAt",
                table: "Orders",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "DeliveredAt");
        }
    }
}
