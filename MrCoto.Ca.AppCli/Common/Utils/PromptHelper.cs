using System;
using McMaster.Extensions.CommandLineUtils;

namespace MrCoto.Ca.AppCli.Common.Utils
{
    public class PromptHelper
    {
        public string PromptString(string message, ConsoleColor color = ConsoleColor.Yellow) => 
            Prompt.GetString("> " + message, promptColor: color) ?? "";
        
        public string PromptPassword(string message, ConsoleColor color = ConsoleColor.Yellow) => 
            Prompt.GetPassword("> " + message, promptColor: color);
        
        public bool PromptYesNo(string message, bool defaultAnswer = false, ConsoleColor color = ConsoleColor.Yellow) => 
            Prompt.GetYesNo("> " + message, defaultAnswer, promptColor: color);
        
        public int PromptInt(string message, ConsoleColor color = ConsoleColor.Yellow, int defaultAnswer = 0) => 
            Prompt.GetInt("> " + message, defaultAnswer, promptColor: color);
    }
}