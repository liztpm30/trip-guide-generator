using Microsoft.Azure.Cosmos;
using trip_guide_generator;
using trip_guide_generator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Initialize Cosmos DB 
IConfigurationSection dbConfig = builder.Configuration.GetSection("CosmosDb");
string databaseName = dbConfig.GetSection("DatabaseName").Value;
string containerName = dbConfig.GetSection("ContainerName").Value;
string account = dbConfig.GetSection("Account").Value;
string key = dbConfig.GetSection("Key").Value;
CosmosClient client = new CosmosClient(account, key);
CosmosDBService cosmosDbService = new CosmosDBService(client, databaseName, containerName);
DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");


builder.Services.AddSingleton<ICosmosDBService>(cosmosDbService);
builder.Services.AddScoped<IUserService, UserService>();

//Pass App Settings
builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection(AppOptions.Section));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

