using System;
using System.Collections.Generic;

namespace MrCoto.Ca.AppCli.Common.Utils
{
    public class CommandUtil
    {
        private readonly PrinterHelper _printerHelper;
        private readonly PromptHelper _promptHelper;
        private readonly PrinterTableHelper _printerTableHelper;

        public CommandUtil(PrinterHelper printerHelper, PromptHelper promptHelper, PrinterTableHelper printerTableHelper)
        {
            _printerHelper = printerHelper;
            _promptHelper = promptHelper;
            _printerTableHelper = printerTableHelper;
        }

        public void PrintTable(List<string> headers, List<List<string>> data) =>
            _printerTableHelper.PrintTable(headers, data);

        public void PrintTitle(string title, ConsoleColor color = ConsoleColor.Green) =>
            _printerHelper.PrintTitle(title, color);

        public void PrintWarning(string message) => _printerHelper.PrintWarning(message);

        public void PrintException(Exception e) => _printerHelper.PrintException(e);

        public void PrintInfo(string message) => _printerHelper.PrintInfo(message);
        public void Print(string message, ConsoleColor color = ConsoleColor.White) =>
            _printerHelper.Print(message, color);
        
        public string PromptString(string message, ConsoleColor color = ConsoleColor.Yellow) => 
            _promptHelper.PromptString(message, color);
        
        public string PromptPassword(string message, ConsoleColor color = ConsoleColor.Yellow) => 
            _promptHelper.PromptPassword(message, color);
        
        public bool PromptYesNo(string message, bool defaultAnswer = false, ConsoleColor color = ConsoleColor.Yellow) => 
            _promptHelper.PromptYesNo(message, defaultAnswer, color);
        
        public int PromptInt(string message, ConsoleColor color = ConsoleColor.Yellow, int defaultAnswer = 0) => 
            _promptHelper.PromptInt(message, color, defaultAnswer);
    }
}