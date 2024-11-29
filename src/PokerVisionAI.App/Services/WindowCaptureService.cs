using System.Drawing;
using System.Runtime.InteropServices;

namespace PokerVisionAI.App.Services;

public class WindowCaptureService
{
    // APIs de Windows necesarias
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private IntPtr _targetWindowHandle;

    public WindowCaptureService()
    {
        _targetWindowHandle = IntPtr.Zero;
    }

    // Método para establecer la ventana objetivo por título
    public bool SetTargetWindowByTitle(string windowTitle)
    {
        _targetWindowHandle = FindWindow(null, windowTitle);
        return _targetWindowHandle != IntPtr.Zero;
    }

    // Método para usar la ventana activa
    public void UseActiveWindow()
    {
        _targetWindowHandle = GetForegroundWindow();
    }

    // Método para capturar la ventana
    public async Task<string> CaptureWindowAsync()
    {
        if (_targetWindowHandle == IntPtr.Zero)
        {
            UseActiveWindow();
        }

        var rect = new Rect();
        GetWindowRect(_targetWindowHandle, ref rect);

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        using (Bitmap bitmap = new Bitmap(width, height))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, new System.Drawing.Size(width, height));
            }

            // Guardar en un archivo temporal
            string fileName = Path.Combine(Path.GetTempPath(), $"capture_{DateTime.Now:yyyyMMddHHmmss}.png");
            bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            return fileName;
        }
    }
}
