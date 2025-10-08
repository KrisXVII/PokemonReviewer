namespace PokemonReviewer.Models;

public class PokemonOwner
{
    public int PokemonId { get; set; }
    public int OwnerId { get; set; }
    public required Pokemon Pokemon { get; set; }
    public required Owner Owner { get; set; }
}