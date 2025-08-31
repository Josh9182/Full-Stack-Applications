namespace AutomiqoSoftware.DTOs.ControllerDTOs.OutputDTOs;

public class ActionResultDto { // Output DTO for DispatcherController
  public bool Success { get; }
  public string StatusMessage { get; }
  public List<string> Logs { get; }

}