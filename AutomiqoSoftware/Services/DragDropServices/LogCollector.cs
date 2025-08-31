using System.Text;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace AutomiqoSoftware.Services.DragDropServices;

public class LogCollector { /* Custom log collector, storing each log as a string[]. 
                               Log type and message are stored and are published once finished.
                               Once published (converted to a string element), the logs are passed
                               through the ActionResultDto and fed to the frontend via an API call.
                            */
    private readonly StringBuilder _sb = new StringBuilder(); // StringBuilder instance.

    public void AutomationStep(string message) { /* Method that returns an appended line with the 
                                                    "AUTOMATION STEP" message type.
                                                 */
        Log("AUTOMATION STEP", message);
    }

    public void Warning(string message) { /* Method that returns an appended line with the 
                                             "WARNING" message type.
                                          */
        Log("WARNING", message);
    }
    public void Error(string message) { /* Method that returns an appended line with the 
                                           "ERROR" message type.
                                        */
        Log("ERROR", message);
    }

    public void Success(string message) { /* Method that returns an appended line with the 
                                             "SUCCESS" message type.
                                          */
        Log("SUCCESS", message);
    }

    public void Log(string messageType, string message) { /* Method to create a new line which 
                                                             includes the date of creation, 
                                                             log type, and log message.
                                                          */
        _sb.AppendLine($"{DateTime.Now:HH:mm:ss} [{messageType}]- {message}");
    }

    public List<string> PublishLogs() { /* Method which returns the array created by the StringBuilder
                                           into a list of new line strings. 
                                        */

        var publishedLogs = _sb.ToString() // Stringified StringBuilder array
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries) /* Split by each new line 
                                                                                  character and disregard 
                                                                                  empty lines.
                                                                               */
                .ToList(); /* Converted .Split() result array into a list format with each element in the 
                              list being a new line string. Basically the format is now a List<string>
                              container with each seperate element [1], [2], etc being a new log line. 
                              This allows for easier rendering by frontend. 
                           */

        return publishedLogs; // return the list. 
    }
}