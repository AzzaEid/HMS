using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class edits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_ManagerDoctorId",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerDoctorId",
                table: "Department",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerDoctorId",
                table: "Department",
                column: "ManagerDoctorId",
                unique: true,
                filter: "[ManagerDoctorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_ManagerDoctorId",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerDoctorId",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerDoctorId",
                table: "Department",
                column: "ManagerDoctorId",
                unique: true);
        }
    }
}
