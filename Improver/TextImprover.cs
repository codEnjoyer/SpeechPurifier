using System.Text;
using NHunspell;
using SpeechPurifier.Analyzer;
using SpeechPurifier.Mistakes;

namespace SpeechPurifier.Improver;

public class TextImprover : ISpeechImprover<TextAnalyzeResult>
{
    public IReadOnlyDictionary<string, Func<string, string>> SingleMistakeReactions { get; }
    public IReadOnlyDictionary<string, Func<IEnumerable<string>, string>> MultipleMistakeReactions { get; }
    private readonly Hunspell _wordSpeller = new("../../../ru_RU.aff", "../../../ru_RU.dic");

    public TextImprover(IReadOnlyDictionary<string, Func<string, string>>? singleMistakeReactions = null,
        IReadOnlyDictionary<string, Func<IEnumerable<string>, string>>? multipleMistakeReactions = null)
    {
        SingleMistakeReactions = singleMistakeReactions ??
                                 new Dictionary<string, Func<string, string>>
                                     (GetDefaultSingleMistakeReactions());
        MultipleMistakeReactions = multipleMistakeReactions ??
                                   new Dictionary<string, Func<IEnumerable<string>, string>>
                                       (GetDefaultMultipleMistakeReactions());
    }

    public string GetRecommendation(TextAnalyzeResult analyzeResult)
    {
        var sb = new StringBuilder();
        foreach (var (mistakeName, mistakes) in analyzeResult.Mistakes)
        {

            if (mistakes.Count == 1)
            {
                var singleMistakeReaction = SingleMistakeReactions[mistakeName];
                sb.AppendLine(singleMistakeReaction(mistakes.First().Entry));
            }
            else
            {
                var multipleMistakeReaction = MultipleMistakeReactions[mistakeName];
                var mistakesEntries = mistakes.Select(mistake => mistake.Entry);
                sb.AppendLine(multipleMistakeReaction(mistakesEntries));
            }
        }
        return sb.ToString();
    }

    private Dictionary<string, Func<string, string>> GetDefaultSingleMistakeReactions() => new()
    {
        {nameof(BadWordMistake), entry => 
            $"\"{entry}\" - это плохое слово. Постарайся заменить его на цензурное."},
        {nameof(SpellingMistake), entry => 
            $"Слово \"{entry}\" написано неправильно. " +
            $"Его правильное написание: \"{_wordSpeller.Suggest(entry).First()}\"."}
    };
    
    private Dictionary<string, Func<IEnumerable<string>, string>> GetDefaultMultipleMistakeReactions() => new()
    {
        {nameof(BadWordMistake), entries => 
            $"\"{string.Join(", ", entries)}\" - это плохие слова. Постарайся заменить их на цензурные."},
        {nameof(SpellingMistake), MultipleSpellingMistakeReaction
        }
    };

    private string MultipleSpellingMistakeReaction(IEnumerable<string> entries)
    {
        var enumerable = entries.ToList();
        var mappings = new List<string>();
        foreach (var word in enumerable)
        {
            var correctWord = _wordSpeller.Suggest(word).First();
            mappings.Add($"\"{word}\" -> \"{correctWord}\"");
        }
        return
            $"Слова \"{string.Join(", ", enumerable)}\" написаны неправильно. " +
            $"Их правильное написание: {string.Join(", ", mappings)}.";
    }
}