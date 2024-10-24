using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TgInstanceAgent.Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sent = table.Column<long>(type: "bigint", nullable: false),
                    Received = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemProxies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdInProviderSystem = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Ip = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Host = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ExpirationTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IpVersion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemProxies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    SystemProxyId = table.Column<Guid>(type: "uuid", nullable: true),
                    SystemProxySetTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instances_SystemProxies_SystemProxyId",
                        column: x => x.SystemProxyId,
                        principalTable: "SystemProxies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ForwardEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    For = table.Column<long>(type: "bigint", nullable: false),
                    To = table.Column<long>(type: "bigint", nullable: false),
                    SendCopy = table.Column<bool>(type: "boolean", nullable: false),
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForwardEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForwardEntries_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proxies",
                columns: table => new
                {
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Host = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExpirationTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proxies", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_Proxies_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MessageCount = table.Column<int>(type: "integer", nullable: false),
                    FileDownloadCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_Restrictions_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSettings",
                columns: table => new
                {
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Messages = table.Column<bool>(type: "boolean", nullable: false),
                    Chats = table.Column<bool>(type: "boolean", nullable: false),
                    Users = table.Column<bool>(type: "boolean", nullable: false),
                    Files = table.Column<bool>(type: "boolean", nullable: false),
                    Stories = table.Column<bool>(type: "boolean", nullable: false),
                    Other = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSettings", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_WebhookSettings_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookUrls",
                columns: table => new
                {
                    Url = table.Column<string>(type: "text", nullable: false),
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookUrls", x => new { x.Url, x.InstanceId });
                    table.ForeignKey(
                        name: "FK_WebhookUrls_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForwardEntries_InstanceId",
                table: "ForwardEntries",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Instances_SystemProxyId",
                table: "Instances",
                column: "SystemProxyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_InstanceId_Date",
                table: "Reports",
                columns: new[] { "InstanceId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebhookUrls_InstanceId",
                table: "WebhookUrls",
                column: "InstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForwardEntries");

            migrationBuilder.DropTable(
                name: "Proxies");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "WebhookSettings");

            migrationBuilder.DropTable(
                name: "WebhookUrls");

            migrationBuilder.DropTable(
                name: "Instances");

            migrationBuilder.DropTable(
                name: "SystemProxies");
        }
    }
}
