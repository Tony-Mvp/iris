using System.Collections.Generic;
using IRIS.Core.Models;
using IRIS.FileSystemManager;

namespace IRIS.Core
{
    public class AiInterpreter
    {
        // Recibe el input y el mapa del sistema
        public InstructionModel ParseInstruction(string input, Dictionary<string, List<string>> systemMap)
        {
            var instruction = new InstructionModel();

            if (input.StartsWith("mover"))
            {
                instruction.Action = "mover";
                var parts = input.Split(" a ");
                if (parts.Length == 2)
                {
                    var src = parts[0].Replace("mover ", "").Replace(" de ", "").Trim();
                    var dst = parts[1].Trim();

                    // Validación básica contra systemMap
                    if (systemMap.ContainsKey(src) || System.IO.File.Exists(src))
                    {
                        instruction.Source = src;
                        instruction.Destination = dst;
                    }
                }
            }
            else if (input.StartsWith("eliminar"))
            {
                instruction.Action = "eliminar";
                var src = input.Replace("eliminar ", "").Trim();
                if (systemMap.ContainsKey(src) || System.IO.File.Exists(src))
                    instruction.Source = src;
            }

            return instruction;
        }
    }
}
