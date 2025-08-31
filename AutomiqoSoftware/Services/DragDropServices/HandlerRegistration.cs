namespace AutomiqoSoftware.Services.DragDropServices;
using AutomiqoSoftware.DTOs.ControllerDTOs.DragDropDTO;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;

public class HandlerRegistration { /* Class which dynamically calls the ActionType found in
                                      IActionHandler and pairs it with the correct method name.
                                      i.e, "moveFiles" = FileMover.

                                   */
    private readonly Dictionary<string, IActionHandler> _handlerNames; /* Dictionary storing the ActionType
                                                                          as the key while the IActionHandler
                                                                          method instance is the value.
                                                                          
                                                                          Ex: "moveFiles" => new FileMover()
    
                                                                       */

    public HandlerRegistration(IEnumerable<IActionHandler> handlerNames) { /* Constructor to inject
                                                                              _handlerNames dictionary.
                                                                              Injecting as an 
                                                                              IEnumerable (Iteratable object).
                                                                              MUST be IEnumerable because .NET
                                                                              DI system can provide all 
                                                                              interface implementors 
                                                                              but it can only do so 
                                                                              as an IEnumerable. 
                                                                           */

        _handlerNames = handlerNames.ToDictionary(h => h.ActionType); /* Convert DI IEnumerable
                                                                         into dictionary to pair
                                                                         ActionType key with
                                                                         IActionHandler value.
                                                                      */
    }

    public IActionHandler GetHandler(string ActionType) { // Obtain the handler based off the ActionType
        if (_handlerNames.TryGetValue(ActionType, out var handler)) { /* If dictionary has ActionType key
                                                                         input, create a value which outputs
                                                                         the value (TryGetValue).
                                                                      */
            return handler; // return said value.
        }
        else { // If the ActionType doesn't exist, 
            throw new ArgumentException($"Action type: {ActionType} is not currently found " +
                "to pair with a method. Please retry.");
        }
     }
}