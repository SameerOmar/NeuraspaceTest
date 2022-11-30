using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NeuraspaceTest.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operatorname = table.Column<string>(name: "operator_name", type: "text", nullable: false),
                    operatorid = table.Column<string>(name: "operator_id", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    satellitename = table.Column<string>(name: "satellite_name", type: "text", nullable: false),
                    operatorid = table.Column<int>(name: "operator_id", type: "integer", nullable: false),
                    satelliteid = table.Column<string>(name: "satellite_id", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satellites_Operators_operator_id",
                        column: x => x.operatorid,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollisionEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false),
                    chaserobjectid = table.Column<string>(name: "chaser_object_id", type: "text", nullable: false),
                    collisiondate = table.Column<DateTime>(name: "collision_date", type: "timestamp with time zone", nullable: false),
                    eventid = table.Column<string>(name: "event_id", type: "text", nullable: false),
                    messageid = table.Column<string>(name: "message_id", type: "text", nullable: false),
                    operatorid = table.Column<int>(name: "operator_id", type: "integer", nullable: false),
                    probabilityofcollision = table.Column<double>(name: "probability_of_collision", type: "double precision", nullable: false),
                    satelliteid = table.Column<int>(name: "satellite_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollisionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollisionEvents_Operators_operator_id",
                        column: x => x.operatorid,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollisionEvents_Satellites_satellite_id",
                        column: x => x.satelliteid,
                        principalTable: "Satellites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollisionEvents_message_id",
                table: "CollisionEvents",
                column: "message_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollisionEvents_operator_id",
                table: "CollisionEvents",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "IX_CollisionEvents_satellite_id",
                table: "CollisionEvents",
                column: "satellite_id");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_operator_id",
                table: "Operators",
                column: "operator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Satellites_operator_id",
                table: "Satellites",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Satellites_satellite_id",
                table: "Satellites",
                column: "satellite_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollisionEvents");

            migrationBuilder.DropTable(
                name: "Satellites");

            migrationBuilder.DropTable(
                name: "Operators");
        }
    }
}
