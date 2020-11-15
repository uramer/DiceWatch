using System.Runtime.CompilerServices;

namespace Dice.Services
{
  /// <summary>
  /// If you want to view logs in the development phase, you need to write log messages using this Logger.
  /// You can then view the logs on both Tizen Log Viewer and Tizen Device Manager in Tools > Tizen menu in Visual Studio.
  /// For more information, see https://docs.tizen.org/application/vstools/tools/log-viewer and https://docs.tizen.org/application/vstools/tools/device-manager.
  ///
  /// To reduce the log output, you can filter logs by Level, Tag, and so on.
  /// For more details, see https://docs.tizen.org/application/vstools/tools/log-viewer#understanding-and-filtering-logs and https://docs.tizen.org/application/vstools/tools/device-manager#understanding-and-filtering-logs.
  ///
  /// In addition, you can find the API reference for Tizen.Log in https://samsung.github.io/TizenFX/stable/api/Tizen.Log.html.
  /// Note that you can see the logs written by Console.WriteLine() or Console.Write() if filtered by 'DOTNET_LAUNCHER' tag.
  /// </summary>
  public static class Logger
  {
    private const string _tag = "Dice";
    private const string prefix = "[Dice]";


    public static void Verbose(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Verbose(_tag, prefix + message, file, func, line);
    }

    public static void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Debug(_tag, prefix + message, file, func, line);
    }

    public static void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Info(_tag, prefix + message, file, func, line);
    }

    public static void Warn(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Warn(_tag, prefix + message, file, func, line);
    }

    public static void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Error(_tag, prefix + message, file, func, line);
    }

    public static void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
    {
      Tizen.Log.Fatal(_tag, prefix + message, file, func, line);
    }
  }
}
