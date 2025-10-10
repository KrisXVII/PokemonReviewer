namespace PokemonReviewer.Interfaces;

public interface IReviewInterface
{
    ICollection<Review> GetReviews();
    Review? GetReview(int reviewId);
    ICollection<Review> GetReviewsOfAPokemon(int pokeId);
    bool ReviewExists(int reviewId);
}