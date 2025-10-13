
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

    public bool CreateCategory(Category category)
    {
        // Change Tracker: creating or manipulating objects this must be done
        // add, update, modifying
        // connected vs disconnected (uncommon)
        // EntityState.Added

        _context.Add(category);
        return Save();
    }

    public bool UpdateCategory(Category category)
    {
        _context.Update(category);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _context.Remove(category);
        return Save();
    }

    public bool Save()
    {
        // Calling SaveChanges() code generates and executes SQL
        var saved = _context.SaveChanges();
        return saved != 0;
    }
}