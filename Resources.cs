using System.Reflection;
using System.Text;
using CodeMechanic.Types;

namespace tpot_reader;

public static class Resources
{
    private static string[] resources;

    public static Assembly ThisAssembly => typeof(Resources).Assembly;

    public static void LoadResources(this Assembly? assembly, bool debug = false)
    {
        if (assembly is null)
        {
            Console.WriteLine("assembly is null");
            return;
        }

        resources = assembly.GetManifestResourceNames();
        if (debug)
            Console.WriteLine("total resources found: " + resources.Length);
        if (resources.Length == 0)
            return;

        if (debug)
            Console.WriteLine($"Resources in {assembly.FullName}");
        if (debug)
            foreach (var resource in resources)
                Console.WriteLine(resource);
    }

    public static class Embedded
    {
        private static string sproc_name = "search_todos";
        private static bool debug = true;

        public static string GetFile(string filename, bool debug = false)
        {
            if (filename.IsEmpty())
                throw new ArgumentNullException(nameof(filename));
            // if (debug) Console.WriteLine($"filname :>> {filename}");
            if (!Path.HasExtension(filename))
                throw new ArgumentException(nameof(filename) + " must have an extension!"); // do NOT allow extensionless files!
            // if (debug) resources.Dump("current resources");
            string filepath = resources.SingleOrDefault(name => name.ToLower().Contains(filename));
            if (debug)
                Console.WriteLine("file path: \n" + filepath);
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filepath)!;
            using var streamReader = new StreamReader(stream, Encoding.UTF8);
            return streamReader.ReadToEnd();
        }

        public static string SqlFile
        {
            get
            {
                // var info = Assembly.GetExecutingAssembly().GetName();
                // var ass_name = info.Name;
                // if (debug) Console.WriteLine("ass name:>> " + ass_name);
                string filename = $"{sproc_name}.sql";
                string filepath = resources.FirstOrDefault(name =>
                    name.ToLower().Contains(filename)
                );
                // if (debug)
                Console.WriteLine("file path: \n" + filepath);
                using var stream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream(filepath)!;
                using var streamReader = new StreamReader(stream, Encoding.UTF8);
                return streamReader.ReadToEnd();
            }
        }
    }
}
