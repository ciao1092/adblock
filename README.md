# ![adblock](adblock.bmp)

## adblock

-------------------------------

Simple C# program that modifies the HOSTS file to block known advertising servers.

Score ![92 points out of 100](92-out-of-100.png) in [adblock-tester.com test](https://adblock-tester.com/) (Contextual advertising + Analytics Tools + Error Monitoring test -- this program does not block images and GIFs from webpages, because that would block ALL images: to decide which images should be blocked is browser- and website-related. That's why Banner Test was disabled).

## Usage

* Download the latest file (<code>x86</code> or <code>x64</code>, depending on your PC) in the Releases section (on the right of repository's homepage on [GitHub](https://github.com/ciao1092/adblock/))
* Open it to install a list of known advertising servers that will be blocked. **Notes**: 
  * Recent Windows versions may complain about downloaded file safety; if you don't trust it, then just compile it yourself. The code is there.
  * The program requires Administrator rights to run.

## Uninstallation

* Replace your computer's <code>HOSTS</code> file with one of the backups of your choice. **Notes**:
  * The <code>HOSTS</code> file will be:
    * in the <code>C:\Windows\System32\drivers\etc</code> or <code>C:\WinNT\System32\drivers\etc</code> directory for Windows NT 3 and up (including Windows XP, Vista, 7, 8(.1), 10 and 11).
    * in the <code>C:\Windows</code> directory for previous Windows versions with networking capabilities (including Windows 95 and other DOS-bootstrapped Windows versions, such as Windows 98 and ME).
  * The <code>Backups</code> folder is located in the same folder <code>adblock.exe</code> is, and is created by <code>adblock.exe</code> upon installation; <code>adblock.exe</code> creates backups of the <code>HOSTS</code> file every time it makes modifications to it and stores them in the <code>Backups</code> folder. Hence the <code>Backups\hosts0001.backup.txt</code> file contains the versions of the <code>HOSTS</code> file found by <code>adblock.exe</code> at the time of installation.
  * To edit the <code>HOSTS</code> file, you can run <code>notepad.exe</code> as Administrator and open the file through ___File___ --> ___Open___. You can delete <code>HOSTS</code> content (know what you're doing!!) and replace it with backup file's content.
* Delete the <code>adblock.exe</code> file you downloaded earlier.
