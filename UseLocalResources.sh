CSPROJ="sus.Game/sus.Game.csproj"
SLN="sus.sln"

dotnet remove $CSPROJ package ppy.sus.Game.Resources;
dotnet sln $SLN add ../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj
dotnet add $CSPROJ reference ../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj

SLNF="sus.Desktop.slnf"
TMP=$(mktemp)
jq '.solution.projects += ["../sus-resources/sus.Game.Resources/sus.Game.Resources.csproj"]' $SLNF > $TMP
mv -f $TMP $SLNF
