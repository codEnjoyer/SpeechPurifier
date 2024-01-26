namespace SpeechPurifier.Mistakes;

public class SpellingMistake : IMistake
{
    public int Weight => 5;
    public string Entry { get; init; }
    public SpellingMistake(string entry) => Entry = entry;
}