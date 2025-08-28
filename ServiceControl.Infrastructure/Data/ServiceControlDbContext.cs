using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.ValueObjects;

namespace ServiceControl.Infrastructure.Data;

public class ServiceControlDbContext : DbContext
{
    public ServiceControlDbContext(DbContextOptions<ServiceControlDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceExecution> ServiceExecutions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    value => ServiceExecutionId.Create(value))
                .HasColumnName("Id")
                .HasMaxLength(50);

            entity.Property(e => e.ServicoExecutado)
                .HasConversion(
                    desc => desc.Value,
                    value => ServiceDescription.Create(value))
                .HasColumnName("ServicoExecutado")
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(e => e.Responsavel)
                .HasConversion(
                    resp => resp.Value,
                    value => ResponsiblePerson.Create(value))
                .HasColumnName("Responsavel")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.WeatherCondition)
                .HasConversion(
                    cond => cond.Value,
                    value => WeatherCondition.Create(value))
                .HasColumnName("WeatherCondition")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Temperature)
                .HasConversion(
                    temp => temp.Value,
                    value => Temperature.Create(value))
                .HasColumnName("Temperature")
                .HasPrecision(5, 2);

            entity.Property(e => e.Data)
                .HasConversion(
                    data => data.Value,
                    value => ExecutionDate.Create(value))
                .HasColumnName("Data");

            entity.Property(e => e.ProcessedAt)
                .HasConversion(
                    proc => proc.Value,
                    value => ProcessedDate.Create(value))
                .HasColumnName("ProcessedAt");

            entity.Property(e => e.CreatedAt)
                .HasConversion(
                    created => created.Value,
                    value => CreatedDate.Create(value))
                .HasColumnName("CreatedAt");
            
            entity.HasIndex(e => e.Data);
            entity.HasIndex(e => e.ProcessedAt);
        });
    }
}
