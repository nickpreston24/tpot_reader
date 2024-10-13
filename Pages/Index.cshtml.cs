using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;
using CodeMechanic.Types;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace tpot_reader.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() { }

    private async Task<List<Paper>> Search(Paper search)
    {
        string sql = "select * from papers";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Paper>(sql, search);
        return records.ToList();
    }

    private SqliteConnection CreateConnection() => new SqliteConnection("Data Source=databaseFile");

    private async Task<List<Grepper.GrepResult>> GetAllLocalTpotLinks()
    {
        string url_regex = @"(?<http_type>https?)://www\.(thepathoftruth.com)(?<remainder>.*?)$";
        // https://regex101.com/r/jDfmhS/1

        string desktop_dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Console.WriteLine("desktop :>> " + desktop_dir);

        string cwd = Directory.GetCurrentDirectory();

        string projects_dir = cwd.GoUp(2);
        Console.WriteLine("projects dir :>> "+  projects_dir);

        var grep_results = new Grepper()
        {
            RootPath = projects_dir,
            Recursive = true,
            FileSearchLinePattern = url_regex
        }
            .GetMatchingFiles()
            .ToList();

            // grep_results.Dump("results");

        // var result = await Search(new Read() { });
        // result.Dump();

        return grep_results;//.Select(x=>x.Line).ToList();
    }

    public async Task<IActionResult> OnGetUpdateStatus(string status = "reading") {
        status.Dump();
        return Partial("_Papers", new Paper().AsList());
    }

    public async Task<IActionResult> OnGetAllPapers(string id = "", string paper_url = "")
    {
        try
        {
            var links_only = await GetAllLocalTpotLinks();
            links_only.Dump("grep results");
            // return Content($"Update complete.");

            // var papers = links_only.Select(line=> new Paper{
            //     tpot_url = line.Trim()
            // }).ToList();
            
            // papers.Dump("papers");

            return Partial("_Papers", new Paper().AsList());
        }
        catch (Exception e)
        {
            return Partial("_Alert", e);
        }
    }
}

public class TpotUrl
{
    public string http_type { get; set; } = string.Empty;
    public string remainder { get; set; } = string.Empty;
}

public class Paper
{
    public string paper_id { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string tpot_url { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
}

// public class TpotLinksRegex : Enumeration {


// }
