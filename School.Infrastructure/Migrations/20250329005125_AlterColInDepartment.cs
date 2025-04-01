using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AlterColInDepartment : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "DepartmentName",
				table: "Departments",
				newName: "Name");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Name",
				table: "Departments",
				newName: "DepartmentName");
		}
	}
}
