using SpeechPurifier.Mistakes;

namespace SpeechPurifier.Analyzer;

public interface IAnalyzeResult
{
    public IReadOnlyList<IMistake> Mistakes { get; }
    public int PurityScore { get; }
}