using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_Produccion.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", null, "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "26468881-d870-4359-9564-63c707037db8", 0, "7a4e67af-b207-45b7-9f74-aaf9ae2120aa", "ApplicationUser", "Rcairo09@gmail.com", true, "DefaultFirstName", "DefaultLastName", false, null, "RCAIRO09@GMAIL.COM", "RCAIRO09@GMAIL.COM", "AQAAAAIAAYagAAAAEMBZ9H/lJjUmokD9ZLMf1XaE5Io/0/pdPSrifWLVXbf2O5ZLU7sAdo80O5uMReSBsg==", null, false, "80da34a8-4ef3-4f9a-b972-a2caf902f690", false, "Rcairo09@gmail.com" },
                    { "ae1d8f38-b4e4-4447-a056-74f1cddcf05f", 0, "263e6a42-2f3a-41c4-bc8a-6eb9f659ba51", "ApplicationUser", "My10@gmail.com", true, "DefaultFirstName", "DefaultLastName", false, null, "MY10@GMAIL.COM", "MY10@GMAIL.COM", "AQAAAAIAAYagAAAAEGWervxYfBnOaWFsUM306/dPh0Z8k/2seYM8JGA7E+5GXnTvO5LTfUuXFdupOR6Tkw==", null, false, "3e8c8868-b5ef-482c-959a-ee3cfc9701c8", false, "My10@gmail.com" },
                    { "f1b23f68-d05e-4cdf-8279-00bdf6a12a16", 0, "47d15a52-514b-476b-9b84-2a8afb00da46", "ApplicationUser", "Oteroanielka7@gmail.com", true, "DefaultFirstName", "DefaultLastName", false, null, "OTEROANIELKA7@GMAIL.COM", "OTEROANIELKA7@GMAIL.COM", "AQAAAAIAAYagAAAAEPCcFga5NcYX4HCMeLq902sjJiSrcb/nWu/pyCtLfy5xGkDmhlbfHGHyqZKUO+U0nA==", null, false, "4150f83b-22f2-4116-af6c-94e1aabc2b47", false, "Oteroanielka7@gmail.com" },
                    { "fba3449f-716a-4da4-9852-7144c3fe303a", 0, "15b2b2a0-0ffb-4b5a-a6b3-af480f083a34", "ApplicationUser", "martinezjohnny324@gmail.com", true, "Johnny", "Eduardo", false, null, "MARTINEZJOHNNY324@GMAIL.COM", "MARTINEZJOHNNY324@GMAIL.COM", "AQAAAAIAAYagAAAAECaS1D88i3mx5bTn0c1l1gR79Z0Isw6U7J75Iyoq4oTEBuZDBlACOgX9LPv/zlAxOw==", null, false, "5af3f4a0-14d7-4fc5-9ece-2dc1f95a8291", false, "martinezjohnny324@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "26468881-d870-4359-9564-63c707037db8" },
                    { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "ae1d8f38-b4e4-4447-a056-74f1cddcf05f" },
                    { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "f1b23f68-d05e-4cdf-8279-00bdf6a12a16" },
                    { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "fba3449f-716a-4da4-9852-7144c3fe303a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "26468881-d870-4359-9564-63c707037db8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "ae1d8f38-b4e4-4447-a056-74f1cddcf05f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "f1b23f68-d05e-4cdf-8279-00bdf6a12a16" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c21468e1-6e45-4ed2-ad0b-1e440ba73d64", "fba3449f-716a-4da4-9852-7144c3fe303a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c21468e1-6e45-4ed2-ad0b-1e440ba73d64");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26468881-d870-4359-9564-63c707037db8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ae1d8f38-b4e4-4447-a056-74f1cddcf05f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1b23f68-d05e-4cdf-8279-00bdf6a12a16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fba3449f-716a-4da4-9852-7144c3fe303a");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
