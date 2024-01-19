using SpeechPurifier.Mistakes;

namespace SpeechPurifier.Analyzer;

public class TextAnalyzeResult
{
    private readonly int _minPurityScore;
    private readonly int _maxPurityScore;
    public IReadOnlyList<IMistake> Mistakes { get; }
    public int PurityScore => GetPurityScore();

    public TextAnalyzeResult(IReadOnlyList<IMistake> mistakes, int minPurityScore = 0, int maxPurityScore = 100)
    {
        Mistakes = mistakes;
        _minPurityScore = minPurityScore;
        _maxPurityScore = maxPurityScore;
    }
    
    private int GetPurityScore()
    {
        var totalBadScore = Mistakes.Sum(mistake => mistake.Weight);
        return Math.Clamp(_maxPurityScore - totalBadScore, _minPurityScore, _maxPurityScore);
    }

    public override string ToString() =>
        $"{PurityScore} out of {_maxPurityScore}";
}