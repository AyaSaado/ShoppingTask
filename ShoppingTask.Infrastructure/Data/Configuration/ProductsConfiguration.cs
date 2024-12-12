namespace ShoppingTask.Infrastructure.Data.Configuration;

internal class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(75).IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Price).HasColumnType("decimal(18,4)").IsRequired();

        builder.Property(x => x.Stock).HasColumnType("decimal(18,4)").IsRequired();

        builder.Property(x => x.ImageUrl).HasMaxLength(500);

        builder.Property(x => x.IsDeleted).HasColumnType("bit").IsRequired();

        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.ToTable(nameof(Product));
    }
}
