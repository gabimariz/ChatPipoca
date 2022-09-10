# ChatPipoca Infrastructure library

## Setting up database connection

```C#
/*
	Open Infra/Data/AppDbContext.cs to configure the mariadb database edit USERNAME, PASSWORD 
	data and mariadb o mysql version
*/

options.UseMySql("server=localhost;username=USERNAME;password=PASSWORD;database=url_shortener",
	// Edit Major, Minor and Path version database
	new MariaDbServerVersion(new Version(MAJOR, MINOR, PATH)));
```

### Generating the migrations
```bash

# Open Infra folder and
# Manage the database
$ dotnet ef database update
```
