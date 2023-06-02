using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wed_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updaterror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TapPhims_PhanPhims_PhanPhimId",
                table: "TapPhims");

            migrationBuilder.RenameColumn(
                name: "PhanPhimId",
                table: "TapPhims",
                newName: "IdPhanPhim");

            migrationBuilder.RenameIndex(
                name: "IX_TapPhims_PhanPhimId",
                table: "TapPhims",
                newName: "IX_TapPhims_IdPhanPhim");

            migrationBuilder.AddForeignKey(
                name: "FK_TapPhims_PhanPhims_IdPhanPhim",
                table: "TapPhims",
                column: "IdPhanPhim",
                principalTable: "PhanPhims",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TapPhims_PhanPhims_IdPhanPhim",
                table: "TapPhims");

            migrationBuilder.RenameColumn(
                name: "IdPhanPhim",
                table: "TapPhims",
                newName: "PhanPhimId");

            migrationBuilder.RenameIndex(
                name: "IX_TapPhims_IdPhanPhim",
                table: "TapPhims",
                newName: "IX_TapPhims_PhanPhimId");

            migrationBuilder.AddForeignKey(
                name: "FK_TapPhims_PhanPhims_PhanPhimId",
                table: "TapPhims",
                column: "PhanPhimId",
                principalTable: "PhanPhims",
                principalColumn: "Id");
        }
    }
}
