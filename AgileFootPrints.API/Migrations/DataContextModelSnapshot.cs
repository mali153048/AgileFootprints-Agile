﻿// <auto-generated />
using System;
using AgileFootPrints.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgileFootPrints.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("AgileFootPrints.API.Models.CodeFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<string>("PathInProject");

                    b.Property<string>("ProjectName");

                    b.HasKey("Id");

                    b.ToTable("CodeFiles");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.CodeFileRevision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CodeFileId");

                    b.Property<int>("RevisionId");

                    b.HasKey("Id");

                    b.HasIndex("CodeFileId");

                    b.HasIndex("RevisionId");

                    b.ToTable("CodeFileRevisions");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Epic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EpicDescription");

                    b.Property<string>("EpicName");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Epics");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("EndTime")
                        .IsRequired();

                    b.Property<int>("ProjectId");

                    b.Property<string>("StartTime")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.Property<string>("Venue")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Message");

                    b.Property<int>("RecieverId");

                    b.Property<int>("SenderId");

                    b.Property<string>("Subject");

                    b.Property<bool>("isMail");

                    b.Property<bool>("isNotification");

                    b.Property<bool>("isRead");

                    b.Property<int>("projectId");

                    b.HasKey("Id");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("projectId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("priority");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectDescription");

                    b.Property<string>("ProjectKey");

                    b.Property<string>("ProjectName");

                    b.Property<int?>("StatusId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.ProjectContributor", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ProjectId");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("projectContributors");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Revision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommitMessage");

                    b.Property<DateTime>("DateCraeted");

                    b.Property<string>("PathInRepository");

                    b.Property<string>("RepositoryURL");

                    b.Property<int?>("WorkItemId");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Revisions");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.ScrumRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ScrumRoleName");

                    b.HasKey("Id");

                    b.ToTable("ScrumRoles");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Sprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("SprintName");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StatusId");

                    b.Property<int>("projectId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("projectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("status");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Story", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcceptanceCriteria");

                    b.Property<int?>("EpicId");

                    b.Property<int?>("PriorityId");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("SprintId");

                    b.Property<int?>("StatusId");

                    b.Property<string>("StoryDescription");

                    b.Property<string>("StoryName");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EpicId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("StatusId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("StatusId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.UserProjectRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProjectId");

                    b.Property<int>("ScrumRolesId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ScrumRolesId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjectRole");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("SprintId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("StatusId");

                    b.Property<int?>("UserId");

                    b.Property<string>("WorkItemDescription");

                    b.Property<string>("WorkItemName");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("WorkItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.CodeFileRevision", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.CodeFile", "CodeFile")
                        .WithMany("CodeFileRevisions")
                        .HasForeignKey("CodeFileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.Revision", "Revision")
                        .WithMany("CodeFileRevisions")
                        .HasForeignKey("RevisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Epic", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany("Epics")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Meeting", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Notification", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.User", "Reciever")
                        .WithMany("NotificationsRecieved")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.User", "Sender")
                        .WithMany("NotificationsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Project", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.ProjectContributor", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany("ProjectContributors")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany("ProjectContributors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Revision", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.WorkItem", "workItem")
                        .WithMany("Revisions")
                        .HasForeignKey("WorkItemId");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Sprint", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.Project", "project")
                        .WithMany("Sprints")
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.Story", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Epic", "Epic")
                        .WithMany("Stories")
                        .HasForeignKey("EpicId");

                    b.HasOne("AgileFootPrints.API.Models.Priority", "Priority")
                        .WithMany("Stories")
                        .HasForeignKey("PriorityId");

                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany("Stories")
                        .HasForeignKey("ProjectId");

                    b.HasOne("AgileFootPrints.API.Models.Sprint", "Sprint")
                        .WithMany("Stories")
                        .HasForeignKey("SprintId");

                    b.HasOne("AgileFootPrints.API.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany("Stories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.User", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Status")
                        .WithMany("Users")
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.UserProjectRole", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.ScrumRoles", "ScrumRole")
                        .WithMany()
                        .HasForeignKey("ScrumRolesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.UserRole", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgileFootPrints.API.Models.WorkItem", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Sprint", "Sprint")
                        .WithMany("WorkItems")
                        .HasForeignKey("SprintId");

                    b.HasOne("AgileFootPrints.API.Models.Status", "Status")
                        .WithMany("WorkItems")
                        .HasForeignKey("StatusId");

                    b.HasOne("AgileFootPrints.API.Models.User", "User")
                        .WithMany("WorkItems")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AgileFootPrints.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
