using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Health.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDietEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Calories = table.Column<double>(type: "double precision", nullable: false),
                    Fats = table.Column<double>(type: "double precision", nullable: false),
                    Proteins = table.Column<double>(type: "double precision", nullable: false),
                    Carbohydrates = table.Column<double>(type: "double precision", nullable: false),
                    DietId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietEvents_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietEvents_DietId",
                table: "DietEvents",
                column: "DietId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietEvents");
        }
    }
}
