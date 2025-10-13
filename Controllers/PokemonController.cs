namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController(
    IPokemonInterface pokemonRepository,
    IOwnerInterface ownerRepository,
    IReviewInterface reviewRepository,
    
    IMapper mapper
    ) : Controller
{

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetPokemon()
    {
        var pokemons = mapper.Map<List<PokemonDto>>(pokemonRepository.GetPokemon());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemons);
    }

    [HttpGet("{pokeId}")]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemon(int pokeId)
    {
        if (!pokemonRepository.PokemonExists(pokeId))
            return NotFound();

        var pokemon = mapper.Map<PokemonDto>(pokemonRepository.GetPokemon(pokeId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemon);
    }

    [HttpGet("{pokeId}/rating")]
    [ProducesResponseType(200, Type = typeof(decimal))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonRating(int pokeId)
    {
        if (!pokemonRepository.PokemonExists(pokeId))
            return NotFound();

        var pokemonRating = pokemonRepository.GetPokemonRating(pokeId);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemonRating);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateOwner(
        [FromQuery] int ownerId,
        [FromQuery] int categoryId,
        [FromBody] PokemonDto pokemonCreate)
    {
        if (pokemonCreate == null)
            return BadRequest(ModelState);

        var pokemon = pokemonRepository.GetPokemon()
            .FirstOrDefault(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper());

        if (pokemon != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pokemonMap = mapper.Map<Pokemon>(pokemonCreate);
        
        if (!pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap));
        {
            ModelState.AddModelError("", "Something went wrong while saving");
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{pokeId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    [ProducesResponseType(404)]
    public IActionResult UpdatePokemon(
        int pokeId,
        [FromQuery] int ownerId,
        [FromQuery] int categoryId,
        [FromBody] PokemonDto updatedPokemon
    )
    {

        if (updatedPokemon == null || pokeId != updatedPokemon.Id)
            return BadRequest(ModelState);

        if (!pokemonRepository.PokemonExists(pokeId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var pokemonMap = mapper.Map<Pokemon>(updatedPokemon);

        if (pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemonMap))
            return Ok(mapper.Map<CategoryDto>(pokemonMap));
        
        ModelState.AddModelError("", "Something went wrong updating pokemon");
        return StatusCode(500, ModelState);    
    } 
    
}