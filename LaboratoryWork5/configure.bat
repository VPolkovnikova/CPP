cd IdentityServer
dotnet publish -c Release -r linux-x64 -o ..\LaboratoryWork5\IdentityServer
rmdir bin /s /q
rmdir obj /s /q

cd ..\WebApplication
dotnet publish -c Release -r linux-x64 -o ..\LaboratoryWork5\WebApplication
rmdir bin /s /q
rmdir obj /s /q

cd ..\Logics
rmdir bin /s /q
rmdir obj /s /q

cd ..
vagrant up
rmdir LaboratoryWork5 /s /q
mkdir LaboratoryWork5