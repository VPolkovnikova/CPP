cd Logics
dotnet pack -o ..\Repository -p:PackageId=VPolkovnikova
rmdir bin /s /q
rmdir obj /s /q
cd ..\Application
dotnet new nugetconfig
dotnet nuget add source ..\Repository
dotnet add package VPolkovnikova
dotnet build