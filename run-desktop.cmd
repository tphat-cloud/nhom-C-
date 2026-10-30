@echo off
rem ============================================================
rem  Chay ung dung quan tri (QuanLyDesktop - WinForms)
rem  Chi can 1 lenh: run-desktop.cmd
rem  - Tu nap mat khau SQL tu file .env vao bien moi truong
rem    FASHIONSHOP_SQL_PASSWORD (khong can setx, khong can go tay)
rem  - Tu khoi dong container SQL Server neu chua chay
rem  - Tai khoan dang nhap: admin / 123456
rem ============================================================
setlocal
cd /d "%~dp0"

set "ENV_FILE=%~dp0.env"

if defined FASHIONSHOP_SQL_PASSWORD goto :checkdb

if exist "%ENV_FILE%" (
    for /f "usebackq tokens=1,* delims==" %%A in ("%ENV_FILE%") do (
        if /i "%%A"=="MSSQL_SA_PASSWORD" set "FASHIONSHOP_SQL_PASSWORD=%%B"
    )
)

:checkdb
if not defined FASHIONSHOP_SQL_PASSWORD (
    echo [LOI] Khong tim thay mat khau SQL trong file .env
    echo       Can co dong: MSSQL_SA_PASSWORD=mat-khau-cua-ban
    echo       Hoac chay: dotnet user-secrets set "FASHIONSHOP_SQL_PASSWORD" "mat-khau" --project QuanLyDesktop
    exit /b 1
)

where docker >nul 2>&1
if errorlevel 1 goto :run

set "SQLC="
for /f "delims=" %%S in ('docker compose ps -q sqlserver 2^>nul') do set "SQLC=%%S"
if not defined SQLC (
    echo [..] Container SQL Server chua chay, dang khoi dong...
    docker compose up -d
    timeout /t 5 /nobreak >nul
)

:run
echo [OK] Da nap mat khau SQL tu .env
echo [..] Dang khoi dong QuanLyDesktop - dang nhap: admin / 123456
echo.
dotnet run --project QuanLyDesktop
exit /b %ERRORLEVEL%
