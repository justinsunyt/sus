# Run this script to use a local copy of sus-framework rather than fetching it from nuget.
# It expects the sus-framework directory to be at the same level as the sus directory
#
# https://github.com/ppy/sus-framework/wiki/Testing-local-framework-checkout-with-other-projects

$GAME_CSPROJ="sus.Game/sus.Game.csproj"
$ANDROID_PROPS="sus.Android.props"
$IOS_PROPS="sus.iOS.props"
$SLN="sus.sln"

dotnet remove $GAME_CSPROJ reference ppy.sus.Framework;
dotnet remove $ANDROID_PROPS reference ppy.sus.Framework.Android;
dotnet remove $IOS_PROPS reference ppy.sus.Framework.iOS;

dotnet sln $SLN add ../sus-framework/sus.Framework/sus.Framework.csproj `
    ../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj `
    ../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj `
    ../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj;

dotnet add $GAME_CSPROJ reference ../sus-framework/sus.Framework/sus.Framework.csproj;
dotnet add $ANDROID_PROPS reference ../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj;
dotnet add $IOS_PROPS reference ../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj;

# workaround for dotnet add not inserting $(MSBuildThisFileDirectory) on props files
(Get-Content "sus.Android.props") -replace "`"..\\sus-framework", "`"`$(MSBuildThisFileDirectory)..\sus-framework" | Set-Content "sus.Android.props"
(Get-Content "sus.iOS.props") -replace "`"..\\sus-framework", "`"`$(MSBuildThisFileDirectory)..\sus-framework" | Set-Content "sus.iOS.props"

# needed because iOS framework nupkg includes a set of properties to work around certain issues during building,
# and those get ignored when referencing framework via project, threfore we have to manually include it via props reference.
(Get-Content "sus.iOS.props") |
    Foreach-Object {
        if ($_ -match "</Project>")
        {
            "  <Import Project=`"`$(MSBuildThisFileDirectory)../sus-framework/sus.Framework.iOS.props`"/>"
        }

        $_
    } | Set-Content "sus.iOS.props"

$TMP=New-TemporaryFile

$SLNF=Get-Content "sus.Desktop.slnf" | ConvertFrom-Json
$SLNF.solution.projects += ("../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj")
ConvertTo-Json $SLNF | Out-File $TMP -Encoding UTF8
Move-Item -Path $TMP -Destination "sus.Desktop.slnf" -Force

$SLNF=Get-Content "sus.Android.slnf" | ConvertFrom-Json
$SLNF.solution.projects += ("../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj", "../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj")
ConvertTo-Json $SLNF | Out-File $TMP -Encoding UTF8
Move-Item -Path $TMP -Destination "sus.Android.slnf" -Force

$SLNF=Get-Content "sus.iOS.slnf" | ConvertFrom-Json
$SLNF.solution.projects += ("../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj", "../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj")
ConvertTo-Json $SLNF | Out-File $TMP -Encoding UTF8
Move-Item -Path $TMP -Destination "sus.iOS.slnf" -Force
