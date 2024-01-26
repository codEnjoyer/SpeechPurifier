using SpeechPurifier.Analyzer;

namespace SpeechPurifier.Improver;

public interface ISpeechImprover<in TAnalyzeResult>
    where TAnalyzeResult : IAnalyzeResult
{
    public IReadOnlyDictionary<string, Func<string, string>> SingleMistakeReactions { get; }
    public IReadOnlyDictionary<string, Func<IEnumerable<string>, string>> MultipleMistakeReactions { get; }
    public string GetRecommendation(TAnalyzeResult analyzeResult);
}