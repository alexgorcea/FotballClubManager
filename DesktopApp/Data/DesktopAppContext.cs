using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesktopApp.Models;

namespace DesktopApp.Data
{
    public class DesktopAppContext : DbContext
    {
        public DesktopAppContext (DbContextOptions<DesktopAppContext> options)
            : base(options)
        {
        }

        public DbSet<DesktopApp.Models.Player> Player { get; set; } = default!;
        public DbSet<DesktopApp.Models.Team> Team { get; set; } = default!;
        public DbSet<DesktopApp.Models.Coach> Coach { get; set; } = default!;
        public DbSet<DesktopApp.Models.Feedback> Feedback { get; set; } = default!;
        public DbSet<DesktopApp.Models.TrainingSession> TrainingSession { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithOne(c => c.Team)
                .HasForeignKey<Coach>(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Team)
                .WithMany()
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Coach)
                .WithMany(c => c.TrainingSessions)
                .HasForeignKey(ts => ts.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.TrainingSession)
                .WithMany(ts => ts.Feedbacks)
                .HasForeignKey(f => f.TrainingSessionId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Player)
                .WithMany()
                .HasForeignKey(f => f.PlayerId);
        }
    }
}
