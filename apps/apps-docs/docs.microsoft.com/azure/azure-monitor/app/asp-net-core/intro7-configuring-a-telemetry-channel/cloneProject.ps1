$source = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro6-default-telemetrymodules";
$destination = "C:\Ggg\Ggg.Csharp\apps\apps-docs\docs.microsoft.com\azure\azure-monitor\app\asp-net-core\intro7-configuring-a-telemetry-channel";

# Remove-Item $destination -Force -Recurse
Copy-Item $source -Destination $destination -Recurse
