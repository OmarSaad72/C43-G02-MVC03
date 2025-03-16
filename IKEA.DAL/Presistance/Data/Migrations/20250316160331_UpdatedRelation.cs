using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKEA.DAL.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentDeptId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DeptManageId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DeptManageId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeptManageId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentDeptId",
                table: "Employees",
                column: "DepartmentDeptId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentDeptId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "DeptManageId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptManageId",
                table: "Employees",
                column: "DeptManageId",
                unique: true,
                filter: "[DeptManageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentDeptId",
                table: "Employees",
                column: "DepartmentDeptId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DeptManageId",
                table: "Employees",
                column: "DeptManageId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
