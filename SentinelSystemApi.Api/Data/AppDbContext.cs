using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Data;

public class AppDbContext : DbContext
{
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      public DbSet<DeviceStatus> DeviceStatuses => Set<DeviceStatus>();
      public DbSet<Event> Events => Set<Event>();
      public DbSet<Telemetry> Telemetries => Set<Telemetry>();
      public DbSet<User> Users => Set<User>();

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<DeviceStatus>(entity =>
            {
                  entity.HasKey(d => d.Id);
                  entity.Property(d => d.Mode).IsRequired();
                  entity.Property(d => d.LastSeenAt).IsRequired();
                  entity.Property(d => d.PanAngle).IsRequired().HasPrecision(4, 1);
                  entity.Property(d => d.TiltAngle).IsRequired().HasPrecision(4, 1);
            });

            modelBuilder.Entity<Telemetry>(entity =>
            {
                  entity.HasKey(t => t.Id);
                  entity.Property(t => t.Pir).IsRequired();
                  entity.Property(t => t.TemperatureC).IsRequired().HasPrecision(5, 2);
                  entity.Property(t => t.Humidity).IsRequired().HasPrecision(5, 2);
                  entity.Property(t => t.Timestamp).IsRequired().HasDefaultValueSql("GETDATE()");

            });
            modelBuilder.Entity<User>(entity =>
            {
                  entity.HasKey(u => u.Id);
                  entity.HasIndex(u => u.Username).IsUnique();
                  entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
                  entity.Property(u => u.PasswordHash).IsRequired();
                  entity.Property(u => u.Role).IsRequired();
                  entity.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Event>(entity =>
            {
                  entity.HasKey(e => e.Id);
                  entity.Property(e => e.ClipPath);
                  entity.Property(e => e.Notes);

                  entity.HasOne(t => t.Telemetry).WithMany(e => e.Events).HasForeignKey(e => e.TelemetryId).OnDelete(DeleteBehavior.SetNull);
            });
      }
}