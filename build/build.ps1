$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project =  "$root/src/WindowsFormsGenericHost"
$output = "$root/artifacts"
dotnet pack "$project" --configuration Release --output "$output"
