# Run tests and collect coverage
Write-Host "Running tests and collecting code coverage..."
dotnet test --settings eng/coverlet.runsettings --collect:"Code Coverage" --results-directory CodeCoverageResults

# Procura todos os arquivos de cobertura gerados pelos projetos de teste
$coverageFiles = Get-ChildItem -Path tests -Recurse -Filter coverage.cobertura.xml | Select-Object -ExpandProperty FullName

if (-not $coverageFiles) {
    Write-Error "Nenhum arquivo coverage.cobertura.xml encontrado nos projetos de teste."
    exit 1
}

# Gera o relatório HTML consolidado
$reportDir = "CodeCoverageResults/Report"
reportgenerator -reports:($coverageFiles -join ";") -targetdir:$reportDir -reporttypes:Html

Write-Host "Code coverage report available at: $reportDir\index.html"
start $reportDir/index.html
