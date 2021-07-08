using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassArms",
                columns: table => new
                {
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Arm = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassArms", x => x.ClassArmID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolRegCodes",
                columns: table => new
                {
                    CodeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolRegCodes", x => x.CodeId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "StudentCodes",
                columns: table => new
                {
                    CodeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCodes", x => x.CodeID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Range = table.Column<int>(type: "INTEGER", nullable: false),
                    SchoolSpecific = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCodes",
                columns: table => new
                {
                    CodeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCodes", x => x.CodeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAs",
                columns: table => new
                {
                    LgaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StateID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAs", x => x.LgaID);
                    table.ForeignKey(
                        name: "FK_LGAs_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSubjects",
                columns: table => new
                {
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    isCompulsory = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjects", x => x.ClassSubjectID);
                    table.ForeignKey(
                        name: "FK_ClassSubjects_ClassArms_ClassArmID",
                        column: x => x.ClassArmID,
                        principalTable: "ClassArms",
                        principalColumn: "ClassArmID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSubjects_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnualEnrollments",
                columns: table => new
                {
                    AnnualEnrollmentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    ThirdTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualEnrollments", x => x.AnnualEnrollmentID);
                    table.ForeignKey(
                        name: "FK_AnnualEnrollments_ClassSubjects_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "ClassSubjects",
                        principalColumn: "ClassSubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletionState = table.Column<bool>(type: "INTEGER", nullable: false),
                    CA = table.Column<int>(type: "INTEGER", nullable: false),
                    Exam = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_ClassSubjects_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "ClassSubjects",
                        principalColumn: "ClassSubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeePayments",
                columns: table => new
                {
                    FeePaymentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeePayments", x => x.FeePaymentID);
                });

            migrationBuilder.CreateTable(
                name: "PrincipalQualifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qlf = table.Column<string>(type: "TEXT", nullable: false),
                    PrincipalID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalQualifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    ProductSalesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: true),
                    SalePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.ProductSalesID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    ClassPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    ResultRecordID = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultID);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    LgaID = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowPositon = table.Column<bool>(type: "INTEGER", nullable: false),
                    Append = table.Column<string>(type: "TEXT", nullable: false),
                    StudentCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ProprietorID = table.Column<string>(type: "TEXT", nullable: false),
                    TeachersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RegNoCount = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionStart = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    AdminID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_Schools_LGAs_LgaID",
                        column: x => x.LgaID,
                        principalTable: "LGAs",
                        principalColumn: "LgaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    LgaID = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: true),
                    RegNo = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: true),
                    Student_SchoolID = table.Column<int>(type: "INTEGER", nullable: true),
                    HasGraduated = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsActivated = table.Column<bool>(type: "INTEGER", nullable: true),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: true),
                    ParentID = table.Column<string>(type: "TEXT", nullable: true),
                    Teacher_SchoolID = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ParentID",
                        column: x => x.ParentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ClassArms_ClassArmID",
                        column: x => x.ClassArmID,
                        principalTable: "ClassArms",
                        principalColumn: "ClassArmID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_LGAs_LgaID",
                        column: x => x.LgaID,
                        principalTable: "LGAs",
                        principalColumn: "LgaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Schools_Student_SchoolID",
                        column: x => x.Student_SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Schools_Teacher_SchoolID",
                        column: x => x.Teacher_SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralExpenses",
                columns: table => new
                {
                    GeneralExpensesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Expense = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralExpenses", x => x.GeneralExpensesID);
                    table.ForeignKey(
                        name: "FK_GeneralExpenses_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPackages",
                columns: table => new
                {
                    SchoolPackageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Package = table.Column<int>(type: "INTEGER", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPackages", x => x.SchoolPackageID);
                    table.ForeignKey(
                        name: "FK_SchoolPackages_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionName = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    Current = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSCompulsories",
                columns: table => new
                {
                    SSCompulsoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSCompulsories", x => x.SSCompulsoryID);
                    table.ForeignKey(
                        name: "FK_SSCompulsories_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSCompulsories_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSDrops",
                columns: table => new
                {
                    SSDropID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDrops", x => x.SSDropID);
                    table.ForeignKey(
                        name: "FK_SSDrops_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSDrops_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherQualifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qlf = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherQualifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TeacherQualifications_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    TeacherSubjectID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.TeacherSubjectID);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_ClassSubjects_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "ClassSubjects",
                        principalColumn: "ClassSubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductExpenses",
                columns: table => new
                {
                    ProductExpenseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    Expense = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExpenses", x => x.ProductExpenseID);
                    table.ForeignKey(
                        name: "FK_ProductExpenses_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<int>(type: "INTEGER", nullable: false),
                    Current = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    SessionID = table.Column<int>(type: "INTEGER", nullable: false),
                    TermStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TermEnd = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermID);
                    table.ForeignKey(
                        name: "FK_Terms_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    FeeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeeName = table.Column<string>(type: "TEXT", nullable: false),
                    FeeType = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAmountPaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAmountOwed = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAmountExpeced = table.Column<decimal>(type: "TEXT", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.FeeID);
                    table.ForeignKey(
                        name: "FK_Fees_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultRecords",
                columns: table => new
                {
                    ResultRecordID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassAverage = table.Column<double>(type: "REAL", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultRecords", x => x.ResultRecordID);
                    table.ForeignKey(
                        name: "FK_ResultRecords_ClassArms_ClassArmID",
                        column: x => x.ClassArmID,
                        principalTable: "ClassArms",
                        principalColumn: "ClassArmID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultRecords_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Closed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Minutes = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    QNPS = table.Column<int>(type: "INTEGER", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_Tests_ClassSubjects_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "ClassSubjects",
                        principalColumn: "ClassSubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tests_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tests_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentFees",
                columns: table => new
                {
                    StudentFeeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    FeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    AmountOwed = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFees", x => x.StudentFeeID);
                    table.ForeignKey(
                        name: "FK_StudentFees_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentFees_Fees_FeeID",
                        column: x => x.FeeID,
                        principalTable: "Fees",
                        principalColumn: "FeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qn = table.Column<string>(type: "TEXT", nullable: false),
                    QType = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrectOptions = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<double>(type: "REAL", nullable: false),
                    AllOptionsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    TestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAttempts",
                columns: table => new
                {
                    AttemptID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeSpent = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Finished = table.Column<bool>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    TestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAttempts", x => x.AttemptID);
                    table.ForeignKey(
                        name: "FK_TestAttempts_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAttempts_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Opt = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    Correct = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAttempts",
                columns: table => new
                {
                    QuestionAttemptID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestAttemptID = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    OptSeed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttempts", x => x.QuestionAttemptID);
                    table.ForeignKey(
                        name: "FK_QuestionAttempts_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAttempts_TestAttempts_TestAttemptID",
                        column: x => x.TestAttemptID,
                        principalTable: "TestAttempts",
                        principalColumn: "AttemptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QAttemptsOptions",
                columns: table => new
                {
                    QAttemptsOptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionAttemptID = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAttemptsOptions", x => x.QAttemptsOptionID);
                    table.ForeignKey(
                        name: "FK_QAttemptsOptions_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QAttemptsOptions_QuestionAttempts_QuestionAttemptID",
                        column: x => x.QuestionAttemptID,
                        principalTable: "QuestionAttempts",
                        principalColumn: "QuestionAttemptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_ClassSubjectID",
                table: "AnnualEnrollments",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_StudentID",
                table: "AnnualEnrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_TermID",
                table: "AnnualEnrollments",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassArmID",
                table: "AspNetUsers",
                column: "ClassArmID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LgaID",
                table: "AspNetUsers",
                column: "LgaID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentID",
                table: "AspNetUsers",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolID",
                table: "AspNetUsers",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Student_SchoolID",
                table: "AspNetUsers",
                column: "Student_SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Teacher_SchoolID",
                table: "AspNetUsers",
                column: "Teacher_SchoolID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_ClassArmID",
                table: "ClassSubjects",
                column: "ClassArmID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_SubjectID",
                table: "ClassSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ClassSubjectID",
                table: "Enrollments",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TermID",
                table: "Enrollments",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_FeePayments_FeeID",
                table: "FeePayments",
                column: "FeeID");

            migrationBuilder.CreateIndex(
                name: "IX_FeePayments_StudentID",
                table: "FeePayments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_TermID",
                table: "Fees",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExpenses_SchoolID",
                table: "GeneralExpenses",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LGAs_StateID",
                table: "LGAs",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalQualifications_PrincipalID",
                table: "PrincipalQualifications",
                column: "PrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExpenses_ProductID",
                table: "ProductExpenses",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SchoolID",
                table: "Products",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductID",
                table: "ProductSales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_StudentID",
                table: "ProductSales",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_QAttemptsOptions_OptionID",
                table: "QAttemptsOptions",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_QAttemptsOptions_QuestionAttemptID",
                table: "QAttemptsOptions",
                column: "QuestionAttemptID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_QuestionID",
                table: "QuestionAttempts",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_TestAttemptID",
                table: "QuestionAttempts",
                column: "TestAttemptID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestID",
                table: "Questions",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRecords_ClassArmID",
                table: "ResultRecords",
                column: "ClassArmID");

            migrationBuilder.CreateIndex(
                name: "IX_ResultRecords_TermID",
                table: "ResultRecords",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ResultRecordID",
                table: "Results",
                column: "ResultRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentID",
                table: "Results",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TermID",
                table: "Results",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPackages_SchoolID",
                table: "SchoolPackages",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AdminID",
                table: "Schools",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_LgaID",
                table: "Schools",
                column: "LgaID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ProprietorID",
                table: "Schools",
                column: "ProprietorID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SchoolID",
                table: "Sessions",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SSCompulsories_SchoolID",
                table: "SSCompulsories",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SSCompulsories_SubjectID",
                table: "SSCompulsories",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SSDrops_SchoolID",
                table: "SSDrops",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SSDrops_SubjectID",
                table: "SSDrops",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFees_FeeID",
                table: "StudentFees",
                column: "FeeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFees_StudentID",
                table: "StudentFees",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherQualifications_TeacherID",
                table: "TeacherQualifications",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_ClassSubjectID",
                table: "TeacherSubjects",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherID",
                table: "TeacherSubjects",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_SessionID",
                table: "Terms",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_StudentID",
                table: "TestAttempts",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_TestID",
                table: "TestAttempts",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ClassSubjectID",
                table: "Tests",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SchoolID",
                table: "Tests",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TermID",
                table: "Tests",
                column: "TermID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualEnrollments_AspNetUsers_StudentID",
                table: "AnnualEnrollments",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualEnrollments_Terms_TermID",
                table: "AnnualEnrollments",
                column: "TermID",
                principalTable: "Terms",
                principalColumn: "TermID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Terms_TermID",
                table: "Enrollments",
                column: "TermID",
                principalTable: "Terms",
                principalColumn: "TermID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeePayments_AspNetUsers_StudentID",
                table: "FeePayments",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeePayments_Fees_FeeID",
                table: "FeePayments",
                column: "FeeID",
                principalTable: "Fees",
                principalColumn: "FeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrincipalQualifications_AspNetUsers_PrincipalID",
                table: "PrincipalQualifications",
                column: "PrincipalID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSales_AspNetUsers_StudentID",
                table: "ProductSales",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSales_Products_ProductID",
                table: "ProductSales",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_StudentID",
                table: "Results",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_ResultRecords_ResultRecordID",
                table: "Results",
                column: "ResultRecordID",
                principalTable: "ResultRecords",
                principalColumn: "ResultRecordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Terms_TermID",
                table: "Results",
                column: "TermID",
                principalTable: "Terms",
                principalColumn: "TermID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_AdminID",
                table: "Schools",
                column: "AdminID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_ProprietorID",
                table: "Schools",
                column: "ProprietorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_AdminID",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_ProprietorID",
                table: "Schools");

            migrationBuilder.DropTable(
                name: "AnnualEnrollments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "FeePayments");

            migrationBuilder.DropTable(
                name: "GeneralExpenses");

            migrationBuilder.DropTable(
                name: "PrincipalQualifications");

            migrationBuilder.DropTable(
                name: "ProductExpenses");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "QAttemptsOptions");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "SchoolPackages");

            migrationBuilder.DropTable(
                name: "SchoolRegCodes");

            migrationBuilder.DropTable(
                name: "SSCompulsories");

            migrationBuilder.DropTable(
                name: "SSDrops");

            migrationBuilder.DropTable(
                name: "StudentCodes");

            migrationBuilder.DropTable(
                name: "StudentFees");

            migrationBuilder.DropTable(
                name: "TeacherCodes");

            migrationBuilder.DropTable(
                name: "TeacherQualifications");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionAttempts");

            migrationBuilder.DropTable(
                name: "ResultRecords");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TestAttempts");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "ClassSubjects");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClassArms");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "LGAs");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
