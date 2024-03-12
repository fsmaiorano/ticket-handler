using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sectors_Holders_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Holders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    HolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Holders_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Holders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    priority = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    holder_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sector_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assignee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Holders_holder_id",
                        column: x => x.holder_id,
                        principalTable: "Holders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Priorities_priority",
                        column: x => x.priority,
                        principalTable: "Priorities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Sectors_sector_id",
                        column: x => x.sector_id,
                        principalTable: "Sectors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Statuses_status",
                        column: x => x.status,
                        principalTable: "Statuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserSectors",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSectors", x => new { x.UserId, x.SectorId });
                    table.ForeignKey(
                        name: "FK_UserSectors_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserSectors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    holder_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sector_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Answers_Tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "Tickets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_id",
                table: "Answers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ticket_id",
                table: "Answers",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_Holders_id",
                table: "Holders",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_id",
                table: "Priorities",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_HolderId",
                table: "Sectors",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_id",
                table: "Sectors",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_id",
                table: "Statuses",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_assignee_id",
                table: "Tickets",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_holder_id",
                table: "Tickets",
                column: "holder_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_id",
                table: "Tickets",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_priority",
                table: "Tickets",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_sector_id",
                table: "Tickets",
                column: "sector_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_status",
                table: "Tickets",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_user_id",
                table: "Tickets",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HolderId",
                table: "Users",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_id",
                table: "Users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSectors_SectorId",
                table: "UserSectors",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSectors_UserId",
                table: "UserSectors",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "UserSectors");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Holders");
        }
    }
}
