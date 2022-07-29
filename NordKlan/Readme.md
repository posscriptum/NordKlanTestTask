This is very simple app for booking meeting room.
Technologies: 
	C#
	.Net Core 3.1
	Entity Freamwork Core 5.0.17 (for using localdb MS SQL)
	Npgsql Entity Freamwork Core 5.0.10 (for using PosgreSQL)
	ASP.NET Identity
	Razor
	Bootstrap

Adjust connection db in appsetings.json
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=nordklandb;Trusted_Connection=True;" - for localdb  MS SQL
    "DefaultConnection": "Host=localhost;Port={your db port};Database=nordklandb;Username=postgres;Password={your db admin password}" - for PostgreSQL

Adjust connection db in Startup
	services.AddDbContext<Context>(options => options.UseSqlServer(connection)); - for localdb MS SQL
    services.AddDbContext<Context>(options => options.UseNpgsql(connection)); - for PostgreSQL
