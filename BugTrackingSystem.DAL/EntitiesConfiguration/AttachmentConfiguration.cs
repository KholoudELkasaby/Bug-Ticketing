using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(a => a.AttachmentId);

        builder.Property(a => a.FileName)
            .HasMaxLength(255);
        builder.Property(a => a.ContentType)
            .HasMaxLength(100);
        builder.Property(a => a.FileUrl)
            .HasMaxLength(500);
        builder.Property(a => a.CreatedDate)
            .HasPrecision(3);
        //Relationships
        builder.HasOne(a => a.Bug)
            .WithMany(b => b.Attachments)
            .HasForeignKey(a => a.BugId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
