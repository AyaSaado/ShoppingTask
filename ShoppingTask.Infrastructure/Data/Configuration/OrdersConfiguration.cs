
internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
 
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnType("datetime"); 

     
        builder.Property(x => x.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); 

    
        builder.HasOne(x => x.User)
            .WithMany(u => u.Orders) 
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.OrderItems)
         .WithOne(oi => oi.Order) 
         .HasForeignKey(oi => oi.OrderId)
         .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(nameof(Order));
    }
}