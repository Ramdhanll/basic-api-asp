using Microsoft.EntityFrameworkCore.Migrations;

namespace basic_api.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account",
                column: "NIK",
                principalTable: "Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling",
                column: "NIK",
                principalTable: "Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account",
                column: "NIK",
                principalTable: "Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Role_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Account_NIK",
                table: "Profiling",
                column: "NIK",
                principalTable: "Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
