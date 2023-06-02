using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wed_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updatetapphim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "TapPhims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdPhanPhim",
                table: "TapPhims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeUpdate",
                table: "TapPhims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "TapPhims");

            migrationBuilder.DropColumn(
                name: "IdPhanPhim",
                table: "TapPhims");

            migrationBuilder.DropColumn(
                name: "TimeUpdate",
                table: "TapPhims");
        }
    }
}
