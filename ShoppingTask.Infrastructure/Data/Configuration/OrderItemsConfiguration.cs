
internal class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

  
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnType("decimal(18,4)"); 


        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

  
        builder.HasOne(x => x.Order)
            .WithMany(o => o.OrderItems) 
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Product)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(OrderItem));
    }
}