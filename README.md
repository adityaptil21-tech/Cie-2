# CarePlus Hospital Appointment Management System
ASP.NET Core MVC (.NET 8) · C# · Bootstrap 5 · JavaScript. No database needed (in-memory sample data).

## Run locally
1. Install the .NET 8 SDK from https://dotnet.microsoft.com/download (check: `dotnet --version`).
2. Unzip the project and open the folder in VS Code (`code .`) or open `HospitalAppointmentSystem.csproj` in Visual Studio 2022.
3. In a terminal inside the project folder:
   ```
   dotnet restore
   dotnet build
   dotnet run
   ```
4. Open the URL printed in the terminal (default http://localhost:5000).
   (Starting from scratch instead? `dotnet new mvc -n HospitalAppointmentSystem`, then copy these files over the generated ones.)

## Feature map
| Requirement | Where |
|---|---|
| Doctor model | `Models/Doctor.cs` |
| `CalculateTotalCharge()` (+₹100) | `Extensions/DoctorExtensions.cs` |
| Async HttpClient (JSONPlaceholder) | `Services/PostService.cs`, `Controllers/PostsController.cs` |
| Cookie (patient name) | `AppointmentsController.Create (POST)`, read in `Create (GET)` and `HomeController.Index` |
| Session (specialization) | `Program.cs` (AddSession/UseSession), set in `AppointmentsController` and `DoctorsController` |
| DoctorId in query string | `/Doctors/Details?doctorId=1`, `/Appointments/Create?doctorId=1` |
| Hidden DoctorId field | `Views/Appointments/Create.cshtml` |
| Response caching (60 s) | `[ResponseCache]` on `DoctorsController.Details` + `AddResponseCaching/UseResponseCaching` |
| IMemoryCache (3 min) | `Services/DoctorService.cs` |

## Troubleshooting
- **"You must install or update .NET to run this application" / target framework error**: edit `<TargetFramework>` in the .csproj to match your SDK (e.g. `net9.0`).
- **Port already in use**: change `applicationUrl` in `Properties/launchSettings.json`, or run `dotnet run --urls http://localhost:5050`.
- **`dotnet` not recognized**: reinstall the SDK and restart the terminal.
- **Posts page shows an error alert**: no internet or JSONPlaceholder blocked; the app handles it gracefully. Click Retry.
- **Icons/fonts/validation scripts missing**: Bootstrap, icons and jQuery validation load from CDNs, so internet access is needed. Server-side validation still works offline.
- **HTTPS certificate warning** (https profile only): run `dotnet dev-certs https --trust`.
- **Doctor details not refreshing**: the page is cached for 60 s by design; wait, or hard-refresh.
- **Build errors after editing**: run `dotnet clean` then `dotnet build`.
