# Deployment Runbook

This runbook outlines steps to deploy the FakeStoreNet application.

## Prerequisites
- Docker and Docker Compose installed
- .NET 8 SDK if building locally
- Access to container registry (optional)

## Deployment Steps

1. Build Docker images:
   ```bash
   docker-compose build
   ```

2. Start services:
   ```bash
   docker-compose up -d
   ```

3. Verify containers:
   ```bash
   docker ps
   ```

4. Apply database migrations:
   - Connect to the SQL Server container:
     ```bash
     docker exec -it <db_container> /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourPassword>"
     ```
   - Run migration scripts located in `docs/infrastructure/migrations`.

5. Smoke test API endpoints:
   - GET `http://localhost:5000/health`
   - POST `http://localhost:5000/products` with sample payload

6. Rollback
   - To stop and remove containers:
     ```bash
     docker-compose down
     ```
   - To remove volumes if needed:
     ```bash
     docker volume rm -f <volume_name>
     ```

---

Use this runbook to ensure consistent deployment across environments.  
Document any environment-specific variables in `.env` files or CI/CD pipeline configuration.
