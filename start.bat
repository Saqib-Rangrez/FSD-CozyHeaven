@echo off

REM Start your C# API server
echo Starting C# API server...
REM Replace the following command with the command to start your C# API server
start cmd /k dotnet run --project "C:\FSD Project\CozyHavenStayServer\CozyHavenStayServer" 

REM Wait for a moment to let the API server start
timeout /t 2

REM Start your Angular development server
echo Starting Angular development server...
REM Replace the following command with the command to start your Angular development server
cd "C:\FSD Project\CozyHeavenStay"
start cmd /k ng serve --open