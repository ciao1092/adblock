<!DOCTYPE html>
<html lang="en">
  <head>
    <title>adblock -- by ciao1092</title>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
  </head>
  <body style="background-color:#eee; color: #393960">
    <h1 id="-adblock-adblock-bmp-"><img src="https://raw.githubusercontent.com/ciao1092/adblock/main/adblock.bmp" alt="adblock" style="background:white"></h1>
    <hr />
    <p>Simple C# program that modifies the HOSTS file to block known advertising servers.</p>
    <p>Score <img src="https://raw.githubusercontent.com/ciao1092/adblock/main/92-out-of-100.png" alt="92 points out of 100"> in <a href="https://adblock-tester.com/">adblock-tester.com test</a> (Contextual advertising + Analytics Tools + Error Monitoring test -- this program does not block images and GIFs from webpages, because that would block ALL images: to decide which images should be blocked is browser- and website-related. That&#39;s why Banner Test was disabled).</p>
    <h2>Installation</h2>
    <ul>
      <li>Download the latest file (<code>x86</code> or <code>x64</code>, depending on your PC) in the Releases section (on the right of repository&#39;s homepage on <a href="https://github.com/ciao1092/adblock/">GitHub</a>)</li>
      <li>Open it to install a list of known advertising servers that will be blocked. <strong>Notes</strong>:
      <ul>
        <li>Recent Windows versions may complain about downloaded file safety; if you don&#39;t trust it, then just compile it yourself. The code is there.</li>
        <li>The program requires Administrator rights to run.</li>
      </ul>
    </ul>
    <h2 id="uninstallation">Uninstallation</h2>
    <ul>
      <li>Replace your computer&#39;s <code>HOSTS</code> file with one of the backups of your choice. <strong>Notes</strong>:
      <ul>
        <li>The <code>HOSTS</code> file will be:
          <ul>
            <li>in the <code>C:\Windows\System32\drivers\etc</code> or <code>C:\WinNT\System32\drivers\etc</code> directory for Windows NT 3 and up (including Windows XP, Vista, 7, 8(.1), 10 and 11).</li>
            <li>in the <code>C:\Windows</code> directory for previous Windows versions with networking capabilities (including Windows 95 and other DOS-bootstrapped Windows versions, such as Windows 98 and ME).</li>
          </ul>
        </li>
        <li>The <code>Backups</code> folder is located in the same folder <code>adblock.exe</code> is, and is created by <code>adblock.exe</code> upon installation; <code>adblock.exe</code> creates backups of the <code>HOSTS</code> file every time it makes modifications to it and stores them in the <code>Backups</code> folder. Hence the <code>Backups\hosts0001.backup.txt</code> file contains the versions of the <code>HOSTS</code> file found by <code>adblock.exe</code> at the time of installation.</li>
        <li>To edit the <code>HOSTS</code> file, you can run <code>notepad.exe</code> as Administrator and open the file through <strong><em>File</em></strong> --&gt; <strong><em>Open</em></strong>. You can delete <code>HOSTS</code> content (know what you&#39;re doing!!) and replace it with backup file&#39;s content.</li>
      </ul>
      <li>Delete the <code>adblock.exe</code> file you downloaded earlier.</li>
    </ul>
  </body>
</html>
