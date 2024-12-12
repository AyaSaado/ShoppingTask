namespace ShoppingTask.Infrastructure.Data.Configuration;

internal class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
   
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();


        builder.Property(x => x.UserName)
            .HasColumnType("nvarchar(50)")
            .IsRequired();


        builder.Property(x => x.Email)
            .HasColumnType("nvarchar(50)")
            .IsRequired();


        builder.Property(x => x.PasswordHash)
            .HasColumnType("nvarchar(400)") 
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();


        builder.HasIndex(x => x.UserName).IsUnique();


        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.ToTable(nameof(User));
    }
}
