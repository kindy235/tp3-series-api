using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeriesApi.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "t_e_serie_ser",
                schema: "public",
                columns: table => new
                {
                    ser_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ser_titre = table.Column<string>(type: "varchar(100)", nullable: false),
                    ser_resume = table.Column<string>(type: "TEXT", nullable: true),
                    ser_nbsaisons = table.Column<int>(type: "integer", nullable: true),
                    ser_nbepisodes = table.Column<int>(type: "integer", nullable: true),
                    ser_anneecreation = table.Column<int>(type: "integer", nullable: true),
                    ser_network = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ser", x => x.ser_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_utilisateur_utl",
                schema: "public",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utl_nom = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_prenom = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_mobile = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: true),
                    utl_mail = table.Column<string>(type: "varchar(100)", nullable: false),
                    utl_pwd = table.Column<string>(type: "varchar(64)", nullable: false),
                    utl_rue = table.Column<string>(type: "varchar(200)", nullable: true),
                    utl_cp = table.Column<string>(type: "char(5)", maxLength: 5, nullable: true),
                    utl_ville = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_pays = table.Column<string>(type: "varchar(50)", nullable: true, defaultValue: "France"),
                    utl_latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    utl_longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    utl_datecreation = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utl", x => x.utl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                schema: "public",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false),
                    ser_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not", x => new { x.utl_id, x.ser_id });
                    table.CheckConstraint("ck_not_note", "not_note between 0 and 5");
                    table.ForeignKey(
                        name: "fk_not_ser",
                        column: x => x.ser_id,
                        principalSchema: "public",
                        principalTable: "t_e_serie_ser",
                        principalColumn: "ser_id");
                    table.ForeignKey(
                        name: "fk_not_utl",
                        column: x => x.utl_id,
                        principalSchema: "public",
                        principalTable: "t_e_utilisateur_utl",
                        principalColumn: "utl_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_utilisateur_utl_utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                column: "utl_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_ser_id",
                schema: "public",
                table: "t_j_notation_not",
                column: "ser_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_notation_not",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_serie_ser",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_utilisateur_utl",
                schema: "public");
        }
    }
}
