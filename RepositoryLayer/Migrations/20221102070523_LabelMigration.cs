using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LableTable_NotesTable_NoteId",
                table: "LableTable");

            migrationBuilder.DropForeignKey(
                name: "FK_LableTable_Usertable_UserId",
                table: "LableTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LableTable",
                table: "LableTable");

            migrationBuilder.RenameTable(
                name: "LableTable",
                newName: "LabelTable");

            migrationBuilder.RenameIndex(
                name: "IX_LableTable_UserId",
                table: "LabelTable",
                newName: "IX_LabelTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LableTable_NoteId",
                table: "LabelTable",
                newName: "IX_LabelTable_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelTable",
                table: "LabelTable",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTable_NotesTable_NoteId",
                table: "LabelTable",
                column: "NoteId",
                principalTable: "NotesTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTable_Usertable_UserId",
                table: "LabelTable",
                column: "UserId",
                principalTable: "Usertable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelTable_NotesTable_NoteId",
                table: "LabelTable");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelTable_Usertable_UserId",
                table: "LabelTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelTable",
                table: "LabelTable");

            migrationBuilder.RenameTable(
                name: "LabelTable",
                newName: "LableTable");

            migrationBuilder.RenameIndex(
                name: "IX_LabelTable_UserId",
                table: "LableTable",
                newName: "IX_LableTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LabelTable_NoteId",
                table: "LableTable",
                newName: "IX_LableTable_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LableTable",
                table: "LableTable",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LableTable_NotesTable_NoteId",
                table: "LableTable",
                column: "NoteId",
                principalTable: "NotesTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LableTable_Usertable_UserId",
                table: "LableTable",
                column: "UserId",
                principalTable: "Usertable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
