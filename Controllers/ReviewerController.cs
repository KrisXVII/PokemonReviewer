namespace PokemonReviewer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewerController : Controller
{
    private readonly IReviewerInterface _reviewerRepository;
    private readonly IMapper _mapper;
    
    public ReviewerController(IReviewerInterface reviewerRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
    public IActionResult GetReviewers()
    {
        var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviewers);
    }
    
    [HttpGet("{reviewerId}")]
    [ProducesResponseType(200, Type = typeof(Reviewer))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewer(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
            return NotFound();

        var reviewer = _mapper.Map<ReviewerDto>(
            _reviewerRepository.GetReviewer(reviewerId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviewer);
    }

    [HttpGet("{reviewerId}/reviews")]
    [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewsByReviewer(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
            return NotFound();

        var reviews = _mapper.Map<List<ReviewDto>>(
            _reviewerRepository.GetReviewsByReviewer(reviewerId));
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
    {
        if (reviewerCreate == null)
            return BadRequest(ModelState);

        var reviewer = _reviewerRepository.GetReviewers()
            .FirstOrDefault(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper());

        if (reviewer != null)
        {
            ModelState.AddModelError("", "Country already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

        if (!_reviewerRepository.CreateReviewer(reviewerMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
        }

        return Ok(_mapper.Map<ReviewerDto>(reviewerMap));
    }
}
