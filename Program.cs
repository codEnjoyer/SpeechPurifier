using SpeechPurifier.Analyzer;

namespace SpeechPurifier;

public static class Program
{
    public static void Main()
    {
        var analyzerConfig = new TextAnalyzerConfiguration();
        var analyzer = new TextAnalyzer(analyzerConfig);
        const string fishText =
            "Задача организации, в особенности же укрепление и развитие структуры требуют от нас анализа форм развития." +
            "С другой стороны постоянный количественный рост и сфера нашей активности требуют определения и " +
            "уточнения существенных финансовых и административных условий. Не следует, однако забывать, что " +
            "начало повседневной работы по формированию позиции позволяет оценить значение модели развития. " +
            "Задача организации, в особенности же рамки и место обучения кадров способствует подготовки и реализации " +
            "модели развития. Значимость этих проблем настолько очевидна, что дальнейшее развитие различных форм " +
            "деятельности позволяет оценить значение новых предложений.";
        var analyzeResult = analyzer.Analyze(fishText);
        Console.WriteLine(analyzeResult);
        Console.ReadKey();
    }
}