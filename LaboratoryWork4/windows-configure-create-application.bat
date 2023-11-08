cd %USERPROFILE%\Desktop
mkdir LaboratoryWork4
cd LaboratoryWork4
dotnet new tool-manifest
dotnet tool install VPolkovnikova --add-source http://10.0.2.2:5000/v3/index.json