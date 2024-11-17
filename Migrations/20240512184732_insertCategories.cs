using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sh_rt.Migrations
{
    public partial class insertCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
                "VALUES ('Básica', 'Versáteis o suficiente para qualquer ocasião. Com cortes simples e cores sólidas.')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
                "VALUES ('Oversized', 'Com modelagens amplas e caimento despojado, proporcionam um visual urbano e contemporâneo.')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
                "VALUES ('Longline', 'Com seu comprimento alongado e cortes diferenciados, adicionam um toque único ao seu estilo. ')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
              "VALUES ('Jersey', 'Feitas de material leve e elástico, proporcionam liberdade de movimento e um caimento impecável. Perfeitas para um dia na cidade ou para curtir um rolê com os amigos.')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
              "VALUES ('Regata', 'Com modelagens atléticas e cortes modernos, são ideais para exibir sua personalidade única e mostrar seu estilo distinto.')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
              "VALUES ('Jaqueta', 'Dos modelos clássicos aos mais arrojados, oferecem estilo e funcionalidade para enfrentar o dia a dia com personalidade.')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
              "VALUES ('Moletom', 'Com estampas irreverentes e detalhes diferenciados, são perfeitos para expressar sua individualidade.')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
