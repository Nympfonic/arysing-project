[mOSVersion]
Measure=Plugin
Plugin=SysInfo
SysInfoType=OS_VERSION

[mOSBits]
Measure=Plugin
Plugin=SysInfo
SysInfoType=OS_BITS

[mUptime]
Measure=Uptime
Format=%3!i!h %2!i!m
OnUpdateAction=[!UpdateMeter Uptime]
UpdateDivider=40

[mRefreshRate]
Measure=Plugin
Plugin=PowershellRM
Line=$rr = Get-WmiObject Win32_VideoController | Select CurrentRefreshRate
Line2=$rr.CurrentRefreshRate

[mGPUModel]
Measure=Plugin
Plugin=PowershellRM
Line=$gpu = Get-WmiObject Win32_VideoController | Select Name
Line2=$gpu.Name

[mCPUModel]
Measure=Plugin
Plugin=PowershellRM
Line=$cpu = Get-WmiObject Win32_Processor | Select Name
Line2=$cpu.Name
RegExpSubstitute=1
Substitute="(?i)(.+)\s\d.*":"\1"

[mCPUFreq]
Measure=Plugin
Plugin=PowershellRM
Line=$cpufreq = Get-WmiObject Win32_Processor | Select MaxClockSpeed
Line2=$cpufreq.MaxClockSpeed

[mCPUFreqRd]
Measure=Calc
Formula=Round(mCPUFreq,-1)/1000

[mRAMUsed]
Measure=PhysicalMemory
OnUpdateAction=[!UpdateMeasure mRAMUsedMB][!UpdateMeter RAMUsage]
UpdateDivider=40

[mRAMTotal]
Measure=PhysicalMemory
Total=1

[mRAMUsedMB]
Measure=Calc
Formula=mRAMUsed/(1024**2)

[mRAMTotalMB]
Measure=Calc
Formula=mRAMTotal/(1024**2)


; Meters -------------------------------------------------

[SpecsStyle]
Meter=String
X=8R
Y=3R
FontFace=#Font2#
FontSize=9
FontColor=#Color2#
InlineSetting=Color | #Color#
InlinePattern=\/\/
InlineSetting2=Weight | 200
InlinePattern2=\/\/(.*)
Container=OverlayContainer
Group=Tween
AntiAlias=1
DynamicVariables=1
;Hidden=1

[OverlayContainer]
Meter=Shape
Shape=Rectangle 10,0,#containerWidth#,370 | Fill Color 0,0,0 | StrokeWidth 0
Y=80

[OSVersionStr]
Meter=String
Text=OS //
X=#page2GroupX#
Y=15r
MeterStyle=SpecsStyle

[OSVersion]
Meter=String
MeasureName=mOSVersion
MeasureName2=mOSBits
Text=%1 x%2
Y=0r
MeterStyle=SpecsStyle

[UptimeStr]
Meter=String
Text=Uptime //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[Uptime]
Meter=String
MeasureName=mUptime
Y=0r
MeterStyle=SpecsStyle

[ResolutionStr]
Meter=String
Text=Resolution //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[Resolution]
Meter=String
MeasureName=mRefreshRate
Text=#PSCREENAREAWIDTH#x#PSCREENAREAHEIGHT# @ %1Hz
Y=0r
MeterStyle=SpecsStyle

[FontStr]
Meter=String
Text=Font //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[Font]
Meter=String
Text=#Font2#
Y=0r
MeterStyle=SpecsStyle

[GPUModelStr]
Meter=String
Text=GPU //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[GPUModel]
Meter=String
MeasureName=mGPUModel
Y=0r
MeterStyle=SpecsStyle

[CPUModelStr]
Meter=String
Text=CPU //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[CPUModel]
Meter=String
MeasureName=mCPUModel
MeasureName2=mCPUFreqRd
Text=%1 @ %2GHz
Y=0r
NumOfDecimals=2
MeterStyle=SpecsStyle

[RAMUsageStr]
Meter=String
Text=RAM //
X=[OSVersionStr:X]
MeterStyle=SpecsStyle

[RAMUsage]
Meter=String
MeasureName=mRAMUsedMB
MeasureName2=mRAMTotalMB
Text=%1MB / %2MB
Y=0r
MeterStyle=SpecsStyle
