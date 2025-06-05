namespace LB9_3;

public class Song
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Composer { get; set; }
    public int Year { get; set; }
    public string Lyrics { get; set; }
    public List<string> Performers { get; set; }

    public override string ToString()
    {
        return $"Назва: {Title}\nАвтор: {Author}\nКомпозитор: {Composer}\nРік: {Year}\nВиконавці: {string.Join(", ", Performers)}\nТекст:\n{Lyrics}\n";
    }
}