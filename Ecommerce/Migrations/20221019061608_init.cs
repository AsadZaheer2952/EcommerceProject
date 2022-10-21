using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("512110dd-aadf-4c2b-9f33-7416c5c690fd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("52f11d3f-c89b-4b91-ac0e-77df7d9de35e"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("aed02198-ff2a-47ed-a5cb-a35b0530aa2d"), "abc", "Category_Name", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6198), "Asad", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6199), "Asad", 0, new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6198), "asad" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("b45ea59e-4e10-4eb6-96bb-e51eab9e9c59"), "test", "test", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6191), "Asad", new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6194), "Asad", 0, new DateTime(2022, 10, 19, 6, 16, 7, 951, DateTimeKind.Utc).AddTicks(6193), "asad" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("aed02198-ff2a-47ed-a5cb-a35b0530aa2d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("b45ea59e-4e10-4eb6-96bb-e51eab9e9c59"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("512110dd-aadf-4c2b-9f33-7416c5c690fd"), "abc", "Category_Name", new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6945), "Asad", new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6946), "Asad", 0, new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6945), "asad" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Category_Description", "Category_Name", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("52f11d3f-c89b-4b91-ac0e-77df7d9de35e"), "test", "test", new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6938), "Asad", new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6941), "Asad", 0, new DateTime(2022, 10, 19, 6, 10, 39, 891, DateTimeKind.Utc).AddTicks(6940), "asad" });
        }
    }
}
