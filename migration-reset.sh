dotnet ef migrations remove --startup-project Squip.Api --project Squip.EntityFramework
dotnet ef migrations add InitialCreate --startup-project Squip.Api --project Squip.EntityFramework
dotnet ef database drop --startup-project Squip.Api --project Squip.EntityFramework
dotnet ef database update --startup-project Squip.Api --project Squip.EntityFramework
