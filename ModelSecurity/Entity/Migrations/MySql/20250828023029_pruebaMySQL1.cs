using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations.MySql
{
    /// <inheritdoc />
    public partial class pruebaMySQL1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormModule_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormModule_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolFormPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolFormPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Form",
                columns: new[] { "Id", "Active", "Code", "CreateAt", "DeleteAt", "Name" },
                values: new object[,]
                {
                    { 1, true, "FORM_PERSON", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(8636), null, "Formulario Persona" },
                    { 2, true, "FORM_USER", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9029), null, "Formulario Usuario" },
                    { 3, true, "FORM_ROL", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9032), null, "Formulario Rol" },
                    { 4, true, "FORM_PERMISSION", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9033), null, "Formulario Permiso" },
                    { 5, true, "FORM_MODULE", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9035), null, "Formulario Módulo" },
                    { 6, true, "FORM_ROLUSER", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9036), null, "Formulario RolUsuario" },
                    { 7, true, "FORM_ROLFORMPERM", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(9038), null, "Formulario RolPermiso" }
                });

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "Active", "CreateAt", "DeleteAt", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(6443), null, "Seguridad" },
                    { 2, true, new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(6834), null, "Gestión de Usuarios" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Active", "Code", "CreateAt", "DeleteAt", "Name" },
                values: new object[,]
                {
                    { 1, true, "PERM_ADD", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(4584), null, "Agregar" },
                    { 2, true, "PERM_EDIT", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(4990), null, "Editar" },
                    { 3, true, "PERM_DELETE", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(4993), null, "Eliminar" },
                    { 4, true, "PERM_DEACTIVATE", new DateTime(2025, 8, 27, 21, 30, 28, 445, DateTimeKind.Local).AddTicks(4995), null, "Desactivar" }
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "Id", "Active", "CreateAt", "DeleteAt", "Description", "Name" },
                values: new object[] { 1, true, new DateTime(2025, 8, 27, 21, 30, 28, 444, DateTimeKind.Local).AddTicks(5272), null, "Administrador del sistema con todos los privilegios", "Admin" });

            migrationBuilder.InsertData(
                table: "FormModule",
                columns: new[] { "Id", "FormId", "ModuleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormModule_FormId",
                table: "FormModule",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormModule_ModuleId",
                table: "FormModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermission_FormId",
                table: "RolFormPermission",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermission_PermissionId",
                table: "RolFormPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolFormPermission_RolId",
                table: "RolFormPermission",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_RolId",
                table: "RolUser",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_UserId",
                table: "RolUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PersonId",
                table: "User",
                column: "PersonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormModule");

            migrationBuilder.DropTable(
                name: "RolFormPermission");

            migrationBuilder.DropTable(
                name: "RolUser");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
