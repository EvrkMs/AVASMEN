param (
    [string]$Path = "$env:ProgramFiles\AVASMENA\AVASMENA.exe"
)

# Создание ярлыка на рабочем столе
$DesktopShortcutPath = [System.IO.Path]::Combine([System.Environment]::GetFolderPath("Desktop"), "AVASMENA.lnk")
$WScriptShell = New-Object -ComObject WScript.Shell
$Shortcut = $WScriptShell.CreateShortcut($DesktopShortcutPath)
$Shortcut.TargetPath = $Path
$Shortcut.WorkingDirectory = Split-Path -Parent $Path
$Shortcut.Save()

# Закрепление ярлыка на панели задач
$TaskBarPath = [System.IO.Path]::Combine([System.Environment]::GetFolderPath("ApplicationData"), 'Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar')
$TaskBarShortcutPath = [System.IO.Path]::Combine($TaskBarPath, 'AVASMENA.lnk')

Copy-Item -Path $DesktopShortcutPath -Destination $TaskBarShortcutPath -Force