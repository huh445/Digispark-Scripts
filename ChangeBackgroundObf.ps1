$u="aHR0cHM6Ly9pLmliYi5jby9EUHFkTDZ2Qy9jdW0ucG5n"
$p="$env:USERPROFILE\wp.jpg"
$wc=New-Object System.Net.WebClient
$wc.DownloadFile([System.Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($u)),$p)
$src = @"
using System.Runtime.InteropServices;
public class x {
  public const int SPI_SETDESKWALLPAPER = 20;
  public const int SPIF_UPDATEINIFILE = 0x01;
  public const int SPIF_SENDWININICHANGE = 0x02;

  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

  public static void n(string m) {
    SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, m, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
  }
}
"@
Add-Type -TypeDefinition $src
[x]::n($p)
