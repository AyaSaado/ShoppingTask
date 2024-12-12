namespace ShoppingTask.EF.Config;

public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.AddedDate).HasColumnType("datetime2");
        builder.Property(r => r.ExpiryDate).HasColumnType("datetime2");
        builder.Property(r => r.Token).HasColumnType("nvarchar").HasMaxLength(256).IsRequired(true);
        builder.Property(r => r.JwtId).HasColumnType("nvarchar").HasMaxLength(128).IsRequired(true);

        builder.ToTable(nameof(RefreshToken));
    }
}
