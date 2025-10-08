using PokemonReviewer.Data;
using PokemonReviewer.Interfaces;

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
    
}