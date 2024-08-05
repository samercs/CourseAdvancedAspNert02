using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Infrastructure.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ToDoItem>()
        //    .HasQueryFilter(i => i.Title.Equals("Samer"));

        modelBuilder.Entity<ToDoItem>()
            .HasOne(i => i.Category)
            .WithMany(i => i.ToDoItems)
            .HasForeignKey(i => i.CategoryId)
            .IsRequired(true);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<Category> Categories { get; set; }

}