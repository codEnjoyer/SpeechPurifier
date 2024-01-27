using SpeechPurifier.Analyzer;
using SpeechPurifier.Improver;

namespace SpeechPurifier;

public static class Program
{
    public static void Main()
    {
        var analyzer = new TextAnalyzer();
        const string fishText =
            "Задача организации, в особенности же укрепление и развитие структуры требуют от нас анализа форм развития." +
            "С другой стороны постоянный количественный лох и сфера нашей активности требуют определения и " +
            "уточнения существенных финансовых и административных условий. Не следует, однако забывать, что " +
            "начало повседневной работы по формированию позиции позволяет оценить значение модели развития. " +
            "Задача организации, в особеннасти же рамки и обучения кадров способствует подготовки и реализации " +
            "модели развития. Значимость этих проблем настолько очевидна, что дальнейшее развитие различных форм " +
            "деятельности позволяет оценить значение новых предложений.";
        var analyzeResult = analyzer.Analyze(fishText);
        var textImprover = new TextImprover();
        var recommendation = textImprover.GetRecommendation(analyzeResult);
        Console.WriteLine(recommendation);
    }
}