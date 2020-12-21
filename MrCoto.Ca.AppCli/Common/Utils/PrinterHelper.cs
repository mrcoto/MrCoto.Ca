using System;
using MrCoto.Ca.Application.Common.Exceptions;
using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.AppCli.Common.Utils
{
    public class PrinterHelper
    {
        public void PrintTitle(string title, ConsoleColor color = ConsoleColor.Green)
        {
            var tabLen = 8;
            var titleLen = title.Length + tabLen;
            var header = new string('=', titleLen);
            Print(header + "\n    " + title + "    \n" + header, color);
        }

        public void PrintWarning(string message) => Print(message, ConsoleColor.Yellow);

        public void PrintException(Exception e)
        {
            var defaultCode = e is BusinessException businessException
                ? businessException.ExceptionCode
                : "SYS:SERVER_ERROR";
            var exceptionMessage = e.Message;
            var message = $"\n[ERROR] {defaultCode} | {exceptionMessage}";
            if (e is ValidationException validationException)
            {
                var errors = validationException.Errors;
                message += "\n\nError List\n==========\n";
                foreach (var detail in errors)
                {
                    message += detail.Key + ": " + string.Join(", ", detail.Value) + "\n";
                }
            } 
            Print(message, ConsoleColor.Red);
        }

        public void PrintInfo(string message) => Print(message, ConsoleColor.Green);
        
        public void Print(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}