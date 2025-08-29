# 🚀 Manual de Instalação - Challenge API

## ⚡ Instalação Rápida (Recomendado)

### **Windows:**
```bash
# Duplo clique no arquivo:
install-containers.bat
```

### **Linux/Mac:**
```bash
chmod +x install-containers.sh
./install-containers.sh
```

---

## 📋 Instalação Manual

### **1. Pré-requisitos**
- Docker instalado e funcionando

### **2. Baixar imagens**
```bash
docker pull lucasmarcoms/challenge-api:v1.0
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

### **3. Criar SQL Server**
```bash
docker run -d --name servicecontrol-sqlserver \
  -e "ACCEPT_EULA=Y" \
  -e "SA_PASSWORD=YourStrong@Passw0rd" \
  -e "MSSQL_PID=Express" \
  -p 1433:1433 \
  -v sqlserver_data:/var/opt/mssql \
  --restart unless-stopped \
  mcr.microsoft.com/mssql/server:2022-latest
```

### **4. Aguardar 30 segundos e criar API**
```bash
docker run -d --name servicecontrol-api \
  -p 5000:8080 \
  -e "ConnectionStrings__DefaultConnection=Server=servicecontrol-sqlserver;Database=ServiceControlDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true" \
  --link servicecontrol-sqlserver \
  --restart unless-stopped \
  lucasmarcoms/challenge-api:v1.0
```

---

## 🌐 Acessar

- **Swagger:** http://localhost:5000/swagger
- **SQL Server:** localhost,1433 (sa/YourStrong@Passw0rd)

---

## 🔧 Comandos Úteis

```bash
# Ver containers
docker ps

# Ver logs
docker logs servicecontrol-api

# Parar tudo
docker stop servicecontrol-api servicecontrol-sqlserver

# Iniciar tudo
docker start servicecontrol-sqlserver servicecontrol-api
```

---

## 🎯 Tecnologias

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Arquitetura DDD**
- **Docker**

---

## Caso haja erros

Se algo não funcionar:
1. Verifique se o Docker está rodando
2. Execute `docker ps` para ver status
3. Execute `docker logs servicecontrol-api` para ver erros

