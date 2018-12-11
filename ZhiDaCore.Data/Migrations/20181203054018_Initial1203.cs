using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZhiDaCore.EFCore.Migrations
{
    public partial class Initial1203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    SequenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BizUnit = table.Column<string>(nullable: true),
                    Exchange = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    TradeId = table.Column<string>(nullable: true),
                    Ccy = table.Column<string>(nullable: true),
                    Side = table.Column<string>(nullable: true),
                    TradePrice = table.Column<decimal>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    EventDesc = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    MMY = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    SecType = table.Column<string>(nullable: true),
                    PutCall = table.Column<string>(nullable: true),
                    StrikePx = table.Column<decimal>(nullable: true),
                    StrikePrice = table.Column<string>(nullable: true),
                    TradeDate = table.Column<DateTime>(nullable: false),
                    BusinessDate = table.Column<string>(nullable: true),
                    Multiplier = table.Column<string>(nullable: true),
                    TradingUser = table.Column<string>(nullable: true),
                    BizDate = table.Column<DateTime>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    ClearingMember = table.Column<string>(nullable: true),
                    TradingMember = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    TransTime = table.Column<DateTime>(nullable: true),
                    ContractSize = table.Column<int>(nullable: false),
                    Market = table.Column<string>(nullable: true),
                    GroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.SequenceID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModityTime = table.Column<DateTime>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
