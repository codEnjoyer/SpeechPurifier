namespace SpeechPurifier.Analyzer;

public class TextAnalyzerConfiguration
{
    public HashSet<string> BadWords { get; private set; }

    public TextAnalyzerConfiguration(HashSet<string>? badWords = null)
    {
        BadWords = badWords ?? new HashSet<string>(GetBadWords());
    }
    
    private IEnumerable<string> GetBadWords()
    {
        using var sr = new StreamReader("../../../swears.txt");
        var words = sr.ReadToEnd().Split('\n')
            .Select(word => word.Trim());
        return words;
    }
}