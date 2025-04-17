using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFoodCom.Migrations
{
    /// <inheritdoc />
    public partial class changePasswordLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Clientes");
        }
    }
}
