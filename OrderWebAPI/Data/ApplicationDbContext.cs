using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models;

namespace OrderWebAPI.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<OrderModel> orderModels { get; set; }
    public DbSet<CategoryModel> categoryModels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //FluentAPI nos campos das tabelas do banco de dados


        //Tabela OrderModel
        builder.Entity<OrderModel>().HasKey(o => o.OrderId);
        builder.Entity<OrderModel>().Property(o => o.NumOrder).HasDefaultValueSql("ABS(CHECKSUM(NEWID())) % 10000");
        builder.Entity<OrderModel>().Property(o => o.NameFull).HasMaxLength(100).IsRequired();
        builder.Entity<OrderModel>().Property(o => o.Description).HasMaxLength(250).IsRequired();
        builder.Entity<OrderModel>().Property(o => o.Price).HasPrecision(14, 2).IsRequired();
        builder.Entity<OrderModel>().Property(c => c.Status).HasConversion<string>().IsRequired();

        //Tabela CategoryModel
        builder.Entity<CategoryModel>().HasKey(c=>c.CategoryId);
        builder.Entity<CategoryModel>().Property(c => c.Service_Type).HasMaxLength(100);



        //Foreign Key
        builder.Entity<OrderModel>().HasOne<CategoryModel>(o => o.CategoryModel).WithMany(c => c.OrderModels).HasForeignKey(c => c.CategoryId);



        base.OnModelCreating(builder);
    }

}
