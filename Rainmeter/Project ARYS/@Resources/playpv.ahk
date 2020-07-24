#NoTrayIcon
#SingleInstance Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

if A_Args.Length() == 1
{
  LongFilePath := A_Args[1]
  SplitPath, LongFilePath, name
  filepath := "PVs\" . name
  Run, "C:\Program Files (x86)\K-Lite Codec Pack\MPC-HC64\mpc-hc64.exe" %filepath% /play /close
  WinWait, ahk_exe mpc-hc64.exe
  while WinExist(ahk_exe mpc-hc64.exe)
  {
    WinMove, ahk_exe mpc-hc64.exe,, 640, 320, 1280, 720
  }
  if !WinExist(ahk_exe mpc-hc64.exe)
  {
    ExitApp
  }
}
else
{
  ExitApp
}
