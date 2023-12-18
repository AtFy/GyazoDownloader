using System.Text.RegularExpressions;

namespace GyazoDownloader;

public static class Downloader
{
    static Downloader()
    {
        _downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }

    private static string _downloadsFolder;
    private static readonly HttpClient _httpClient = new();
    public static async Task Download(string link)
    {
        using var responsePage = await _httpClient.GetAsync(link);
        
        var pageContent = await responsePage.Content.ReadAsStringAsync();
        
        var videoLink = pageContent.Split("property=\"og:image\" /><meta content=\"")[1]
            .Split("\" property=\"og:video:url\"")[0];

        var responseVideo = await _httpClient.GetByteArrayAsync(videoLink);
        
        
        File.WriteAllBytes(GenerateFileName(), responseVideo);
    }

    public static void OverrideFolder(string path)
    {
        _downloadsFolder = path;
    }

    private static string GenerateFileName()
    {
        var iterator = 1;
        var name = $"/{iterator}.mp4";
        while (true)
        {
            if (!File.Exists($"{_downloadsFolder}{name}"))
            {
                return $"{_downloadsFolder}{name}";
            }

            ++iterator;
            name = $"/{iterator}.mp4";
        }
    }
}