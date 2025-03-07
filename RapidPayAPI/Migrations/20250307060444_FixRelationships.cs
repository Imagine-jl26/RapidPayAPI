using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPayAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_CardToken",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardToken",
                table: "Cards",
                column: "CardToken",
                unique: true);
        }
    }
}
