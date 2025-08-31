namespace AutomiqoSoftware.Interfaces.DragDropInterfaces;
using AutomiqoSoftware.DTOs.ControllerDTOs.DragDropDTO;
using AutomiqoSoftware.Services.DragDropServices;



public interface IActionHandler { // DND interface for each method in the collection of functions.
    string ActionType { get; } // Type of method to be called
    Task HandleAsyncMethod(ActionResultDto actionResult, LogCollector logCollector); /* Async method which
                                                                                       uses the data stored
                                                                                       via the ActionResultDto
                                                                                       and the logs collected
                                                                                       via the LogCollector to
                                                                                       organize a functional
                                                                                       method.
                                                                                    */
}