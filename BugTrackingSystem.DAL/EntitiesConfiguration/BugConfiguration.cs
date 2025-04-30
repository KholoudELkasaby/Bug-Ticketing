using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;
public class BugConfiguration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder)
    {
        builder.HasKey(b => b.BugId);
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(b => b.Priority)
            .IsRequired()
            .HasConversion<string>();
        //Relationships
        builder.HasMany(b => b.BugAssignments)
            .WithOne(ba => ba.Bug)
            .HasForeignKey(ba => ba.BugId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(b => b.Attachments)
            .WithOne(a => a.Bug)
            .HasForeignKey(a => a.BugId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(b => b.Project)
            .WithMany(p => p.Bugs)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
