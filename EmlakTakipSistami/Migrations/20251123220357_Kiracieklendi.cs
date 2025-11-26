using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakTakipSistami.Migrations
{
    /// <inheritdoc />
    public partial class Kiracieklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kiracilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaireId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiracilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kiracilar_Daireler_DaireId",
                        column: x => x.DaireId,
                        principalTable: "Daireler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kiracilar_DaireId",
                table: "Kiracilar",
                column: "DaireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kiracilar");
        }
    }
}
