# Define some objects
$path = "$env:USERPROFILE\wallpaper.jpg"
$link = "https://i.ibb.co/XnjN8wm/Ethan-In-His-Dreams.png"
$client = New-Object System.Net.WebClient

Remove-Item $path -ErrorAction SilentlyContinue
$client.DownloadFile($link, $path)

# Set registry keys to use Tiled wallpaper style
Set-ItemProperty -Path 'HKCU:\Control Panel\Desktop' -Name WallpaperStyle -Value 0
Set-ItemProperty -Path 'HKCU:\Control Panel\Desktop' -Name TileWallpaper -Value 1

# C# class to change wallpaper
$setwallpapersrc = @"
using System.Runtime.InteropServices;

public class Wallpaper
{
  public const int SetDesktopWallpaper = 20;
  public const int UpdateIniFile = 0x01;
  public const int SendWinIniChange = 0x02;
  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
  public static void SetWallpaper(string path)
  {
    SystemParametersInfo(SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange);
  }
}
"@

Add-Type -TypeDefinition $setwallpapersrc
[Wallpaper]::SetWallpaper($path)
