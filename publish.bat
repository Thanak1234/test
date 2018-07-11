@echo off
cd "src\Workflow.Web.Service"

@echo off
setlocal ENABLEDELAYEDEXPANSION
set source="\bin"
set target="..\..\publish"

rem /l = list only, /u = already existing, /y = yes to replace
rem /f = display full source and destination file names, /c = continue on errors
rem /s = subdirectories, /k = keep attributes
rem split the strings at ->, we're only interested in the part after ->
for /f "usebackq tokens=1,* delims=>" %%a in (`xcopy %source% %target% /l /u /y /f /c /s /k`) do (
    set file=%%b
    rem ignore lines without a destination, e.g. 15 file(s) copied
    if x!file! neq x (
        rem remove the leading space, original string is source -> destination
        set file=!file:~1!
        for %%f in ("!file!") do (
            if not exist %%~dpf\backup\bin\* md %%~dpf\backup\bin
            rem only backup if not already backed up
            if not exist %%~dpf\backup\bin\%%~nxf move %%f %%~dpf\backup\bin
        )
    )
)
xcopy %source% %target% /y /c /q /s /k