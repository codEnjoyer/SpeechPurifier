using System.Text.RegularExpressions;
using NHunspell;
using SpeechPurifier.Mistakes;

namespace SpeechPurifier.Analyzer;

public partial class TextAnalyzer
{
    private readonly TextAnalyzerConfiguration _configuration;
    private readonly Hunspell _spellingAnalyzer;

    [GeneratedRegex(@"\.{3,}|[\.!\?…](?=\s)")]
    private static partial Regex SentenceEnd();

    [GeneratedRegex(@"\W|_")]
    private static partial Regex WordSeparator();

    public TextAnalyzer(TextAnalyzerConfiguration configuration)
    {
        _configuration = configuration;
        _spellingAnalyzer = new Hunspell("../../../ru_RU.aff", "../../../ru_RU.dic");
    }

    public TextAnalyzeResult Analyze(string text)
    {
        var sentences = SentenceEnd().Split(text);
        var wordsSequence = sentences
            .SelectMany(sentence => WordSeparator().Split(sentence))
            .Where(word => !string.IsNullOrWhiteSpace(word));
            
        var words = wordsSequence as string[] ?? wordsSequence.ToArray();
        Console.WriteLine(string.Join(", ", words));

        // TODO: Переписать на один цикл
        var badWordsMistakes = GetBadWordsMistakes(words);
        var misspellingMistakes = GetMisspellingErrors(words, _spellingAnalyzer);
        return new TextAnalyzeResult(badWordsMistakes.Concat(misspellingMistakes).ToList());
    }

    private IEnumerable<IMistake> GetBadWordsMistakes(IEnumerable<string> words)
    {
        return words
            .Where(word => _configuration.BadWords.Contains(word.ToLower()))
            .Select(word => new BadWordMistake(word));
    }

    private static IEnumerable<IMistake> GetMisspellingErrors(IEnumerable<string> words, Hunspell hunspell)
    {
        return words
            .Where(word => !hunspell.Spell(word))
            .Select(word => new MisspellingMistake(word));
    }
}