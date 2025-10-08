
namespace PokemonReviewer.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Pokemon>().Property(p => p.Name).HasColumnType("varchar(255)");
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.Property(o => o.FirstName).HasColumnType("varchar(255)");
            entity.Property(o => o.LastName).HasColumnType("varchar(255)"); 
            entity.Property(o => o.Gym).HasColumnType("varchar(255)");
        });
        modelBuilder.Entity<Country>().Property(c => c.Name).HasColumnType("varchar(255)");
        modelBuilder.Entity<Category>().Property(ca => ca.Name).HasColumnType("varchar(255)");
        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(r => r.Title).HasColumnType("varchar(255)");
            entity.Property(o => o.Text).HasColumnType("varchar(255)"); 
        });
        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.Property(re => re.FirstName).HasColumnType("varchar(255)");
            entity.Property(re => re.LastName).HasColumnType("varchar(255)");
        });
        
        modelBuilder.Entity<PokemonCategory>()
            .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.PokemonId);
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Category)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.CategoryId);
        
        modelBuilder.Entity<PokemonOwner>()
            .HasKey(pc => new { pc.PokemonId, pc.OwnerId });
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonOwners)
            .HasForeignKey(c => c.PokemonId);
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(p => p.Owner)
            .WithMany(pc => pc.PokemonOwners)
            .HasForeignKey(c => c.OwnerId);
    }
}
