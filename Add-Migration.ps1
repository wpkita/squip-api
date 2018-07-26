Add-Migration Blah -Project SquipApi.EntityFramework -StartupProject SquipApi.WebApi -Context SquipContext
Remove-Migration -Project SquipApi.EntityFramework -StartupProject SquipApi.WebApi -Context SquipContext
Update-Database -Context SquipContext
