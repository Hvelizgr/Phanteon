# Ejecutar en la raíz del proyecto (Windows PowerShell)
$folders = @(
  "Phanteon/Views",
  "Phanteon/ViewModels",
  "Phanteon/Models",
  "Phanteon/Services/Interfaces",
  "Phanteon/Services/Implementations",
  "Phanteon/Helpers",
  "Phanteon/Resources/Styles",
  "Phanteon/Resources/Images",
  "Phanteon/Resources/Fonts",
  "Phanteon/Tests"
)


foreach ($f in $folders) {
    New-Item -ItemType Directory -Force -Path $f | Out-Null
    $gitkeep = Join-Path $f ".gitkeep"
    if (-not (Test-Path $gitkeep)) {
        New-Item -ItemType File -Force -Path $gitkeep | Out-Null
        Set-Content -Path $gitkeep -Value "# Mantener la carpeta en el repositorio"
    }
}
Write-Host "Carpetas creadas y .gitkeep añadidos."