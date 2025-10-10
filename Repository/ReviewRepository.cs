namespace PokemonReviewer.Repository;

public class ReviewRepository : IReviewInterface
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }
    
    public bool ReviewExists(int id)
    {
        return _context.Reviews.Any(o => o.Id == id);
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public Review? GetReview(int reviewId)
    {
        return _context.Reviews.Find(reviewId);
    }

    public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
    {
        return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
    }

    public bool CreateReview(Review review)
    {
        _context.Add(review);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved != 0;
    }
}