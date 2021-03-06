// <auto-generated />
using System;
using HR_platform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HR_platform.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HR_platform.Models.JobCandidate", b =>
                {
                    b.Property<int>("JobCandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobCandidateId");

                    b.ToTable("JobCandidates");

                    b.HasData(
                        new
                        {
                            JobCandidateId = 1,
                            ContactNumber = "555-789-456",
                            DateOfBirth = new DateTime(1984, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "leo@email.com",
                            FullName = "Leonardo Blue"
                        },
                        new
                        {
                            JobCandidateId = 2,
                            ContactNumber = "123-987-654",
                            DateOfBirth = new DateTime(1990, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "skull1@email.com",
                            FullName = "Rick Hunter"
                        },
                        new
                        {
                            JobCandidateId = 3,
                            ContactNumber = "111-222-333-444",
                            DateOfBirth = new DateTime(1965, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johndoe@email.com",
                            FullName = "John Doe"
                        });
                });

            modelBuilder.Entity("HR_platform.Models.JobCandidateSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobCandidateId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobCandidateId");

                    b.HasIndex("SkillId");

                    b.ToTable("JobCandidateSkills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            JobCandidateId = 1,
                            SkillId = 1
                        },
                        new
                        {
                            Id = 2,
                            JobCandidateId = 1,
                            SkillId = 4
                        },
                        new
                        {
                            Id = 3,
                            JobCandidateId = 2,
                            SkillId = 2
                        },
                        new
                        {
                            Id = 4,
                            JobCandidateId = 2,
                            SkillId = 3
                        },
                        new
                        {
                            Id = 5,
                            JobCandidateId = 2,
                            SkillId = 6
                        },
                        new
                        {
                            Id = 6,
                            JobCandidateId = 3,
                            SkillId = 5
                        });
                });

            modelBuilder.Entity("HR_platform.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            SkillId = 1,
                            Name = "Java programming"
                        },
                        new
                        {
                            SkillId = 2,
                            Name = "C# programming"
                        },
                        new
                        {
                            SkillId = 3,
                            Name = "Database design"
                        },
                        new
                        {
                            SkillId = 4,
                            Name = "English language"
                        },
                        new
                        {
                            SkillId = 5,
                            Name = "Russian language"
                        },
                        new
                        {
                            SkillId = 6,
                            Name = "German language"
                        });
                });

            modelBuilder.Entity("HR_platform.Models.JobCandidateSkill", b =>
                {
                    b.HasOne("HR_platform.Models.JobCandidate", "JobCandidate")
                        .WithMany("JobCandidateSkills")
                        .HasForeignKey("JobCandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HR_platform.Models.Skill", "Skill")
                        .WithMany("JobCandidateSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobCandidate");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("HR_platform.Models.JobCandidate", b =>
                {
                    b.Navigation("JobCandidateSkills");
                });

            modelBuilder.Entity("HR_platform.Models.Skill", b =>
                {
                    b.Navigation("JobCandidateSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
