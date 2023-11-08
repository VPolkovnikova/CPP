curl https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip -o BaGet.zip -L
mkdir BaGet
tar zxf BaGet.zip -C BaGet
del BaGet.zip
cd BaGet
start /b dotnet Baget.dll

cd ..\Application
dotnet pack -o .
dotnet nuget push VPolkovnikova.1.0.0.nupkg -s http://localhost:5000/v3/index.json
del VPolkovnikova.1.0.0.nupkg
rmdir bin /s /q
rmdir obj /s /q
cd ..\Logics
rmdir bin /s /q
rmdir obj /s /q

cd ..
vagrant up linux
vagrant halt linux
set FIRST_BOOT=0
vagrant up mac
vagrant halt mac
set FIRST_BOOT=
vagrant up mac
vagrant halt mac
vagrant up windows
vagrant halt windows

taskkill /f /fi "MODULES eq Baget.dll"
rmdir BaGet /s /q