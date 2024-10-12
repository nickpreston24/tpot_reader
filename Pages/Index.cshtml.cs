using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tpot_reader.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() { }

    private async Task<List<string>> GetAllLocalTpotLinks()
    {
        // string sample_link = @"https://www.thepathoftruth.com/teachings/on-gifts-tithes-and-offerings-to-thepathoftruth";
        string url_regex = @"(?<http_type>https?)://www\.(thepathoftruth.com)(?<remainder>.*?)$";
        // https://regex101.com/r/jDfmhS/1

        string desktop_dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Console.WriteLine("desktop :>> " + desktop_dir);

        var sharpifyrc_files = new Grepper()
        {
            RootPath = desktop_dir,
            Recursive = true,
            FileSearchLinePattern = url_regex
        }
            .GetMatchingFiles()
            .ToList();

            sharpifyrc_files.Dump("files");


        return new List<string>();
    }

    public async Task<IActionResult> OnGetMarkAsRed(string id = "", string paper_url = "")
    {
        var links_only = await GetAllLocalTpotLinks();
        links_only.Dump();
        return Content("Update complete.");
    }
}

public class TpotUrl
{
    public string http_type { get; set; } = string.Empty;
    public string remainder { get; set; } = string.Empty;
}

// public class TpotLinksRegex : Enumeration {


// }
