namespace PokerVisionAI.App.Services;

public class ImageNavigatorService
{
    public List<string> ImageFiles { get; private set; } = new();
    public int CurrentIndex { get; private set; } = -1;

    public async Task InitializeFromDirectory(string directoryPath)
    {
        var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
        ImageFiles = Directory.GetFiles(directoryPath)
            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
            .ToList();

        CurrentIndex = ImageFiles.Any() ? 0 : -1;
    }

    public string GetCurrentImage()
    {
        return CurrentIndex >= 0 && CurrentIndex < ImageFiles.Count
            ? ImageFiles[CurrentIndex]
            : string.Empty;
    }

    public bool CanMoveNext() => CurrentIndex < ImageFiles.Count - 1;
    public bool CanMovePrevious() => CurrentIndex > 0;

    public void MoveNext()
    {
        if (CanMoveNext())
            CurrentIndex++;
    }

    public void MovePrevious()
    {
        if (CanMovePrevious())
            CurrentIndex--;
    }
}
