namespace PokemonReviewer.Models;

public class PokemonCategory
{
    public int PokemonId { get; set; }
    public int CategoryId { get; set; }
    public required Pokemon Pokemon { get; set; }
    public required Category Category { get; set; }
}