$base = (Get-Item ..).FullName
$src = Join-Path $base 'src'
$pkg = Join-Path $base 'pkg'
$out = Join-Path $base 'out'
$nuget = "$src\.nuget\NuGet.exe"

Task Default -depends Initialize-Directories, Create-NuGetPackages

Task Initialize-Directories {
  if(Test-Path $out) {
    Remove-Item -Recurse -Force $out
  }
  
  New-Item $out -Type directory  
}

Task Create-NuGetPackages -depends Build {
	Push-Location $out
	Get-ChildItem $pkg -Filter *.nuspec | ForEach {
	  $spec = $_.FullName
	  Exec { Invoke-Expression "$nuget pack $spec" }

	}
	Pop-Location
}

Task Build {
	Exec { msbuild "$src\Web.Optimization.sln" /t:Build /p:Configuration=Release }
}