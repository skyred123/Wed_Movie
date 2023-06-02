using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wed_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updatetapphim1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPhanPhim",
                table: "TapPhims");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdPhanPhim",
                table: "TapPhims",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
