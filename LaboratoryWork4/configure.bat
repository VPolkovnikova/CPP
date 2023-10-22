curl https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip -o BaGet.zip -L
mkdir BaGet
tar zxf BaGet.zip -C BaGet
del BaGet.zip
start dotnet BaGet\Baget.dll

cd Application
dotnet pack -o .
dotnet nuget push VPolkovnikova.1.0.0.nupkg -s http://localhost:5000/v3/index.json
del VPolkovnikova.1.0.0.nupkg
rmdir bin /s /q
rmdir obj /s /q
cd ..\Logics
rmdir bin /s /q
rmdir obj /s /q

cd ..
set VAGRANT_HOME=.vagrant.d
vagrant up