using Chamber.CallBack.Types;
using Events;
using Messages.Handling.Args;
using Microsoft.Speech.Recognition;
using System.Globalization;
using Telegram.Bot.Types;

namespace Chamber.Recogrition;

public static class Recognizer
{

    private static readonly string[] _words =
    [
        "создай обращение"
    ];

    private static Choices AddRande(this Choices choice, string[] words)
    {
        choice.Add(words);
        return choice;
    }
    private static GrammarBuilder AppendChoices(this GrammarBuilder builder, Choices choice)
    {
        builder.Append(choice);
        return builder;
    }

    public static void Start()
    {
        CultureInfo culture = new("ru-RU");

        SpeechRecognitionEngine? speechRecognitionEngine = null;

        foreach (RecognizerInfo info in SpeechRecognitionEngine.InstalledRecognizers())
        {
            if (info.Culture.Equals(culture))
            {
                speechRecognitionEngine = new SpeechRecognitionEngine(info);
                break;
            }
        }

        if (speechRecognitionEngine == null)
        {
            return;
        }

        speechRecognitionEngine?.LoadGrammar(
                new Grammar(
                    new GrammarBuilder().AppendChoices(
                      new Choices().AddRande(_words))));

        if (speechRecognitionEngine == null)
        {
            return;
        }

        speechRecognitionEngine.SetInputToDefaultAudioDevice();
        speechRecognitionEngine.SpeechRecognized += Engine_SpeechRecognized;
    }

    private static void Engine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        string text = e.Result.Text;
        float confidence = e.Result.Confidence;

        if (confidence < 0.6)
        {
            return;
        }

        if (text.ToLower() == "создай обращение")
        {
            PriorityEventHandler.Invoke(new CallBackRecievedArgs(new CallbackQuery()
            {
                Message = new(),
                Data = new CallBackPacket(5082579517, CallBackCode.PrintProblemTypes).Pack()
            }));
        }

    }
}
