@echo off

FOR /F %%I IN ("%0") DO SET BATDIR=%%~dpI

ECHO.
ECHO Project directory: %BATDIR%

cd /d %BATDIR%

ECHO.
ECHO Stopping ChannelPorts
call %windir%\system32\inetsrv\appcmd stop apppool /apppool.name:"ChannelPorts" | exit 0
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO Removing old publish
call RMDIR /S /Q Publish

ECHO.
ECHO Building Model...
call dotnet build M#\Model\ /nologo
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO Building Domain...
call dotnet build Domain\ /nologo
if ERRORLEVEL 1 (goto error)

:: What do we do if the UI wont build because the website isnt built???
:: A free lunch to whoever solves this entirely in this script
ECHO.
ECHO Building UI...
call dotnet build M#\UI\ /nologo
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO Building Website...
call dotnet build Website\ /nologo
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO Publishing...
call dotnet publish Website\Website.csproj /p:PublishDir="..\Publish"
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO Restarting ChannelPorts
call %windir%\system32\inetsrv\appcmd start apppool /apppool.name:"ChannelPorts"
if ERRORLEVEL 1 (goto error)

ECHO Finished!
exit /b 0

:error
echo ##################################
echo Error occured!
echo Please resolve and run again to publish.
echo ##################################
set /p cont= Press Enter to exit.
exit /b -1