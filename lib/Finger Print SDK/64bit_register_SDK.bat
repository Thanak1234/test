@echo off
copy *.dll %windir%\syswow64\

regsvr32.exe %windir%\syswow64\zkemkeeper.dll

exit