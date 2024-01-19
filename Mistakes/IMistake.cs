namespace SpeechPurifier.Mistakes;

public interface IMistake
{
    public int Weight { get; }
    public string Entry { get; init; }
}