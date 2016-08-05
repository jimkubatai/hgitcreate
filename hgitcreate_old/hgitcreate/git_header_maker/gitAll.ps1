set-location ".\"
if( Get-Process | where {$_.Name -eq "gitCmd"} ) { Stop-Process -Name gitCmd }
if( Test-Path ".\temp.bat" ) { remove-item temp.bat }
.\git.ps1
set-location ..\primak_lib\
.\git.ps1
set-location ..\canopen_lib\
.\git.ps1
set-location ..\lpc2000_lib\
.\git.ps1
set-location ..\BumerangExchange_lib\
.\git.ps1