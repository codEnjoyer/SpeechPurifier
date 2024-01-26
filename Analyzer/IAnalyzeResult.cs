using SpeechPurifier.Mistakes;

namespace SpeechPurifier.Analyzer;

public interface IAnalyzeResult
{
    public Dictionary<string, List<IMistake>> Mistakes { get; }
    public int PurityScore { get; }
}