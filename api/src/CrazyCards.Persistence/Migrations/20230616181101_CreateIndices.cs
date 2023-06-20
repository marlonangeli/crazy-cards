using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Player_Email",
                table: "Player",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_Username",
                table: "Player",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hero_Name",
                table: "Hero",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Class_Name",
                table: "Class",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Player_Email",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_Username",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Hero_Name",
                table: "Hero");

            migrationBuilder.DropIndex(
                name: "IX_Class_Name",
                table: "Class");
        }
    }
}
