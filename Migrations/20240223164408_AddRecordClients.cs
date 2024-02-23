using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Name", "Order", "PhoneNumber", "UpdatedAt" },
                values: new object[] { 3, "Ruisseau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Client3@outlook.fr", "Client3", "Basket", "0999999999", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
