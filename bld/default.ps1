$base = (Get-Item ..).FullName
$src = Join-Path $base 'src'
$pkg = Join-Path $base 'pkg'
$out = Join-Path $base 'out'
$nuget = "$src\.nuget\NuGet.exe"
$version = "0.0.0"

Task Default -depends Initialize-Directories, Create-NuGetPackages

Task Initialize-Directories {
  if(Test-Path $out) {
    Remove-Item -Recurse -Force $out
  }
  
  New-Item $out -Type directory  
}

Task Create-NuGetPackages -depends Build, Set-PackageVersion {
  Push-Location $out
  
  Get-ChildItem $pkg -Filter *.nuspec | ForEach {
    $spec = $_.FullName
    Exec { Invoke-Expression "$nuget pack $spec" }
  }
  
  Pop-Location
}

Task Build {
  #Exec { msbuild "$src\Web.Optimization.sln" /t:Build /p:Configuration=Release }
}

Task Set-PackageVersion {
  $file = "version.xml"
  $xml = [xml](Get-Content $file)

  [string] $suffix = $xml.version.suffix
  [int] $major = $xml.version.major
  [int] $minor = $xml.version.minor
  [int] $patch = [int] $xml.version.patch + 1

  $xml.version.patch = [string] $patch
  $xml.Save($file)

  if ($suffix.Length -gt 0) {
    $version = "$major.$minor.$patch-$suffix"
  }
  else {
    $version = "$major.$minor.$patch"
  }
  
  # Set NuGet package version.
  Get-ChildItem $pkg -Filter *.nuspec | ForEach {
    $spec = [xml](Get-Content $_.FullName)
    $spec.package.metadata.version = $version
	$spec.Save($_.FullName)
  }
}