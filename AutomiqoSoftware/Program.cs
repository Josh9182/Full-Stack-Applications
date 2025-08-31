using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutomiqoSoftware.Sessions;
using AutomiqoSoftware.Authorization;
using AutomiqoSoftware.OAuth;
using AutomiqoSoftware.Migrations;
using AutomiqoSoftware.Crypto;
using AutomiqoSoftware.Users;


var builder = WebApplication.CreateBuilder(args); /* 
													This creates the API buider as well 
													as allowing command line arguments 
													such as environment settings or feature flags. 
												  */

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); /*
																						Database Integration
																						based on appsettings.json.
																					   */

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)); /* 
											Integrate EF Core ORM via DbContext
											with the connectionString var.
										  */

builder.Services.AddControllers()
        .AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        }); /*
			This block allows for the API to recognize and register all 
			controllers listed. Additionally, the JSON setting allows 
			for property names to be formatted as PascalCase.
			*/


var app = builder.Build(); // Iniates the construction of the application

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); /*
								Creates a temporary service scope in order 
								to get the DbContext while also making sure 
								the database exists, if !exists -> create.
								 */
}

app.MapControllers(); /*
					Maps [ApiController] classes and their routes 
					such as /api/auth/login to the application. 
					Without this line, all controller endpoints 
					will not respond to HTTP requests. 
					*/

app.Run(); /*
		  Launches the application and begins to list 
		  for incoming HTTP requests, activating the entire pipeline.
		  */