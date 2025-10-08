namespace PokemonReviewer.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<PokemonCategory>? PokemonCategories { get; set; }
}