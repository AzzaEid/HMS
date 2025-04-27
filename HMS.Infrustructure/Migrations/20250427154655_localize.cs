using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class localize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patients",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Doctors",
                newName: "NameEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Patients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Doctors",
                newName: "Name");
        }
    }
}
