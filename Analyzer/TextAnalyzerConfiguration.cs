namespace SpeechPurifier.Analyzer;

public class TextAnalyzerConfiguration
{
    public HashSet<string> BadWords { get; private set; }

    public TextAnalyzerConfiguration(HashSet<string>? badWords = null)
    {
        BadWords = badWords ?? new HashSet<string> {"дурак", "балбес"};
    }
}