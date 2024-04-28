using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Model.Mahasiswa;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<mhsDB>(opt => opt.UseInMemoryDatabase("mahasiswa"));
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

// configure swagger tutorial di https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio-code
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/", async (mhsDB db) =>
{
    // default data using array list 
    try
    {
        var mhs = new Mahasiswa[]
{
        new Mahasiswa { Id = 1, Nama = "Abdillah Aufa Taqiyya", Nim = "1302220131" },
        new Mahasiswa { Id = 2, Nama = "Irvan Dzawin Nuha", Nim = "1302220072" },
        new Mahasiswa { Id = 3, Nama = "Mohammed Yousef Gumilar", Nim = "1302220085" },
        new Mahasiswa { Id = 4, Nama = "Joshua Daniel Simanjuntak", Nim = "1302220072" },
        new Mahasiswa { Id = 5, Nama = "Rakha Galih Nugraha Sukma", Nim = "1302223118" },
};
        db.mhs.AddRange(mhs);


        await db.SaveChangesAsync();
    }
    catch (Exception e)
    {

    }
    // dari aspnetcore.httml
    return Results.Redirect("/swagger"); // redirect to swagger
});

app.MapGet("api/mahasiswa", async (mhsDB db) =>
{



    return Results.Ok(await db.mhs.ToListAsync());
});

app.MapGet("/mahasiswa/{id}", async (mhsDB db, int id) =>
{
    var mhs = await db.mhs.FindAsync(id);
    if (mhs == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(mhs);
});

app.MapPost("/mahasiswa", async (mhsDB db, Mahasiswa mhs) =>
{
    Console.WriteLine(mhs);
    db.mhs.Add(mhs);
    await db.SaveChangesAsync();
    return Results.Created($"/mahasiswa/{mhs.Id}", mhs);
});

app.MapPut("/mahasiswa/{id}", async (mhsDB db, int id, Mahasiswa mhs) =>
{
    if (id != mhs.Id)
    {
        return Results.BadRequest();
    }
    db.Entry(mhs).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/mahasiswa/{id}", async (mhsDB db, int id) =>
{
    var mhs = await db.mhs.FindAsync(id);
    if (mhs == null)
    {
        return Results.NotFound();
    }
    db.mhs.Remove(mhs);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();