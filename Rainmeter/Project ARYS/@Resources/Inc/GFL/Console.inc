[mConsoleScript]
Measure=Script
ScriptFile=#@#Scripts/Console.lua
ConsoleLog=#@#Scripts/verbose.txt

[mInputString]
Measure=String
String=
Hidden=1

[ConsoleBG]
Meter=Image
X=80
Y=(#SCREENAREAHEIGHT#/2+40)
W=400
H=600
SolidColor=20,20,20

[ConsoleTextStyle]
X=0r
Y=2R
FontColor=255,168,69
FontFace=Envy Code R
FontSize=12
AntiAlias=1

[ConsoleVerboseLine1]
Meter=String
Text=G&K - GRIFON & KRYUGER
X=10r
Y=10r
MeterStyle=ConsoleTextStyle

[ConsoleVerboseLine2]
Meter=String
Text=PRIVATE MILITARY CONTRACTOR
MeterStyle=ConsoleTextStyle

[ConsoleVerboseLine3]
Meter=String
Text=USER STATUS: AUTHENTICATED
MeterStyle=ConsoleTextStyle

[ConsoleOutputLine]
Meter=String
MeasureName=mConsoleScript
MeterStyle=ConsoleTextStyle

[ConsoleLabel]
Meter=String
Text=>
X=85
Y=(#SCREENAREAHEIGHT#-104)
W=390
H=24
SolidColor=20,20,20
MeterStyle=ConsoleTextStyle
LeftMouseUpAction=[!CommandMeasure mConsoleInput "ExecuteBatch ALL"]
MouseActionCursorName=Text

[mConsoleInput]
Measure=Plugin
Plugin=InputText
X=93
Y=(#SCREENAREAHEIGHT#-104)
W=382
H=24
Command1=[!SetOption mInputString String "$UserInput$"]
Command2=[!UpdateMeasure mInputString]
Command3=[!UpdateMeasure mConsoleScript]
Command4=["#@#reclick.exe"]
SolidColor=20,20,20
FontColor=255,168,69
FontFace=Envy Code R
FontSize=12
AntiAlias=1
FocusDismiss=1
