@echo off
cls
:start
Freeria.exe -dedicated -config serverconfig.txt
@echo.
@echo Restarting server...
@echo.
goto start