namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerController : Controller
{
    private readonly IOwnerInterface _ownerRepository;
    private readonly ICountryInterface _countryRepository;
    private readonly IMapper _mapper;
    
    public OwnerController(IOwnerInterface ownerRepository, ICountryInterface countryRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
    public IActionResult GetOwners()
    {
        var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owners);
    }
    
    [HttpGet("{ownerId}")]
    [ProducesResponseType(200, Type = typeof(Owner))]
    [ProducesResponseType(400)]
    public IActionResult GetOwner(int ownerId)
    {
        if (!_ownerRepository.OwnerExists(ownerId))
            return NotFound();

        var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(owner);
    }

    [HttpGet("{ownerId}/pokemon")]
    [ProducesResponseType(200, Type = typeof(Owner))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonByOwner(int ownerId)
    {
        if (!_ownerRepository.OwnerExists(ownerId))
        {
            return NotFound();
        }

        var owner = _mapper.Map<List<PokemonDto>>(
            _ownerRepository.GetPokemonByOwner(ownerId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(owner);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
    {
        if (ownerCreate == null)
            return BadRequest(ModelState);

        var owner = _ownerRepository.GetOwners()
            .FirstOrDefault(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper());

        if (owner != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        

        var ownerMap = _mapper.Map<Owner>(ownerCreate);
        var country = _countryRepository.GetCountry(countryId);
        if (country != null)
        {
            ownerMap.Country = country;
        }
        else
        {
            ModelState.AddModelError("", "Country is null");
        }

        if (!_ownerRepository.CreateOwner(ownerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
        }

        return Ok("Successfully created");
    }
    
    [HttpPut("{ownerId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(Owner))]
    [ProducesResponseType(404)]
    public IActionResult UpdateOwner(
        int ownerId,
        [FromBody] OwnerDto updatedOwner
    )
    {

        if (updatedOwner == null || ownerId != updatedOwner.Id)
            return BadRequest(ModelState);

        if (!_ownerRepository.OwnerExists(ownerId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var ownerMap = _mapper.Map<Owner>(updatedOwner);

        if (!_ownerRepository.UpdateOwner(ownerMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }
        
        return Ok(_mapper.Map<OwnerDto>(ownerMap));
    }
    
    [HttpDelete("{ownerId:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    public IActionResult DeleteOwner(int ownerId)
    {
        
        var ownerToDelete = _ownerRepository.GetOwner(ownerId);
        if (ownerToDelete is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        if (_ownerRepository.DeleteOwner(ownerToDelete))
            return NoContent();
        
        ModelState.AddModelError("", "Somwthing went wrong deleting owner");
        return StatusCode(500, ModelState);

    }
    
}