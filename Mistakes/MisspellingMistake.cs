namespace SpeechPurifier.Mistakes;

public class MisspellingMistake : IMistake
{
    public int Weight => 5;
    public string Entry { get; init; }
    public MisspellingMistake(string entry) => Entry = entry;
}