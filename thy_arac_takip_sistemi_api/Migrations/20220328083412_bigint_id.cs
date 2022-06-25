using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace thy_arac_takip_sistemi_api.Migrations
{
    public partial class bigint_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ButtonEntries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    buttonNo = table.Column<int>(type: "int", nullable: false),
                    doorNo = table.Column<int>(type: "int", nullable: false),
                    dateLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButtonEntries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "DoorAssigns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doorNo = table.Column<int>(type: "int", nullable: false),
                    plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reservationId = table.Column<int>(type: "int", nullable: false),
                    dateAssign = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorAssigns", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eventLogType = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PTSLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doorNo = table.Column<int>(type: "int", nullable: false),
                    isButtonEntry = table.Column<bool>(type: "bit", nullable: false),
                    buttonNo = table.Column<int>(type: "int", nullable: false),
                    plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PTSLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReaderModules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    readerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    readerId = table.Column<int>(type: "int", nullable: false),
                    readerIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    readerData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doorCountStart = table.Column<int>(type: "int", nullable: false),
                    doorCountFinish = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateLastRead = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderModules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservationType = table.Column<int>(type: "int", nullable: true),
                    agentId = table.Column<long>(type: "bigint", nullable: true),
                    agentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driverSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driverPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    carPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    carType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateEstimatedArriveStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateEstimatedArriveFinish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCarArrived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isArrived = table.Column<bool>(type: "bit", nullable: true),
                    isUnload = table.Column<bool>(type: "bit", nullable: true),
                    isWaiting = table.Column<bool>(type: "bit", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    sccText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doorNumber = table.Column<int>(type: "int", nullable: true),
                    dateDoorAssigned = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SCCs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCCs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doorNumber = table.Column<int>(type: "int", nullable: false),
                    doorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    operationArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    order = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    isNotEmpty = table.Column<bool>(type: "bit", nullable: false),
                    isBusy = table.Column<bool>(type: "bit", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastOwnerReservationId = table.Column<int>(type: "int", nullable: false),
                    lastOwnerPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateLastOwned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateLastExit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    reservationId = table.Column<int>(type: "int", nullable: false),
                    readerModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doors_ReaderModules_readerModuleId",
                        column: x => x.readerModuleId,
                        principalTable: "ReaderModules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AWBs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    awbText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<int>(type: "int", nullable: true),
                    weightUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pieces = table.Column<int>(type: "int", nullable: true),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    handlingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateFlight = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sccText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWBs", x => x.id);
                    table.ForeignKey(
                        name: "FK_AWBs_Reservations_reservationId",
                        column: x => x.reservationId,
                        principalTable: "Reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitingQueues",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingQueues", x => x.id);
                    table.ForeignKey(
                        name: "FK_WaitingQueues_Reservations_reservationId",
                        column: x => x.reservationId,
                        principalTable: "Reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sccId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lats", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lats_SCCs_sccId",
                        column: x => x.sccId,
                        principalTable: "SCCs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorSCC",
                columns: table => new
                {
                    doorListid = table.Column<int>(type: "int", nullable: false),
                    sccListid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorSCC", x => new { x.doorListid, x.sccListid });
                    table.ForeignKey(
                        name: "FK_DoorSCC_Doors_doorListid",
                        column: x => x.doorListid,
                        principalTable: "Doors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoorSCC_SCCs_sccListid",
                        column: x => x.sccListid,
                        principalTable: "SCCs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCCTexts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sccText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    awbId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCCTexts", x => x.id);
                    table.ForeignKey(
                        name: "FK_SCCTexts_AWBs_awbId",
                        column: x => x.awbId,
                        principalTable: "AWBs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configs",
                columns: new[] { "key", "value" },
                values: new object[,]
                {
                    { "RESERVATION_DELAY", "300" },
                    { "BUTTON_DELAY", "7200" },
                    { "SMS_URL", "https://wsdev.thy.com/sms-gateway/send-sms/bulk" },
                    { "SMS_USERNAME", "AYP-THYAO" },
                    { "SMS_PASSWORD", "hPhjix8sOvf2" },
                    { "SMS_BASIC_AUTH_USERNAME", "wstestuser" },
                    { "SMS_BASIC_AUTH_PASSWORD", "thy1234" },
                    { "apikey", "l7xx890646a1315c4c4181441a0c292a4666" },
                    { "apisecret", "3f0517bbf36441f88488a86703d4acb5" },
                    { "clientTransactionId", "TEST1234" },
                });

            migrationBuilder.InsertData(
                table: "SCCs",
                columns: new[] { "id", "code", "strength" },
                values: new object[,]
                {
                    { 1, "JOKER", 1 },
                    { 2, "IMPORT", 2 },
                    { 3, "ICHAT", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWBs_reservationId",
                table: "AWBs",
                column: "reservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_readerModuleId",
                table: "Doors",
                column: "readerModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoorSCC_sccListid",
                table: "DoorSCC",
                column: "sccListid");

            migrationBuilder.CreateIndex(
                name: "IX_Lats_sccId",
                table: "Lats",
                column: "sccId");

            migrationBuilder.CreateIndex(
                name: "IX_SCCTexts_awbId",
                table: "SCCTexts",
                column: "awbId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingQueues_reservationId",
                table: "WaitingQueues",
                column: "reservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ButtonEntries");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "DoorAssigns");

            migrationBuilder.DropTable(
                name: "DoorSCC");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "Lats");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "NLogs");

            migrationBuilder.DropTable(
                name: "PTSLogs");

            migrationBuilder.DropTable(
                name: "SCCTexts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WaitingQueues");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "SCCs");

            migrationBuilder.DropTable(
                name: "AWBs");

            migrationBuilder.DropTable(
                name: "ReaderModules");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
