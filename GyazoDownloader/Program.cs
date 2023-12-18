namespace GyazoDownloader;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length != 0)
        {
            Downloader.OverrideFolder(args[0]);
        }
        
        while (true)
        {
            Console.WriteLine("Paste the video link below.");
            var link = Console.ReadLine();
            await Downloader.Download(link);
        }
    }
}