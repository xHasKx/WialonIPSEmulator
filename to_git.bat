@echo off
set t=..\WialonIPSEmulator_git

copy WialonIPSEmulator.sln %t%\WialonIPSEmulator.sln /Y
copy to_git.bat %t%\to_git.bat /Y
mkdir %t%\WialonIPSEmulator
cd WialonIPSEmulator
set t=..\%t%\WialonIPSEmulator
copy *.cs %t%\*.cs /Y
copy *.resx %t%\*.resx /Y
copy *.pdf %t%\*.pdf /Y
copy *.csproj %t%\*.csproj /Y
mkdir %t%\Properties
cd Properties
set t=..\%t%\Properties
copy *.* %t%\*.* /Y

pause