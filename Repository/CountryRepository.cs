
namespace PokemonReviewer.Repository;

public class CountryRepository : ICountryInterface
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    
    public CountryRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }

    public ICollection<Country> GetCountries()
    {
        return _context.Countries.ToList();
    }

    public Country? GetCountry(int id)
    {
        return _context.Countries.Find(id);
    }

    public Country? GetCountryByOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersFromACountry(int countryId)
    {
        return _context.Owners.Where(c => c.Country.Id == countryId).ToList();
    }

    public bool CreateCountry(Country country)
    {
        _context.Add(country);
        return Save();
    }

    public bool UpdateCountry(Country country)
    {
        _context.Update(country);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved != 0;
    }
}