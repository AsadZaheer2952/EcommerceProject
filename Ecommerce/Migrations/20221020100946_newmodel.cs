using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    public partial class newmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("aed02198-ff2a-47ed-a5cb-a35b0530aa2d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b45ea59e-4e10-4eb6-96bb-e51eab9e9c59"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("6219b919-5dd7-4b0a-95cf-b5fe6c51234b"), "test", "test", new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6173), "Asad", new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6180), "Asad", 0, new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6178), "asad" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("ab472cd7-ac7d-4e48-a28c-7a0ce6fa847d"), "abc", "Category_Name", new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6186), "Asad", new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6188), "Asad", 0, new DateTime(2022, 10, 20, 10, 9, 45, 742, DateTimeKind.Utc).AddTicks(6187), "asad" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("6219b919-5dd7-4b0a-95cf-b5fe6c51234b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("ab472cd7-ac7d-4e48-a28c-7a0ce6fa847d"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("aed02198-ff2a-47ed-a5cb-a35b0530aa2d"), "abc", "Category_Name", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6198), "Asad", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6199), "Asad", 0, new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6198), "asad" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("b45ea59e-4e10-4eb6-96bb-e51eab9e9c59"), "test", "test", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6191), "Asad", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6194), "Asad", 0, new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6193), "asad" });
        }
    }
}
