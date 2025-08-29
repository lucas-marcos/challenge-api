@echo off
echo ========================================
echo    INSTALADOR AUTOMATICO - CHALLENGE API
echo ========================================
echo.

echo [1/6] Verificando Docker...
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERRO: Docker nao esta instalado ou nao esta funcionando!
    echo Por favor, instale o Docker Desktop e reinicie o computador.
    pause
    exit /b 1
)
echo Docker encontrado! ✓

echo.
echo [2/6] Baixando imagem do SQL Server...
docker pull mcr.microsoft.com/mssql/server:2022-latest
if %errorlevel% neq 0 (
    echo ERRO: Falha ao baixar a imagem do SQL Server!
    pause
    exit /b 1
)
echo Imagem baixada com sucesso! ✓

echo.
echo [3/6] Baixando sua API do Docker Hub...
docker pull lucasmarcoms/challenge-api:v1.0
if %errorlevel% neq 0 (
    echo ERRO: Falha ao baixar a imagem da API!
    pause
    exit /b 1
)
echo API baixada com sucesso! ✓

echo.
echo [4/6] Criando container do SQL Server...
docker run -d --name servicecontrol-sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" -e "MSSQL_PID=Express" -e "MSSQL_TCP_PORT=1433" -p 1433:1433 -v sqlserver_data:/var/opt/mssql --restart unless-stopped mcr.microsoft.com/mssql/server:2022-latest
if %errorlevel% neq 0 (
    echo ERRO: Falha ao criar o container do SQL Server!
    pause
    exit /b 1
)
echo Container criado com sucesso! ✓

echo.
echo [5/6] Aguardando inicializacao do SQL Server...
echo Aguarde 30 segundos para o SQL Server inicializar completamente...
timeout /t 30 /nobreak >nul

echo.
echo [6/6] Criando container da API conectado ao SQL Server...
docker run -d --name servicecontrol-api -p 5000:8080 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ConnectionStrings__DefaultConnection=Server=servicecontrol-sqlserver;Database=ServiceControlDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true" --link servicecontrol-sqlserver --restart unless-stopped lucasmarcoms/challenge-api:v1.0
if %errorlevel% neq 0 (
    echo ERRO: Falha ao criar o container da API!
    pause
    exit /b 1
)
echo API criada com sucesso! ✓

echo.
echo ========================================
echo    INSTALACAO CONCLUIDA COM SUCESSO!
echo ========================================
echo.
echo Aguardando inicializacao da API...
timeout /t 15 /nobreak >nul

echo.
echo Verificando status dos containers...
docker ps
echo.
echo ========================================
echo    CONFIGURACAO COMPLETA!
echo ========================================
echo.
echo SQL Server esta rodando na porta 1433
echo API esta rodando na porta 5000
echo.
echo Para conectar no banco:
echo 1. Abra o SSMS ou Azure Data Studio
echo 2. Server: localhost,1433
echo 3. Login: sa / Senha: YourStrong@Passw0rd
echo 4. Execute: CREATE DATABASE ServiceControlDB;
echo.
echo Para acessar o Swagger:
echo http://localhost:5000/swagger
echo.
echo Para ver logs da API:
echo docker logs servicecontrol-api
echo.
echo Para ver logs do SQL Server:
echo docker logs servicecontrol-sqlserver
echo.
pause
