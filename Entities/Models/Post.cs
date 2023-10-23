using System;
using Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Models;

public class Post: BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public string Descriotion { get; set; }
    
    public int CategoryId { get; set; }
    
    public int AuthorId { get; set; }
    
    // This is: Navigation Property
    public Category Category { get; set; }
    
    public User Author { get; set; }
}

// This is: FluentApi 
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Descriotion).IsRequired();
        builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
        builder.HasOne(p => p.Author).WithMany(c => c.Posts).HasForeignKey(p => p.AuthorId);
    }
}