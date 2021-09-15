using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_platform.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCandidates",
                columns: table => new
                {
                    JobCandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidates", x => x.JobCandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCandidateId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCandidateSkills_JobCandidates_JobCandidateId",
                        column: x => x.JobCandidateId,
                        principalTable: "JobCandidates",
                        principalColumn: "JobCandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCandidateSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "JobCandidates",
                columns: new[] { "JobCandidateId", "ContactNumber", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, "555-789-456", new DateTime(1984, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "leo@email.com", "Leonardo Blue" },
                    { 2, "123-987-654", new DateTime(1990, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "skull1@email.com", "Rick Hunter" },
                    { 3, "111-222-333-444", new DateTime(1965, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@email.com", "John Doe" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Name" },
                values: new object[,]
                {
                    { 1, "Java programming" },
                    { 2, "C# programming" },
                    { 3, "Database design" },
                    { 4, "English language" },
                    { 5, "Russian language" },
                    { 6, "German language" }
                });

            migrationBuilder.InsertData(
                table: "JobCandidateSkills",
                columns: new[] { "Id", "JobCandidateId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 3, 2, 2 },
                    { 4, 2, 3 },
                    { 2, 1, 4 },
                    { 6, 3, 5 },
                    { 5, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateSkills_JobCandidateId",
                table: "JobCandidateSkills",
                column: "JobCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateSkills_SkillId",
                table: "JobCandidateSkills",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCandidateSkills");

            migrationBuilder.DropTable(
                name: "JobCandidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
