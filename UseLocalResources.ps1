$CSPROJ="sus.Game/sus.Game.csproj"
$SLN="sus.sln"

dotnet remove $CSPROJ package ppy.sus.Game.Resources;
dotnet sln $SLN add ../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj
dotnet add $CSPROJ reference ../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj

$SLNF=Get-Content "sus.Desktop.slnf" | ConvertFrom-Json
$TMP=New-TemporaryFile
$SLNF.solution.projects += ("../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj")
ConvertTo-Json $SLNF | Out-File $TMP -Encoding UTF8
Move-Item -Path $TMP -Destination "sus.Desktop.slnf" -Force
