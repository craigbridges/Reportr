using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Reportr.Registration.Entity.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var options = ReportrDbContext.CurrentContextOptions;
            Validate.IsNotNull(options);

            using (var context = new ReportrDbContext(options))
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText =
                        @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN (
                            'RegisteredReports'
                            ,'ReportCategories'
                            ,'ReportRoleAssignments'
                            ,'ReportParameterConstraints'
                            ,'ReportCategoryAssignments'
                            ,'RegisteredReportSourceRevisions'
                            ,'ReportRoles'
                        )";

                    context.Database.OpenConnection();

                    var count = (int)command.ExecuteScalar();

                    if (count == 7)
                    {
                        return;
                    }
                }
            }

            migrationBuilder.CreateTable(
                name: "RegisteredReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SourceType = table.Column<int>(nullable: false),
                    BuilderTypeName = table.Column<string>(nullable: true),
                    BuilderTypeFullName = table.Column<string>(nullable: true),
                    BuilderTypeAssemblyQualifiedName = table.Column<string>(nullable: true),
                    BuilderAssemblyName = table.Column<string>(nullable: true),
                    BuilderAssemblyLocation = table.Column<string>(nullable: true),
                    ScriptSourceCode = table.Column<string>(nullable: true),
                    DateSourceSpecified = table.Column<DateTime>(nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    DateDisabled = table.Column<DateTime>(nullable: true),
                    DateEnabled = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ParentCategoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCategories_ReportCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportRoleAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ReportName = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRoleAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredReportSourceRevisions",
                columns: table => new
                {
                    RevisionId = table.Column<Guid>(nullable: false),
                    ReportId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    DateOriginallySpecified = table.Column<DateTime>(nullable: false),
                    DateRevised = table.Column<DateTime>(nullable: false),
                    SourceType = table.Column<int>(nullable: false),
                    BuilderTypeName = table.Column<string>(nullable: true),
                    ScriptSourceCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredReportSourceRevisions", x => new { x.RevisionId, x.ReportId });
                    table.ForeignKey(
                        name: "FK_RegisteredReportSourceRevisions_RegisteredReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "RegisteredReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategoryAssignments",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: false),
                    ReportName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategoryAssignments", x => new { x.AssignmentId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ReportCategoryAssignments_ReportCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportParameterConstraints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: false),
                    ParameterName = table.Column<string>(nullable: true),
                    MappingType = table.Column<int>(nullable: false),
                    MappingValue = table.Column<string>(nullable: true),
                    MappingValueTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportParameterConstraints", x => new { x.Id, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_ReportParameterConstraints_ReportRoleAssignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "ReportRoleAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredReports_Id",
                table: "RegisteredReports",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredReportSourceRevisions_ReportId",
                table: "RegisteredReportSourceRevisions",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_Id",
                table: "ReportCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_ParentCategoryId",
                table: "ReportCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategoryAssignments_CategoryId",
                table: "ReportCategoryAssignments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportParameterConstraints_AssignmentId",
                table: "ReportParameterConstraints",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoleAssignments_Id",
                table: "ReportRoleAssignments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRoles_Id",
                table: "ReportRoles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredReportSourceRevisions");

            migrationBuilder.DropTable(
                name: "ReportCategoryAssignments");

            migrationBuilder.DropTable(
                name: "ReportParameterConstraints");

            migrationBuilder.DropTable(
                name: "ReportRoles");

            migrationBuilder.DropTable(
                name: "RegisteredReports");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropTable(
                name: "ReportRoleAssignments");
        }
    }
}
