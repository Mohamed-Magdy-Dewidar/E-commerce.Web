using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Editing_FistrName_To_FirstName_in_Addresses_IdentityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "Addresses",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Addresses",
                newName: "FisrtName");
        }
    }
}
