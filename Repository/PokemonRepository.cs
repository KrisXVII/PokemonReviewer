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

    public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
    {
        var pokemonOwnerEntity = _context.Owners.Find(ownerId);
        var category = _context.Categories.Find(categoryId);
        
        if (pokemonOwnerEntity == null || category == null)
            return false;

        var pokemonOwner = new PokemonOwner
        {
            Pokemon = pokemon,
            Owner = pokemonOwnerEntity
        };
            
        _context.Add(pokemonOwner);

        var pokemonCategory = new PokemonCategory
        {
            Category = category,
            Pokemon = pokemon
        };

        _context.PokemonCategories.Add(pokemonCategory);
        _context.Pokemon.Add(pokemon);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved != 0;
    }
}