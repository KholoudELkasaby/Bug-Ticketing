using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class BugAssignmentConfiguration : IEntityTypeConfiguration<BugAssignment>
{
    public void Configure(EntityTypeBuilder<BugAssignment> builder)
    {
        builder.HasKey(ba => new { ba.BugId, ba.UserId });
        builder
            .HasOne(ba => ba.Bug)
            .WithMany(b => b.BugAssignments)
            .HasForeignKey(ba => ba.BugId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(ba => ba.User)
            .WithMany(u => u.BugAssignments)
            .HasForeignKey(ba => ba.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
