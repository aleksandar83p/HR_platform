using Microsoft.EntityFrameworkCore;
using System;

namespace HR_platform.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobCandidateSkill> JobCandidateSkills { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 1, Name = "Java programming" });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 2, Name = "C# programming" });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 3, Name = "Database design" });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 4, Name = "English language" });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 5, Name = "Russian language" });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 6, Name = "German language" });

            modelBuilder.Entity<JobCandidate>().HasData(
                new JobCandidate
                {
                    JobCandidateId = 1,
                    FullName = "Leonardo Blue",
                    DateOfBirth = new DateTime(1984, 05, 01),
                    ContactNumber = "555-789-456",
                    Email = "leo@email.com"
                });

            modelBuilder.Entity<JobCandidate>().HasData(
                new JobCandidate
                {
                    JobCandidateId = 2,
                    FullName = "Rick Hunter",
                    DateOfBirth = new DateTime(1990, 11, 22),
                    ContactNumber = "123-987-654",
                    Email = "skull1@email.com"
                });

            modelBuilder.Entity<JobCandidate>().HasData(
                new JobCandidate
                {
                    JobCandidateId = 3,
                    FullName = "John Doe",
                    DateOfBirth = new DateTime(1965, 01, 29),
                    ContactNumber = "111-222-333-444",
                    Email = "johndoe@email.com"
                });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
                new JobCandidateSkill
                {
                    Id = 1,
                    JobCandidateId = 1,
                    SkillId = 1
                });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
               new JobCandidateSkill
               {
                   Id = 2,
                   JobCandidateId = 1,
                   SkillId = 4
               });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
               new JobCandidateSkill
               {
                   Id = 3,
                   JobCandidateId = 2,
                   SkillId = 2
               });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
               new JobCandidateSkill
               {
                   Id = 4,
                   JobCandidateId = 2,
                   SkillId = 3
               });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
               new JobCandidateSkill
               {
                   Id = 5,
                   JobCandidateId = 2,
                   SkillId = 6
               });

            modelBuilder.Entity<JobCandidateSkill>().HasData(
               new JobCandidateSkill
               {
                   Id = 6,
                   JobCandidateId = 3,
                   SkillId = 5
               });
        }
    }
}
