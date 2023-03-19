using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectportfolio.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_DetailId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_LogotypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_MockupId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TechnicalDescription",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MockupId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LogotypeId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InitialDescription",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DetailId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_DetailId",
                table: "Projects",
                column: "DetailId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_LogotypeId",
                table: "Projects",
                column: "LogotypeId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_MockupId",
                table: "Projects",
                column: "MockupId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_DetailId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_LogotypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Images_MockupId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "TechnicalDescription",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "MockupId",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "LogotypeId",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "InitialDescription",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DetailId",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_DetailId",
                table: "Projects",
                column: "DetailId",
                principalTable: "Images",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_LogotypeId",
                table: "Projects",
                column: "LogotypeId",
                principalTable: "Images",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Images_MockupId",
                table: "Projects",
                column: "MockupId",
                principalTable: "Images",
                principalColumn: "ImageId");
        }
    }
}
