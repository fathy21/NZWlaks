using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seeddatafordifficultyandregionmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a596bb82-f61a-40bd-a7c2-8b14e764aa6d"), "Easy" },
                    { new Guid("e362b264-a682-48fb-8fe6-f8eb914134ba"), "Medium" },
                    { new Guid("f9010147-e264-40f8-ace3-5672565b1c6e"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("29cf8535-ee40-4002-9eae-39a6172e8637"), "NTH", "Northland", "NTH-image.jph" },
                    { new Guid("370bbc1c-addb-4128-91c3-e90f4b4b1c99"), "AUK", "Auckland", "AUK-image.jph" },
                    { new Guid("4edb47c8-3f14-4f67-b8ab-6a7328b5f145"), "QST", "QueensTown", "QST-image.jph" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a596bb82-f61a-40bd-a7c2-8b14e764aa6d"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e362b264-a682-48fb-8fe6-f8eb914134ba"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f9010147-e264-40f8-ace3-5672565b1c6e"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("29cf8535-ee40-4002-9eae-39a6172e8637"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("370bbc1c-addb-4128-91c3-e90f4b4b1c99"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("4edb47c8-3f14-4f67-b8ab-6a7328b5f145"));
        }
    }
}
