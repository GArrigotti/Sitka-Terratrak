using System;
using Serilog;

namespace Api.Architecture.Console
{
    public static class ConsoleDecorator
    {
        public static void Decorate(this Exception exception, ILogger logger)
        {
            logger.Error($"┌{new string('─', 100)}┐");
            logger.Error($"│{"Exception:".Center()}│");
            logger.Error($"│{exception.Message.Center()}│");
            logger.Error($"└{new string('─', 100)}┘");
        }

        public static string Center(this string content, int window = 100)
        {
            int left = (window - content.Length) / 2;
            int right = window - (left + content.Length);

            return $"{new String(' ', left)}{content}{new String(' ', right)}";
        }
    }
}
