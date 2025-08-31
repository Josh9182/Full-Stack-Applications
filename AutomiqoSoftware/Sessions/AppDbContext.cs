using Microsoft.EntityFrameworkCore;
using AutomiqoSoftware.Models.Users;

namespace AutomiqoSoftware.Sessions;

public class AppDbContext : DbContext { // Creating a new class, inheriting from EF's DbContext
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } /* Constructor to communciate with EF, 
                                                                                    providing it with necessary options. 
                                                                                    Additionally, using CI to access DbContext
                                                                                    and provide with options parameter using ": base" */
    public DbSet<User> User { get; set; } /* EF Table (DbSet) accessing the .Models User class, 
                                           creating a public property table titled Users
                                           Any other tables should be constructed */
}