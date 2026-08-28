# Sentinel Web API
## Introduction:
The Sentinel Web API is a system that serves the purpose of receiving sensor readings from the Sentinel Monitoring System (still in development). The API can register users, their devices, and each device's respective telemetry readings, events, and status.
### Ownership Model
Users can have one or more devices. A device has one DeviceStatus, many Telemetry readings, and many Events. Authentication and authorization are in effect with every model. Registered users can see only their devices, their sensor readings, events, and the device status. \
An administrator has access to all users and all devices registered. 
### Authentication and Authorization Details
Most endpoints are authenticated and only accessible to logged in users. Only the POST Telemetry endpoint is not authenticated because it will be accessed by the microcontroller sending the telemetry readings. Adding authentication to this endpoints adds unnecessary complexity and latency. \
Only admins are authorized to delete telemetry records, delete device status, delete an event, and perform any functions on the users. \
To promote a user registered to admin, it must be done through an SQL query inside the database. 
```sql
UPDATE Users SET Role = 0 WHERE Username = '{username}';
```
### Process Explanation
A user registers their username and password. Then they add the device and name it. Upon being added to the server, the device starts to automatically send telemetry readings and generates its device status record. The user can view the telemetry readings sent by their device. They can also see the device status whenever they want. An event is created whenever the user changes the mode of the device, or when the device senses movement. This is generated automatically. 

## Dependencies:
All dependencies are restored automatically via `dotnet restore`. All packages listed below:
| Package | Version | Purpose |
|---|---|---|
| AutoMapper | 13.0.1 | Mapping entities to DTOs |
| BCrypt.Net-Next | 4.2.0 | Password hashing |
| FluentValidation.AspNetCore | 11.3.1 | Input validation for user request |
| Microsoft.AspNetCore.Authentication.JwtBearer | 10.0.11 | JWT authentication | 
| Microsoft.EntityFrameworkCore | 10.0.11 | Database access |
| Microsoft.EntityFrameworkCore.Design | 10.0.11 | EF Core migrations tools |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.11 | EF Core for SQL Server | 
| Swashbuckle.AspNetCore | 10.2.3 | Testing endpoints via Swagger UI |

**Requires .NET 10 SDK and a running SQL Server on your local machine.**
## Configuration:
### Database Connection
The project uses an SQL Server database. Add your SQL Server connection string in appsettings.json:

```json
{
"ConnectionStrings": {
    "Default": "Your connection string"
  }
}
```
### JWT Configuration
The project uses JWT for authentication configure JWT settings in user-secrets. Run the following in the SentinelSystemApi.Api directory

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Secret" "your own 32+ character string"
dotnet user-secrets set "Jwt:Issuer" "SentinelSystemApi"
dotnet user-secrets set "Jwt:Audience" "SentinelSystemApiUsers"
dotnet user-secrets set "Jwt:ExpiryMinutes" "60"
```
### Database migration
To run the migration to your own database, run this command
```bash
dotnet ef database update
```

### Running the project
To run the project, use
```bash
dotnet run
```
Once running, you can test the API using Swagger UI at `http://localhost:{port}/swagger`. Check your console for the port.\
You can register a user using the register endpoint `POST api/Auth/Register`, logging in through `POST api/Auth/Login`, copying the JWT from the response and pasting it into the authorize button top right of the page in Swagger UI. 
