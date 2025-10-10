namespace PokemonReviewer.Interfaces;

public interface ICategoryInterface
{
    ICollection<Category> GetCategories();
    Category? GetCategory(int id);
    Category? GetCategory(string name);
    ICollection<Pokemon> GetPokemonByCategory(int categoryId);
    bool CategoryExists(int id);
    bool CreateCategory(Category category);
    bool Save();
}