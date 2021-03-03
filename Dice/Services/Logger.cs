using System.Runtime.CompilerServices;

namespace Dice.Services
{
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
