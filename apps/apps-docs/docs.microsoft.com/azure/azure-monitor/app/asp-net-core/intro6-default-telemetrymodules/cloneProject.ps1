$source = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro5-adding-telemetry-processors";
$destination = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro6-default-telemetrymodules";

# Remove-Item $destination -Force -Recurse
Copy-Item $source -Destination $destination -Recurse
