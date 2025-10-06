namespace PokemonReviewer.lib;

public static class JsonHelper
{
    public static string ToPrettyJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    public static void Print(this object obj)
    {
        AnsiConsole.WriteLine(ToPrettyJson(obj));
    }
}