var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Routing

// "/shirts"
app.MapGet("/shirts", () =>
{
    return "Reading all the shirts";
    //return new[]
    //{
    //    new { Id = 1, Name = "Red Shirt", Size = "M" },
    //    new { Id = 2, Name = "Blue Shirt", Size = "L" },
    //    new { Id = 3, Name = "Green Shirt", Size = "S" }
    //};
});

app.MapGet("/shirts/{id}", (int id) =>
{
    return $"Reading shirt with ID: {id}";
    //return new { Id = id, Name = $"Shirt {id}", Size = "M" };
});

app.MapPost("/shirts", () =>
{
    return $"Creating a shirt";
    // In a real application, you would save the shirt to a database here
});

app.MapPut("/shirts/{id}", (int id) =>
{
    return $"Updating shirt with ID: {id}";
    // In a real application, you would update the shirt in the database here
});

app.MapDelete("/shirts/{id}", (int id) =>
{
    return $"Deleting shirt with ID: {id}";
    // In a real application, you would delete the shirt from the database here
});

app.Run();
