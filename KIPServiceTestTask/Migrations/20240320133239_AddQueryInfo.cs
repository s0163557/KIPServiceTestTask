using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KIPServiceTestTask.Migrations
{
    /// <inheritdoc />
    public partial class AddQueryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueryInfos",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from = table.Column<DateOnly>(type: "date", nullable: false),
                    timer = table.Column<DateTime>(type: "datetime2", nullable: false),
                    to = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryInfos", x => x.guid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryInfos");
        }
    }
}
