using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoSommarProjekt.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyInToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "ToDoItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "ToDoItems");
        }
    }
}
