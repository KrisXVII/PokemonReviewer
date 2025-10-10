namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewInterface _reviewRepository;
    private readonly IPokemonInterface _pokemonInterface;
    private readonly IReviewerInterface _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewController(
        IReviewInterface reviewRepository,
        IPokemonInterface pokemonInterface,
        IReviewerInterface reviewerRepository,
        IMapper mapper
    )
    {
        _reviewRepository = reviewRepository;
        _pokemonInterface = pokemonInterface;
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
    public IActionResult GetReviews()
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    public IActionResult GetReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
            return NotFound();

        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(review);
    }

    [HttpGet("pokemon/{pokeId}")]
    [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewsForAPokemon(int pokeId)
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(pokeId));
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(reviews);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult CreateReview(
        [FromQuery] int pokeId,
        [FromQuery] int reviewerId,
        [FromBody] ReviewDto reviewCreate
    )
    {
        if (reviewCreate == null)
            return BadRequest(ModelState);

        var review = _reviewRepository.GetReviews()
            .FirstOrDefault(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper());

        if (review != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviewMap = _mapper.Map<Review>(reviewCreate);
        var pokemon = _pokemonInterface.GetPokemon(pokeId);
        var reviewer = _reviewerRepository.GetReviewer(reviewerId);
        if (pokemon != null && reviewer != null)
        {
            reviewMap.Pokemon = pokemon;
            reviewMap.Reviewer = reviewer;
        }
        else
        {
            ModelState.AddModelError("", "Pokemon or reviewer not found");
        }

        if (!_reviewRepository.CreateReview(reviewMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
        }

        return Ok(reviewMap);
    }
}