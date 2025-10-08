using Microsoft.AspNetCore.Mvc;
using PokemonReviewer.Interfaces;

namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : Controller
{

    private readonly IPokemonInterface _pokemonRepository;
    
    public PokemonController(IPokemonInterface pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemon()
    {
        var pokemons = _pokemonRepository.GetPokemon();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemons);
    }
}