using Microsoft.EntityFrameworkCore.Migrations;

namespace XiechengTravel.Migrations
{
    public partial class InitializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "308660dc-ae51-480f-824d-7dca6714c3e2",
                column: "ConcurrencyStamp",
                value: "e2c6c72d-c6d5-4ebc-a725-248096d7372c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b056d988-cc22-42d6-909d-789fd8dabe8a", "AQAAAAEAACcQAAAAELNYHcTCVGSc1dhhyH/d+NfQRe6QjgpYfEZ9L5CuTwpfozSQ/0TeGE6D8fl6K4mdYw==", "9c3dbb16-3640-4e76-8bcc-b79f39402786" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "308660dc-ae51-480f-824d-7dca6714c3e2",
                column: "ConcurrencyStamp",
                value: "78db1274-b8cf-4de9-9f7f-19ff43e4f698");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "047bdcc5-30f7-4e79-b04b-39e422fb021f", "AQAAAAEAACcQAAAAEKorVj3394Y5VrjREJZFix9Qmn8Gg4wu4sBefoUAYR98sboWIQFnbEyP2qure8y+VA==", "15a6a37b-b6f9-4085-a2cd-d31264db0f1d" });
        }
    }
}
