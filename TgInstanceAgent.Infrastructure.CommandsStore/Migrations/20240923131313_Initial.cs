using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TgInstanceAgent.Infrastructure.CommandsStore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.CommandId);
                });

            migrationBuilder.CreateTable(
                name: "AddForwardEntryCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    For = table.Column<long>(type: "bigint", nullable: false),
                    To = table.Column<long>(type: "bigint", nullable: false),
                    SendCopy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddForwardEntryCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_AddForwardEntryCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddInstanceCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddInstanceCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_AddInstanceCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddWebhookUrlCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddWebhookUrlCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_AddWebhookUrlCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoveForwardEntryCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    For = table.Column<long>(type: "bigint", nullable: false),
                    To = table.Column<long>(type: "bigint", nullable: false),
                    SendCopy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoveForwardEntryCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_RemoveForwardEntryCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoveWebhookUrlCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoveWebhookUrlCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_RemoveWebhookUrlCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StartInstanceCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartInstanceCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_StartInstanceCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StopInstanceCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopInstanceCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_StopInstanceCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpdateWebhookSettingCommands",
                columns: table => new
                {
                    CommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Messages = table.Column<bool>(type: "boolean", nullable: false),
                    Chats = table.Column<bool>(type: "boolean", nullable: false),
                    Users = table.Column<bool>(type: "boolean", nullable: false),
                    Files = table.Column<bool>(type: "boolean", nullable: false),
                    Stories = table.Column<bool>(type: "boolean", nullable: false),
                    Other = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateWebhookSettingCommands", x => x.CommandId);
                    table.ForeignKey(
                        name: "FK_UpdateWebhookSettingCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "CommandId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddForwardEntryCommands");

            migrationBuilder.DropTable(
                name: "AddInstanceCommands");

            migrationBuilder.DropTable(
                name: "AddWebhookUrlCommands");

            migrationBuilder.DropTable(
                name: "RemoveForwardEntryCommands");

            migrationBuilder.DropTable(
                name: "RemoveWebhookUrlCommands");

            migrationBuilder.DropTable(
                name: "StartInstanceCommands");

            migrationBuilder.DropTable(
                name: "StopInstanceCommands");

            migrationBuilder.DropTable(
                name: "UpdateWebhookSettingCommands");

            migrationBuilder.DropTable(
                name: "Commands");
        }
    }
}
