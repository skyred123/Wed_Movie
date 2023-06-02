using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wed_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updatetimeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeUpdate",
                table: "PhanPhims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeUpdate",
                table: "PhanPhims");
        }
    }
}
