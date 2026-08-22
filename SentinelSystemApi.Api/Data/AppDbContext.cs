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
	public DbSet<Device> Devices => Set<Device>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DeviceStatus>(entity =>
		{
			entity.HasKey(ds => ds.Id);
			entity.Property(ds => ds.Mode).IsRequired();
			entity.Property(ds => ds.LastSeenAt).IsRequired();
			entity.Property(ds => ds.PanAngle).IsRequired().HasPrecision(4, 1);
			entity.Property(ds => ds.TiltAngle).IsRequired().HasPrecision(4, 1);

			entity.HasOne(d => d.Device).WithOne(ds => ds.DeviceStatus).HasForeignKey<DeviceStatus>(ds => ds.DeviceId).OnDelete(DeleteBehavior.Cascade);
			entity.HasIndex(ds => ds.DeviceId).IsUnique();
		});

		modelBuilder.Entity<Telemetry>(entity =>
		{
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Pir).IsRequired();
			entity.Property(t => t.TemperatureC).IsRequired().HasPrecision(5, 2);
			entity.Property(t => t.Humidity).IsRequired().HasPrecision(5, 2);
			entity.Property(t => t.Timestamp).IsRequired().HasDefaultValueSql("GETDATE()");

			entity.HasOne(d => d.Device).WithMany(t => t.TelemetryReadings).HasForeignKey(t => t.DeviceId).OnDelete(DeleteBehavior.Cascade);

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
		modelBuilder.Entity<Device>(entity =>
		{
			entity.HasKey(d => d.Id);
			entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
			entity.Property(d => d.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

			entity.HasOne(o => o.Owner).WithMany(d => d.Devices).HasForeignKey(d => d.OwnerId).OnDelete(DeleteBehavior.Cascade);
		});
	}
}