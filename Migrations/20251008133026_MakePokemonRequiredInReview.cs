using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewer.Migrations
{
    /// <inheritdoc />
    public partial class MakePokemonRequiredInReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id");
        }
    }
}
