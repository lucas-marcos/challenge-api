using Microsoft.EntityFrameworkCore;
using ServiceControl.Domain.Entities;

namespace ServiceControl.Infrastructure.Data;

public class ServiceControlDbContext : DbContext
{
    public ServiceControlDbContext(DbContextOptions<ServiceControlDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OrderDescription)
                .HasColumnName("OrderDescription")
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(e => e.ResponsiblePerson)
                .HasColumnName("ResponsiblePerson")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.WeatherCondition)
                .HasColumnName("WeatherCondition")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Temperature)
                .HasColumnName("Temperature")
                .HasPrecision(5, 2);

            entity.Property(e => e.ExecutionDate)
                .HasColumnName("ExecutionDate");

            entity.Property(e => e.ProcessedAt)
                .HasColumnName("ProcessedAt");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt");
            
            entity.HasIndex(e => e.ExecutionDate);
            entity.HasIndex(e => e.ProcessedAt);
        });
    }
}
