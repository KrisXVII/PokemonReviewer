namespace PokemonReviewer.Repository;

public class PokemonRepository : IPokemonInterface
{
    private readonly DataContext _context;
    
    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Pokemon> GetPokemon()
    {
        return _context.Pokemon.OrderBy(p => p.Id).ToList();
    }

    public Pokemon? GetPokemon(int id)
    {
        return _context.Pokemon.Find(id);
    }

    public Pokemon? GetPokemon(string name)
    {
        return _context.Pokemon.FirstOrDefault(p => p.Name == name);
    }

    public decimal GetPokemonRating(int pokeId)
    {
        var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokeId);
        if (!reviews.Any())
            return 0;
        return (decimal) reviews.Sum(r => r.Rating) / reviews.Count();
    }

    public bool PokemonExists(int pokeId)
    {
        return _context.Pokemon.Any(p => p.Id == pokeId);
    }
}