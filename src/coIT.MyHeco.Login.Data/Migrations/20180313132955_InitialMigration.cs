using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coIT.MyHeco.Login.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Benutzer",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false, defaultValue: "MyHecoBenutzer"),
                    WrongLogins = table.Column<int>(nullable: false, defaultValue: 0),
                    Firma_Name = table.Column<string>(nullable: true),
                    LoginInformation_Email = table.Column<string>(nullable: true),
                    LoginInformation_Passwort = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Benutzer", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Benutzer");
        }
    }
}