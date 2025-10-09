namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
    
    private readonly ICountryInterface _countryInterface;
    private readonly IMapper _mapper;
    
    public CountryController(ICountryInterface countryInterface, IMapper mapper)
    {
        _countryInterface = countryInterface;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetCountry()
    {
        var countries = _mapper.Map<List<CountryDto>>(_countryInterface.GetCountries());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(countries);
    }
    
    [HttpGet("{countryId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountry(int countryId)
    {
        if (!_countryInterface.CountryExists(countryId))
            return NotFound();

        var country = _mapper.Map<CountryDto>(_countryInterface.GetCountry(countryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }

    [HttpGet("/owners/{ownerId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountryOfAnOwner(int ownerId)
    {
        var country = _mapper.Map<CountryDto>(_countryInterface.GetCountryByOwner(ownerId));
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }
    
}

