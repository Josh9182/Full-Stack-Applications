namespace AutomiqoSoftware.Models.Users;

public class User { // User table, holding all necessary user information.

    public Guid UserID { get; set; } = Guid.NewGuid();
    public string UserEmail { get; set; }
    public string Username { get; set; }
    public string HashedPass { get; set; }
    public string Salt { get; set; }

}