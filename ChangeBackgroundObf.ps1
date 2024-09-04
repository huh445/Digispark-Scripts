$u="aHR0cHM6Ly9kamouZ2VvcmdpYS5nb3Yvc2l0ZXMvZGpqLmdl..."
$p="$env:USERPROFILE\wp.jpg"
$wc=New-Object System.Net.WebClient
$wc.DownloadFile([System.Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($u)),$p)
$src=@"
using System.Runtime.InteropServices;
public class x{public const int a=20;public const int b=0x01;public const int c=0x02;
[DllImport("user32.dll",SetLastError=true,CharSet=CharSet.Auto)]
private static extern int y(int z,int q,string r,int s);
public static void n(string m){y(a,0,m,b|c);}}
"@
Add-Type -TypeDefinition $src
[x]::n($p)
