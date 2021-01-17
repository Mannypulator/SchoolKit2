﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Models;

namespace WebApi.Migrations
{
    [DbContext(typeof(SchoolKitContext))]
    [Migration("20200702201705_droptables")]
    partial class droptables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApi.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("LgaID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LgaID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("WebApi.Models.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClassID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("WebApi.Models.ClassArm", b =>
                {
                    b.Property<int>("ClassArmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Arm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClassArmID");

                    b.HasIndex("ClassID");

                    b.ToTable("ClassArms");
                });

            modelBuilder.Entity("WebApi.Models.ClassSubject", b =>
                {
                    b.Property<int>("ClassSubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassArmID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("SubjectID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isCompulsory")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClassSubjectID");

                    b.HasIndex("ClassArmID");

                    b.HasIndex("SubjectID");

                    b.ToTable("ClassSubjects");
                });

            modelBuilder.Entity("WebApi.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CA")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassSubjectID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CompletionState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Exam")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TermID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.Property<int>("grade")
                        .HasColumnType("INTEGER");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("ClassSubjectID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TermID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("WebApi.Models.LGA", b =>
                {
                    b.Property<int>("LgaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("StateID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LgaID");

                    b.HasIndex("StateID");

                    b.ToTable("LGAs");
                });

            modelBuilder.Entity("WebApi.Models.PrincipalQualification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PrincipalID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Qlf")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("PrincipalID");

                    b.ToTable("PrincipalQualifications");
                });

            modelBuilder.Entity("WebApi.Models.Result", b =>
                {
                    b.Property<int>("ResultID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<int>("ClassPosition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TermID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ResultID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TermID");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("WebApi.Models.ResultRecord", b =>
                {
                    b.Property<int>("ResultRecordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassArmID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ResultRecordID");

                    b.HasIndex("ClassArmID");

                    b.HasIndex("SchoolID");

                    b.HasIndex("TermID");

                    b.ToTable("ResultRecords");
                });

            modelBuilder.Entity("WebApi.Models.SSCompulsory", b =>
                {
                    b.Property<int>("SSCompulsoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubjectID")
                        .HasColumnType("INTEGER");

                    b.HasKey("SSCompulsoryID");

                    b.HasIndex("SchoolID");

                    b.HasIndex("SubjectID");

                    b.ToTable("SSCompulsories");
                });

            modelBuilder.Entity("WebApi.Models.SSDrop", b =>
                {
                    b.Property<int>("SSDropID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubjectID")
                        .HasColumnType("INTEGER");

                    b.HasKey("SSDropID");

                    b.HasIndex("SchoolID");

                    b.HasIndex("SubjectID");

                    b.ToTable("SSDrops");
                });

            modelBuilder.Entity("WebApi.Models.School", b =>
                {
                    b.Property<int>("SchoolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Append")
                        .HasColumnType("TEXT");

                    b.Property<int>("LgaID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("RegNoCount")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SessionStart")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowPositon")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeachersCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("SchoolID");

                    b.HasIndex("LgaID");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("WebApi.Models.State", b =>
                {
                    b.Property<int>("StateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("StateID");

                    b.ToTable("States");
                });

            modelBuilder.Entity("WebApi.Models.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Range")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SchoolSpecific")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("SubjectID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebApi.Models.TeacherQualification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Qlf")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("TeacherID");

                    b.ToTable("TeacherQualifications");
                });

            modelBuilder.Entity("WebApi.Models.TeacherSubject", b =>
                {
                    b.Property<int>("TeacherSubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassSubjectID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeacherID")
                        .HasColumnType("TEXT");

                    b.HasKey("TeacherSubjectID");

                    b.HasIndex("ClassSubjectID");

                    b.HasIndex("TeacherID");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("WebApi.Models.Term", b =>
                {
                    b.Property<int>("TermID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Current")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Label")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Session")
                        .HasColumnType("TEXT");

                    b.HasKey("TermID");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("WebApi.Models.Test", b =>
                {
                    b.Property<int>("TestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassSubjectID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Minutes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TestID");

                    b.HasIndex("ClassSubjectID");

                    b.HasIndex("SchoolID");

                    b.HasIndex("TermID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("WebApi.Models.TestAttempt", b =>
                {
                    b.Property<int>("AttemptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TestID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeSpent")
                        .HasColumnType("INTEGER");

                    b.HasKey("AttemptID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TestID");

                    b.ToTable("TestAttempts");
                });

            modelBuilder.Entity("WebApi.Models.Admin", b =>
                {
                    b.HasBaseType("WebApi.Models.ApplicationUser");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("WebApi.Models.Principal", b =>
                {
                    b.HasBaseType("WebApi.Models.ApplicationUser");

                    b.Property<int>("SchoolID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("SchoolID");

                    b.HasDiscriminator().HasValue("Principal");
                });

            modelBuilder.Entity("WebApi.Models.Student", b =>
                {
                    b.HasBaseType("WebApi.Models.ApplicationUser");

                    b.Property<int>("ClassArmID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasGraduated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegNo")
                        .HasColumnType("TEXT");

                    b.Property<int>("SchoolID")
                        .HasColumnName("Student_SchoolID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ClassArmID");

                    b.HasIndex("SchoolID");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("WebApi.Models.Teacher", b =>
                {
                    b.HasBaseType("WebApi.Models.ApplicationUser");

                    b.Property<int>("SchoolID")
                        .HasColumnName("Teacher_SchoolID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("SchoolID");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.ApplicationUser", b =>
                {
                    b.HasOne("WebApi.Models.LGA", "LGA")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("LgaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.ClassArm", b =>
                {
                    b.HasOne("WebApi.Models.Class", "Class")
                        .WithMany("ClassArms")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.ClassSubject", b =>
                {
                    b.HasOne("WebApi.Models.ClassArm", "ClassArm")
                        .WithMany("ClassSubject")
                        .HasForeignKey("ClassArmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Subject", "Subject")
                        .WithMany("ClassSubject")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Enrollment", b =>
                {
                    b.HasOne("WebApi.Models.ClassSubject", "ClassSubject")
                        .WithMany("Enrollments")
                        .HasForeignKey("ClassSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Term", "Term")
                        .WithMany("Enrollments")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.LGA", b =>
                {
                    b.HasOne("WebApi.Models.State", "State")
                        .WithMany("LGAs")
                        .HasForeignKey("StateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.PrincipalQualification", b =>
                {
                    b.HasOne("WebApi.Models.Principal", "Principal")
                        .WithMany("PrincipalQualifications")
                        .HasForeignKey("PrincipalID");
                });

            modelBuilder.Entity("WebApi.Models.Result", b =>
                {
                    b.HasOne("WebApi.Models.Student", "Student")
                        .WithMany("Results")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Term", "Term")
                        .WithMany("Results")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.ResultRecord", b =>
                {
                    b.HasOne("WebApi.Models.ClassArm", "ClassArm")
                        .WithMany("ResultRecords")
                        .HasForeignKey("ClassArmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("ResultRecords")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Term", "Term")
                        .WithMany("ResultRecords")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.SSCompulsory", b =>
                {
                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("SSCompulsories")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Subject", "Subject")
                        .WithMany("SSCompulsories")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.SSDrop", b =>
                {
                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("SSDrops")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Subject", "Subject")
                        .WithMany("SSDrops")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.School", b =>
                {
                    b.HasOne("WebApi.Models.LGA", "LGA")
                        .WithMany("Schools")
                        .HasForeignKey("LgaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.TeacherQualification", b =>
                {
                    b.HasOne("WebApi.Models.Teacher", "Teacher")
                        .WithMany("TeacherQualifications")
                        .HasForeignKey("TeacherID");
                });

            modelBuilder.Entity("WebApi.Models.TeacherSubject", b =>
                {
                    b.HasOne("WebApi.Models.ClassSubject", "ClassSubject")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("ClassSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Teacher", "Teacher")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherID");
                });

            modelBuilder.Entity("WebApi.Models.Test", b =>
                {
                    b.HasOne("WebApi.Models.ClassSubject", "ClassSubject")
                        .WithMany("Tests")
                        .HasForeignKey("ClassSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("Tests")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Term", "Term")
                        .WithMany("Tests")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.TestAttempt", b =>
                {
                    b.HasOne("WebApi.Models.Student", "Student")
                        .WithMany("TestAttempts")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Test", "Test")
                        .WithMany("Attempts")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Principal", b =>
                {
                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("Principals")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Student", b =>
                {
                    b.HasOne("WebApi.Models.ClassArm", "ClassArm")
                        .WithMany("Students")
                        .HasForeignKey("ClassArmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Teacher", b =>
                {
                    b.HasOne("WebApi.Models.School", "School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
