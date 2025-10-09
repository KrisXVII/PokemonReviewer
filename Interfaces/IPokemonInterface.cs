
namespace PokemonReviewer.Interfaces;

public interface IPokemonInterface
{
    ICollection<Pokemon> GetPokemon();
    Pokemon? GetPokemon(int id);
    Pokemon? GetPokemon(string name);
    decimal GetPokemonRating(int pokeId);
    bool PokemonExists(int pokeId);

}