using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraClone.Migrations
{
    /// <inheritdoc />
    public partial class changedAsigneeToAssignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Accounts_AsigneeId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "AsigneeId",
                table: "Tickets",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_AsigneeId",
                table: "Tickets",
                newName: "IX_Tickets_AssigneeId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTimestamp",
                value: 1700984308L);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Accounts_AssigneeId",
                table: "Tickets",
                column: "AssigneeId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Accounts_AssigneeId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Tickets",
                newName: "AsigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_AssigneeId",
                table: "Tickets",
                newName: "IX_Tickets_AsigneeId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTimestamp",
                value: 1700769058L);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Accounts_AsigneeId",
                table: "Tickets",
                column: "AsigneeId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
