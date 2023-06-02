using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wed_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CT_Hangs_PhanPhims_PhanPhimId",
                table: "CT_Hangs");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_Hangs_Phims_IdPhim",
                table: "CT_Hangs");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_TheLoais_PhanPhims_PhanPhimId",
                table: "CT_TheLoais");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_TheLoais_Phims_IdPhim",
                table: "CT_TheLoais");

            migrationBuilder.DropIndex(
                name: "IX_CT_TheLoais_PhanPhimId",
                table: "CT_TheLoais");

            migrationBuilder.DropIndex(
                name: "IX_CT_Hangs_PhanPhimId",
                table: "CT_Hangs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Phims");

            migrationBuilder.DropColumn(
                name: "PhanPhimId",
                table: "CT_TheLoais");

            migrationBuilder.DropColumn(
                name: "PhanPhimId",
                table: "CT_Hangs");

            migrationBuilder.RenameColumn(
                name: "IdPhim",
                table: "CT_TheLoais",
                newName: "IdPhanPhim");

            migrationBuilder.RenameColumn(
                name: "IdPhim",
                table: "CT_Hangs",
                newName: "IdPhanPhim");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_Hangs_PhanPhims_IdPhanPhim",
                table: "CT_Hangs",
                column: "IdPhanPhim",
                principalTable: "PhanPhims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CT_TheLoais_PhanPhims_IdPhanPhim",
                table: "CT_TheLoais",
                column: "IdPhanPhim",
                principalTable: "PhanPhims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CT_Hangs_PhanPhims_IdPhanPhim",
                table: "CT_Hangs");

            migrationBuilder.DropForeignKey(
                name: "FK_CT_TheLoais_PhanPhims_IdPhanPhim",
                table: "CT_TheLoais");

            migrationBuilder.RenameColumn(
                name: "IdPhanPhim",
                table: "CT_TheLoais",
                newName: "IdPhim");

            migrationBuilder.RenameColumn(
                name: "IdPhanPhim",
                table: "CT_Hangs",
                newName: "IdPhim");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Phims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanPhimId",
                table: "CT_TheLoais",
                type: "nvarchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhanPhimId",
                table: "CT_Hangs",
                type: "nvarchar(128)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CT_TheLoais_PhanPhimId",
                table: "CT_TheLoais",
                column: "PhanPhimId");

            migrationBuilder.CreateIndex(
                name: "IX_CT_Hangs_PhanPhimId",
                table: "CT_Hangs",
                column: "PhanPhimId");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_Hangs_PhanPhims_PhanPhimId",
                table: "CT_Hangs",
                column: "PhanPhimId",
                principalTable: "PhanPhims",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_Hangs_Phims_IdPhim",
                table: "CT_Hangs",
                column: "IdPhim",
                principalTable: "Phims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CT_TheLoais_PhanPhims_PhanPhimId",
                table: "CT_TheLoais",
                column: "PhanPhimId",
                principalTable: "PhanPhims",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CT_TheLoais_Phims_IdPhim",
                table: "CT_TheLoais",
                column: "IdPhim",
                principalTable: "Phims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
