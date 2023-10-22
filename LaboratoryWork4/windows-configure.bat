cd %USERPROFILE%\Downloads
curl https://download.visualstudio.microsoft.com/download/pr/92e8b771-8624-48a6-9ffc-9fda1f301fb4/85b45cdf39b2a773fbf8d5d71c3d4774/dotnet-sdk-8.0.100-rc.2.23502.2-win-x64.exe -o dotnet-installer.exe -L
dotnet-installer.exe
del dotnet-installer.exe

cd %USERPROFILE%\Desktop
mkdir LaboratoryWork4
cd LaboratoryWork4
dotnet new tool-manifest
dotnet tool install VPolkovnikova --add-source http://10.0.2.2:5000/v3/index.json --no-cache