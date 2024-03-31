using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCEBlueCross.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    State = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DOB = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubscribedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    MotherQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChilhoodQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BossQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BandQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    ErrorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RegisteringUserUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.ErrorId);
                    table.ForeignKey(
                        name: "FK_Errors_Users_RegisteringUserUserId",
                        column: x => x.RegisteringUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Payors",
                columns: table => new
                {
                    PayorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayorAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    State = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RegisteringUserUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payor", x => x.PayorId);
                    table.ForeignKey(
                        name: "FK_Payor_Users_RegisteringUserUserId",
                        column: x => x.RegisteringUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProviderAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    State = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RegisteringUserUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_Providers_Users_RegisteringUserUserId",
                        column: x => x.RegisteringUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    ConditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConditionLabel = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ErrorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ConditionId);
                    table.ForeignKey(
                        name: "FK_Condition_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "ErrorId");
                });

            migrationBuilder.CreateTable(
                name: "PayorErrors",
                columns: table => new
                {
                    PayorErrorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorId = table.Column<int>(type: "int", nullable: false),
                    PayorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayorError", x => x.PayorErrorId);
                    table.ForeignKey(
                        name: "FK_PayorError_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "ErrorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayorError_Payor_PayorId",
                        column: x => x.PayorId,
                        principalTable: "Payor",
                        principalColumn: "PayorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EntryHour = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    DischargeHour = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    InstitutionalClaimCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionalClaimCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeBill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferalNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayerClaimControlNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutoAccidentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileInf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnsetSymptomDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    InitialTreatmentDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastSeenDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AcuteManifestationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AccidentDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastMenstrualDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastXRayDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    HearingVisionPrescriptionDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DisabilityDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastWorkedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AuthorizedReturnWorkDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AssumedCareDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RepricerReceivedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PrincipalDiagnosisCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmitingDiagnosisCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientReasonCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalCausesCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisRelatedCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherDiagnosisCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalProcedureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherProcedureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurrenceSpamCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurrenceInformationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueInformationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionInformationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentCodeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimPricingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostService = table.Column<double>(type: "float", nullable: false),
                    CostMaterial = table.Column<double>(type: "float", nullable: false),
                    CostMedicine = table.Column<double>(type: "float", nullable: false),
                    ProviderCost = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    MemberUserId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    PayorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claims_Payor_PayorId",
                        column: x => x.PayorId,
                        principalTable: "Payor",
                        principalColumn: "PayorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Users_MemberUserId",
                        column: x => x.MemberUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_MemberUserId",
                table: "Claims",
                column: "MemberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PayorId",
                table: "Claims",
                column: "PayorId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ProviderId",
                table: "Claims",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_ErrorId",
                table: "Condition",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_Errors_RegisteringUserUserId",
                table: "Errors",
                column: "RegisteringUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payor_RegisteringUserUserId",
                table: "Payor",
                column: "RegisteringUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayorError_ErrorId",
                table: "PayorError",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_PayorError_PayorId",
                table: "PayorError",
                column: "PayorId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_RegisteringUserUserId",
                table: "Providers",
                column: "RegisteringUserUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "PayorError");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Payor");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
