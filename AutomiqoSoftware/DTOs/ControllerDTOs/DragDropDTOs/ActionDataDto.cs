using AutomiqoSoftware.Enumerations;

namespace AutomiqoSoftware.DTOs.ControllerDTOs.DragDropDTOs;

public class ActionDataDto { // Input DTO to filter action data from frontend interaction
    public string ActionType { get; set; } = ""; /* Purpose for each puzzle piece, non nullable.
                                                             If nullable it would result in comp error.  
                                                          */
    public string PrimaryElement { get; set; } /* Primary element for ActionType to work off of. 
                                                           Ex: URL, File Path.
                                                        */
    public string? SecondaryElement { get; set; } /* (*OPTIONAL*) Secondary element to work with primary 
                                                     element. Ex: File Path #2 (Transfer)
                                                  */
    public List<string>? InputValue { get; set; } /* (*OPTIONAL*) Multiple input value for ActionType 
                                                     to work with. Ex: [diapers;milk;laundry detergent;]
                                                  */    
    public InputType? InputType { get; set; } /* (*OPTIONAL*) Input type working off of InputValue 
                                                 for CV Engine to work better. Using Enumeration InputType.
                                                 Ex: Button, Link, etc.
                                              */
    public Dictionary<string, object>? Parameters { get; set; } /* (*OPTIONAL*) Parameters for ActionType 
                                                                   to work off of. Ex: Repeat every 5 secs.
                                                                */
}