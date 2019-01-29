$local = Get-Location;
$final_local = "C:\Deploy\";
New-Item -ItemType Directory -Force -Path C:\Deploy
$local =  join-path ($local) "RoadStatus";
cd $local;
dotnet publish RoadStatus.csproj -o c:\Deploy -c Release -r win10-x64
cd $final_local;