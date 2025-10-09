
namespace PokemonReviewer.Repository;

public class CategoryRepository : ICategoryInterface
{

    private DataContext _context; // context = code that gives access to the database

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    
    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public ICollection<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category? GetCategory(int id)
    {
        return _context.Categories.Find(id);
    }
    
    public Category? GetCategory(string name)
    {
        return _context.Categories.FirstOrDefault(p => p.Name == name);
    }

    public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
    {
        return _context.PokemonCategories
            .Where(e => e.CategoryId == categoryId)
            .Select(c => c.Pokemon).ToList();
    }
}