cd ~/LaboratoryWork5/IdentityServer
dotnet IdentityServer.dll --urls http://0.0.0.0:5001 &
cd ../WebApplication
dotnet WebApplication.dll --urls http://0.0.0.0:5002 &