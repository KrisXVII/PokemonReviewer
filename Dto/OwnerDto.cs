namespace PokemonReviewer.Dto;

public class OwnerDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Gym { get; set; }
    public Country? Country { get; set; }
}