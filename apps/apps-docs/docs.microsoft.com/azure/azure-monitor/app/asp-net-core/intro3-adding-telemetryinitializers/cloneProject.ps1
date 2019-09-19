$source = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro3-adding-telemetryinitializers";
$destination = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro4-removing-telemetryinitializers";

# Remove-Item $destination -Force -Recurse
Copy-Item $source -Destination $destination -Recurse
