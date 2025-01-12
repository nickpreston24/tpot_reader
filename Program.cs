using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        var readmes = GetReadmeFiles();
        var _ = GetFilesWithTpotLinks();
    }

    private static string[] GetFilesWithTpotLinks()
    {
        string cwd = Directory.GetCurrentDirectory();
        string projects_dir = cwd.GoUp(3);
        string pattern = @"www\.thepathoftruth.com\/";

        Console.WriteLine("projects dir: >>" + projects_dir);

        var results = new Grepper()
        {
            RootPath = projects_dir,
            FileSearchLinePattern = pattern,
            FileSearchMask = "*.json",
        }
            .GetFileNames()
            .ToArray();

        results.Take(5).Dump("results");

        return results;
    }

    private static string[] GetReadmeFiles()
    {
        string cwd = Directory.GetCurrentDirectory();
        Console.WriteLine(cwd);
        var results = new Grepper() { RootPath = cwd, FileSearchMask = "*README.md" }
            .GetFileNames()
            .ToArray();

        results.Dump("files");
        return results;
    }
}



// static RunAsWebsite() {

// var builder = WebApplication.CreateBuilder(args);

// // Resources.ThisAssembly.LoadResources(); // initialize all embedded resources.

// DotEnv.Load();

// // Add services to the container.
// builder.Services.AddRazorPages();

// DotEnv.Load();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapRazorPages();

// app.Run();

// }
