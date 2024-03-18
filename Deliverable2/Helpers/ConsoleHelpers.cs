public static class ConsoleHelpers
{
    public static void AddPadding()
    {
        "    ".ToConsole();
    }


    public static void ToConsole(this string message)
    {
        Console.WriteLine(message);
    }

    public static string? AskQuestionGetResponse(this string message)
    {
        message.ToConsole();
        return Console.ReadLine();
    }
}