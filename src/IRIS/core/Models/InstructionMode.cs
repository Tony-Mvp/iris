namespace IRIS.core.Models
{
   public class InstructionModel
    {
      public string Action { get; set; }
      public string Source { get; set; }
      public string Destination { get; set; }
      public bool RequiresConfirmation { get; set; } = true;
    }
}