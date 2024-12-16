﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TooDue.Models;

#nullable disable

namespace TooDue.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241120104424_Migration5")]
    partial class Migration5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TooDue.Models.Comment", b =>
                {
                    b.Property<int>("Comment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Comment_id"));

                    b.Property<DateTime>("Comment_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Comment_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Users")
                        .HasColumnType("int");

                    b.HasKey("Comment_id");

                    b.HasIndex("Users");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TooDue.Models.Project", b =>
                {
                    b.Property<int>("Project_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Project_id"));

                    b.Property<DateTime>("Project_create_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Project_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Project_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Project_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Project_id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TooDue.Models.Project_User_Role", b =>
                {
                    b.Property<int>("Pur_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pur_id"));

                    b.Property<int>("Projects")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Users")
                        .HasColumnType("int");

                    b.HasKey("Pur_id");

                    b.HasIndex("Projects");

                    b.HasIndex("Users");

                    b.ToTable("Project_User_Roles");
                });

            modelBuilder.Entity("TooDue.Models.Project_User_Task", b =>
                {
                    b.Property<int>("Put_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Put_id"));

                    b.Property<int>("Projects")
                        .HasColumnType("int");

                    b.Property<bool>("Task_completion")
                        .HasColumnType("bit");

                    b.Property<int>("Tasks")
                        .HasColumnType("int");

                    b.Property<int>("Users")
                        .HasColumnType("int");

                    b.HasKey("Put_id");

                    b.HasIndex("Projects");

                    b.HasIndex("Tasks");

                    b.HasIndex("Users");

                    b.ToTable("Project_User_Tasks");
                });

            modelBuilder.Entity("TooDue.Models.Task", b =>
                {
                    b.Property<int>("Task_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Task_id"));

                    b.Property<bool>("Is_collaborative")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Task_complete_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Task_create_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Task_deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Task_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Task_label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Task_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Task_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Task_update_date")
                        .HasColumnType("datetime2");

                    b.HasKey("Task_id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TooDue.Models.Theme", b =>
                {
                    b.Property<int>("Theme_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Theme_id"));

                    b.Property<string>("Animal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dark_accent_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flower")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Light_accent_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Main_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Theme_id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("TooDue.Models.User", b =>
                {
                    b.Property<int>("User_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_id"));

                    b.Property<string>("Banner_picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profile_picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Themes")
                        .HasColumnType("int");

                    b.Property<string>("User_bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_displayname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_id");

                    b.HasIndex("Themes");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TooDue.Models.Comment", b =>
                {
                    b.HasOne("TooDue.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Users")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TooDue.Models.Project_User_Role", b =>
                {
                    b.HasOne("TooDue.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("Projects")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TooDue.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Users")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TooDue.Models.Project_User_Task", b =>
                {
                    b.HasOne("TooDue.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("Projects")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TooDue.Models.Task", "Task")
                        .WithMany()
                        .HasForeignKey("Tasks")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TooDue.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Users")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TooDue.Models.User", b =>
                {
                    b.HasOne("TooDue.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("Themes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });
#pragma warning restore 612, 618
        }
    }
}
