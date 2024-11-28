using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPASSData.Migrations
{
    /// <inheritdoc />
    public partial class DB_20241127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "NEWID()", comment: "使用者 Id"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "姓名"),
                    Ac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "帳號"),
                    Sw = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "密碼"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "信箱"),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手機"),
                    AuthenticatorUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "驗證器 使用者 Id"),
                    SourceId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "平台來源Id"),
                    Is_SuperUser = table.Column<bool>(type: "bit", nullable: false, comment: "超級使用者"),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false, comment: "帳號狀態"),
                    Last_Login = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "上次登入時間"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()", comment: "建立者 Id"),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "刪除者 Id"),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "更新者 Id"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否刪除"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()", comment: "創建日期"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()", comment: "更新日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
