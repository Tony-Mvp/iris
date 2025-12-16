using System;
using System.Threading.Tasks;
using IRIS.Core;
using IRIS.FileSystemManager;
using IRIS.CommandExecutor;
using IRIS.Logger;

namespace IRIS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LogManager.Initialize("logs/iris.log");
            LogManager.Log("IRIS application starting...");

            var aiInterpreter = new AIInterpreter();
            var fileScanner = new DirectoryScanner();
            var commandExecutor = new CommandExecutor();

            Console.WriteLine("Sanning file system...");
            var systemMap =  fileScanner.ScanSystem("/home"); // for security
            Console.WriteLine($"scanned directories: {systemMap.Directories.Count}, files: {systemMap.Count}");

            while (true)
            {
                Console.Write("Enter your command: ");
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Please enter a valid command.");
                    continue;
                }

                LogManager.Log($"User input: {userInput}");

                var aiResponse = await aiInterpreter.InterpretCommand(userInput, systemMap);
                LogManager.Log($"AI response: {aiResponse}");

                var executionResult = commandExecutor.Execute(aiResponse);
                LogManager.Log($"Execution result: {executionResult}");

                Console.WriteLine($"Result: {executionResult}");
            }
        }
    }
}