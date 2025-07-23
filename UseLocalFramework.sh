#!/bin/sh

# Run this script to use a local copy of sus-framework rather than fetching it from nuget.
# It expects the sus-framework directory to be at the same level as the sus directory
#
# https://github.com/ppy/sus-framework/wiki/Testing-local-framework-checkout-with-other-projects

GAME_CSPROJ="sus.Game/sus.Game.csproj"
ANDROID_PROPS="sus.Android.props"
IOS_PROPS="sus.iOS.props"
SLN="sus.sln"

dotnet remove $GAME_CSPROJ reference ppy.sus.Framework
dotnet remove $ANDROID_PROPS reference ppy.sus.Framework.Android
dotnet remove $IOS_PROPS reference ppy.sus.Framework.iOS

dotnet sln $SLN add ../sus-framework/sus.Framework/sus.Framework.csproj \
    ../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj \
    ../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj \
    ../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj

dotnet add $GAME_CSPROJ reference ../sus-framework/sus.Framework/sus.Framework.csproj
dotnet add $ANDROID_PROPS reference ../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj
dotnet add $IOS_PROPS reference ../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj

# workaround for dotnet add not inserting $(MSBuildThisFileDirectory) on props files
sed -i.bak 's:"..\\sus-framework:"$(MSBuildThisFileDirectory)..\\sus-framework:g' ./sus.Android.props && rm sus.Android.props.bak
sed -i.bak 's:"..\\sus-framework:"$(MSBuildThisFileDirectory)..\\sus-framework:g' ./sus.iOS.props && rm sus.iOS.props.bak

# needed because iOS framework nupkg includes a set of properties to work around certain issues during building,
# and those get ignored when referencing framework via project, threfore we have to manually include it via props reference.
sed -i.bak '/<\/Project>/i\
  <Import Project=\"$(MSBuildThisFileDirectory)../sus-framework/sus.Framework.iOS.props\"/>\
' ./sus.iOS.props && rm sus.iOS.props.bak

tmp=$(mktemp)

jq '.solution.projects += ["../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj"]' sus.Desktop.slnf > $tmp
mv -f $tmp sus.Desktop.slnf

jq '.solution.projects += ["../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj", "../sus-framework/sus.Framework.Android/sus.Framework.Android.csproj"]' sus.Android.slnf > $tmp
mv -f $tmp sus.Android.slnf

jq '.solution.projects += ["../sus-framework/sus.Framework/sus.Framework.csproj", "../sus-framework/sus.Framework.NativeLibs/sus.Framework.NativeLibs.csproj", "../sus-framework/sus.Framework.iOS/sus.Framework.iOS.csproj"]' sus.iOS.slnf > $tmp
mv -f $tmp sus.iOS.slnf
