# Squip

## Database Setup

This project uses PostgreSQL with Entity Framework Core. Follow these steps to set up the database locally:

### 1. Install PostgreSQL

Download and install PostgreSQL from [postgresql.org](https://www.postgresql.org/download/).

For Windows, you can also use the installer or Docker:

```bash
docker run --name squip-postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d <password_here>
```

### 2. Create the Database

Connect to PostgreSQL and create a database:

```sql
CREATE DATABASE squip;
```

Or using Docker:

```bash
docker exec -it squip-postgres psql -U postgres -c "CREATE DATABASE squip;"
```

### 3. Configure Connection String

Add the connection string to your local configuration:

```bash
cd Squip.Rest
dotnet user-secrets set "ConnectionStrings:SquipDatabase" "Host=localhost;Database=squip;Username=postgres;Password=<password_here>"
```

### 4. Run Migrations

Apply the Entity Framework migrations to create the database schema:

```bash
cd Squip.Rest
dotnet ef database update
```

This will create all necessary tables (Ideas, Users, Habits, Logs, Moods, Targets, etc.).
