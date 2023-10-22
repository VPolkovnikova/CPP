cd /usr/local/bin
wget https://download.visualstudio.microsoft.com/download/pr/9144f37e-b370-41ee-a86f-2d2a69251652/bc1d544112ec134184a5aec7f7a1eaf9/dotnet-sdk-8.0.100-rc.2.23502.2-linux-x64.tar.gz -O dotnet.tar.gz
mkdir dotnet
tar zxf dotnet.tar.gz -C dotnet
rm dotnet.tar.gz
cd ~
cat > .bashrc << EOL
export DOTNET_ROOT=/usr/local/bin/dotnet
export PATH=$PATH:/usr/local/bin/dotnet
EOL
source .bashrc

mkdir LaboratoryWork4
cd LaboratoryWork4
dotnet new tool-manifest
dotnet tool install VPolkovnikova --add-source http://10.0.2.2:5000/v3/index.json --no-cache