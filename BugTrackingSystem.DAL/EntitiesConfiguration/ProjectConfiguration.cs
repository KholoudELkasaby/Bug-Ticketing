using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.ProjectId);
        builder.Property(p => p.Name)
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .HasMaxLength(2000);
        builder.Property(p => p.Status)
            .HasConversion<string>();
        builder.Property(p => p.StartDate)
            .HasPrecision(3);
        builder.Property(p => p.EndDate)
            .HasPrecision(3);
        //Relationships
        builder.HasMany(p => p.Bugs)
            .WithOne(b => b.Project)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.ProjectMembers)
            .WithOne(pm => pm.Project)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
