# Define some objects
$path = "$env:USERPROFILE\wallpaper.jpg"
$link = "https://i.ibb.co/jg1v95Y/unnamed.png"
$client = New-Object System.Net.WebClient

Remove-Item $path -ErrorAction SilentlyContinue # Remove the file if it exists

$client.DownloadFile($link, $path) # Download the file

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

[Wallpaper]::SetWallpaper($path) # Set the wallpaper
exit