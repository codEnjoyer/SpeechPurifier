using SpeechPurifier.Analyzer;

namespace SpeechPurifier;

public static class Program
{
    public static void Main()
    {
        var analyzerConfig = new TextAnalyzerConfiguration();
        var analyzer = new TextAnalyzer(analyzerConfig);
        var analyzeResult = analyzer.Analyze("Ты дурак! А я крутецкий. Балбес!");
        Console.WriteLine(analyzeResult);
        Console.ReadKey();
    }
}