using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakTakipSistami.Migrations
{
    /// <inheritdoc />
    public partial class DaireAdiEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DaireAdi",
                table: "Daireler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaireAdi",
                table: "Daireler");
        }
    }
}
