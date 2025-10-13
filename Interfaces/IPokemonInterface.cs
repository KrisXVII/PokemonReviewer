
namespace PokemonReviewer.Interfaces;

public interface IPokemonInterface
{
    ICollection<Pokemon> GetPokemon();
    Pokemon? GetPokemon(int id);
    Pokemon? GetPokemon(string name);
    decimal GetPokemonRating(int pokeId);
    bool PokemonExists(int pokeId);
    bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
    bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
    bool DeletePokemon(Pokemon pokemon);
    bool Save();

}