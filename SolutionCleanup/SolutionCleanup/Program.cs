namespace SolutionCleanup
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Pass the path to the solution file:");
            Console.WriteLine(@"Example: (C:\MySolution\MySolution.sln)");
            string solutionFilePath = Console.ReadLine();

            if (solutionFilePath == null)
            {
                Console.WriteLine("Usage: SolutionCleanup <solution_file_path>");
                return;
            }

            if (!File.Exists(solutionFilePath))
            {
                Console.WriteLine("File not found: {0}", solutionFilePath);
                return;
            }

            string solutionDirectory = Path.GetDirectoryName(solutionFilePath);
            string[] projectFiles = Directory.GetFiles(solutionDirectory, "*.csproj", SearchOption.AllDirectories);

            foreach (string projectFile in projectFiles)
            {
                string projectDirectory = Path.GetDirectoryName(projectFile);
                string[] binDirectories = Directory.GetDirectories(projectDirectory, "bin", SearchOption.TopDirectoryOnly);
                string[] objDirectories = Directory.GetDirectories(projectDirectory, "obj", SearchOption.TopDirectoryOnly);

                foreach (string binDirectory in binDirectories)
                {
                    Console.WriteLine("Deleting directory: {0}", binDirectory);
                    Directory.Delete(binDirectory, recursive: true);
                }

                foreach (string objDirectory in objDirectories)
                {
                    Console.WriteLine("Deleting directory: {0}", objDirectory);
                    Directory.Delete(objDirectory, recursive: true);
                }
            }

            Console.WriteLine("Cleanup complete.");
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
    }
}
