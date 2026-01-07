@echo off
echo ========================================
echo    PretoBoost 2.0 - Build Script
echo ========================================
echo.

echo [1/4] Verificando .NET SDK...
dotnet --version
if errorlevel 1 (
    echo ERRO: .NET SDK nao encontrado!
    echo Por favor, instale o .NET 8.0 SDK de: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo.
echo [2/4] Restaurando dependencias...
dotnet restore
if errorlevel 1 (
    echo ERRO: Falha ao restaurar dependencias!
    pause
    exit /b 1
)

echo.
echo [3/4] Compilando em modo Release...
dotnet build -c Release
if errorlevel 1 (
    echo ERRO: Falha na compilacao!
    pause
    exit /b 1
)

echo.
echo [4/4] Publicando executavel unico...
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:EnableWindowsTargeting=true --self-contained true
if errorlevel 1 (
    echo ERRO: Falha na publicacao!
    pause
    exit /b 1
)

echo.
echo ========================================
echo    BUILD CONCLUIDO COM SUCESSO!
echo ========================================
echo.
echo O executavel foi gerado em:
echo bin\Release\net8.0-windows\win-x64\publish\PretoBoost.exe
echo.
pause
