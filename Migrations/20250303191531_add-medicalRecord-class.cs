using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addmedicalRecordclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescription_Prescriptions_PrescriptionsPrescriptionID",
                table: "MedicationPrescription");

            migrationBuilder.RenameColumn(
                name: "PrescriptionDate",
                table: "Prescriptions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "PrescriptionID",
                table: "Prescriptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PrescriptionsPrescriptionID",
                table: "MedicationPrescription",
                newName: "PrescriptionsId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationPrescription_PrescriptionsPrescriptionID",
                table: "MedicationPrescription",
                newName: "IX_MedicationPrescription_PrescriptionsId");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Appointments",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescription_Prescriptions_PrescriptionsId",
                table: "MedicationPrescription",
                column: "PrescriptionsId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPrescription_Prescriptions_PrescriptionsId",
                table: "MedicationPrescription");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Prescriptions",
                newName: "PrescriptionDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prescriptions",
                newName: "PrescriptionID");

            migrationBuilder.RenameColumn(
                name: "PrescriptionsId",
                table: "MedicationPrescription",
                newName: "PrescriptionsPrescriptionID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationPrescription_PrescriptionsId",
                table: "MedicationPrescription",
                newName: "IX_MedicationPrescription_PrescriptionsPrescriptionID");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "AppointmentDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Appointments",
                newName: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPrescription_Prescriptions_PrescriptionsPrescriptionID",
                table: "MedicationPrescription",
                column: "PrescriptionsPrescriptionID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
