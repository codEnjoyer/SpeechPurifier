﻿using System.Text.RegularExpressions;
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

    public TextAnalyzer(TextAnalyzerConfiguration? configuration = null)
    {
        _configuration = configuration ?? new TextAnalyzerConfiguration();
        _spellingAnalyzer = new Hunspell("../../../ru_RU.aff", "../../../ru_RU.dic");
    }

    public TextAnalyzeResult Analyze(string text)
    {
        var sentences = SentenceEnd().Split(text);
        var wordsSequence = sentences
            .SelectMany(sentence => WordSeparator().Split(sentence))
            .Where(word => !string.IsNullOrWhiteSpace(word));
        var mistakes = GetMistakesEntries(wordsSequence);
        return new TextAnalyzeResult(mistakes);
    }
    
    private Dictionary<string, List<IMistake>> GetMistakesEntries(IEnumerable<string> words)
    {
        var mistakes = new Dictionary<string, List<IMistake>>();
        foreach (var word in words)
        {
            if (_configuration.BadWords.Contains(word.ToLower()))
                AddMistake(new BadWordMistake(word), mistakes);
            else if (!_spellingAnalyzer.Spell(word))
                AddMistake(new SpellingMistake(word), mistakes);
        }
        return mistakes;
    }
    
    private static void AddMistake(IMistake mistake, IDictionary<string, List<IMistake>> mistakes)
    {
        var mistakeName = mistake.GetType().Name;
        if (!mistakes.ContainsKey(mistakeName))
            mistakes.Add(mistakeName, new List<IMistake> {mistake});
        else
            mistakes[mistakeName].Add(mistake);
    }
}