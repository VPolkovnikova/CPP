cd ~/Downloads
curl https://download.visualstudio.microsoft.com/download/pr/69d7c726-56c4-4652-94e5-4e10a5ac846f/4ef542bc620666656a74d0f6e2235fb8/dotnet-sdk-8.0.100-rc.2.23502.2-osx-x64.pkg -o dotnet-installer.pkg -L
installer -pkg dotnet-installer.pkg -target /
rm dotnet-installer.pkg

cd ~/Desktop
mkdir LaboratoryWork4
cd LaboratoryWork4
dotnet new tool-manifest
dotnet tool install VPolkovnikova --add-source http://10.0.2.2:5000/v3/index.json --no-cache