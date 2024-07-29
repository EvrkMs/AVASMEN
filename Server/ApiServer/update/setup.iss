[Setup]
AppName=AVASMENA
AppVersion=1.0.7
DefaultDirName={pf}\AVASMENA
DefaultGroupName=AVASMENA
OutputBaseFilename=Setup
Compression=lzma
SolidCompression=yes
DisableDirPage=yes
DisableProgramGroupPage=yes

[Files]
Source: "debug\*.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "PinToTaskbar.ps1"; DestDir: "{tmp}"; Flags: deleteafterinstall
Source: "DebugDown\*.*"; DestDir: "{pf}\DownloadPhoto"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\AVASMENA"; Filename: "{app}\AVASMENA.exe"
Name: "{group}\{cm:UninstallProgram,AVASMENA}"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\AVASMENA.exe"; Description: "{cm:LaunchProgram,AVASMENA}"; Flags: nowait postinstall skipifsilent
Filename: "powershell.exe"; Parameters: "-ExecutionPolicy Bypass -File ""{tmp}\PinToTaskbar.ps1"" -Path ""{app}\AVASMENA.exe"""; StatusMsg: "Закрепление ярлыка на панели задач..."; Flags: runhidden
Filename: "sc.exe"; Parameters: "create DownloadPhoto binPath= ""{pf}\DownloadPhoto\DownloadPhoto.exe"" start= auto"; Flags: runhidden waituntilterminated
Filename: "sc.exe"; Parameters: "start DownloadPhoto"; Flags: runhidden waituntilterminated

[UninstallRun]
Filename: "sc.exe"; Parameters: "stop DownloadPhoto"; Flags: runhidden waituntilterminated
Filename: "sc.exe"; Parameters: "delete DownloadPhoto"; Flags: runhidden waituntilterminated