using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystem.Migrations
{
    public partial class PatientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Patients",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
        Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
        ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
        MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
        Invalidated = table.Column<int>(type: "int", nullable: false),
        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
        ModifiedBy = table.Column<int>(type: "int", nullable: false),
        // Additional column for navigation property
        PatientId = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Patients", x => x.Id);
    });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
