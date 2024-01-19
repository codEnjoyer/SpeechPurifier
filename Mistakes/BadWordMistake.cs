namespace SpeechPurifier.Mistakes;

public class BadWordMistake : IMistake
{
    public int Weight => 10;
    public string Entry { get; init; }

    public BadWordMistake(string entry) => Entry = entry;
}