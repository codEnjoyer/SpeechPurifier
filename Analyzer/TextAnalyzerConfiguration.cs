namespace SpeechPurifier.Analyzer;

public class TextAnalyzerConfiguration
{
    public IReadOnlySet<string> BadWords { get; private set; }

    public TextAnalyzerConfiguration(IReadOnlySet<string>? badWords = null)
    {
        BadWords = badWords ?? new HashSet<string>(GetBadWords());
    }
    
    private static IEnumerable<string> GetBadWords()
    {
        using var sr = new StreamReader("../../../swears.txt");
        var words = sr.ReadToEnd().Split('\n')
            .Select(word => word.Trim());
        return words;
    }
}