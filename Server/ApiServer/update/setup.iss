[Setup]
AppName=AVASMENA
AppVersion=1.0.3
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

[Icons]
Name: "{group}\AVASMENA"; Filename: "{app}\AVASMENA.exe"
Name: "{group}\{cm:UninstallProgram,AVASMENA}"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\AVASMENA.exe"; Description: "{cm:LaunchProgram,AVASMENA}"; Flags: nowait postinstall skipifsilent
Filename: "powershell.exe"; Parameters: "-ExecutionPolicy Bypass -File ""{tmp}\PinToTaskbar.ps1"" -Path ""{app}\AVASMENA.exe"""; StatusMsg: "Закрепление ярлыка на панели задач..."; Flags: runhidden