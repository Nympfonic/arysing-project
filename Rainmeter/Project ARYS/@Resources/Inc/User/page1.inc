[GNKLogoHighlight]
Meter=Shape
Shape=Path Chevron | Fill Color 255,168,69 | StrokeWidth 0
Chevron=0,0 | LineTo 25,15 | LineTo 50,0 | LineTo 50,4 | LineTo 25,19 | LineTo 0,4 | ClosePath 1
X=(#page1GroupX#-(#width#-140))
Y=48
Container=OverlayContainer
Group=Tween | Welcome
DynamicVariables=1

[GNKLogo]
Meter=Image
ImageName=#@#Images/Icons/gnk-logo.png
X=(#page1GroupX#-(#width#-140))
Y=0
W=50
Container=OverlayContainer
MouseOverAction=[!CommandMeasure mTween "Start(4)"]
MouseLeaveAction=[!CommandMeasure mTween "Reverse(4)"]
LeftMouseUpAction=[!ActivateConfig "Project ARYS\GFL"]
Group=Tween | Welcome
DynamicVariables=1


; Weather Section -------------------------------------------------

[mWeather]
Measure=WebParser
URL=https://api.darksky.net/forecast/#key#/#latitude#,#longitude#?exclude=alerts,minutely,hourly&units=#units#
RegExp=(?siU)^(.*)$
FinishAction=[!EnableMeasureGroup Weather][!UpdateMeasureGroup Weather][!UpdateMeterGroup Welcome][!ShowMeterGroup Welcome]
UpdateDivider=(62.5)
UpdateRate=3600


; Weather Currently Measures -------------------------------------------------

[mWeatherLocation]
Measure=Plugin
Plugin=JsonParser.dll
Source=[mWeather]
Query="timezone"
Group=Weather
Disabled=1

[mWeatherSummary]
Measure=Plugin
Plugin=JsonParser.dll
Source=[mWeather]
Query="currently.summary"
Group=Weather
Disabled=1

[mWeatherSummaryIcon]
Measure=Plugin
Plugin=JsonParser.dll
Source=[mWeather]
Query="currently.icon"
Group=Weather
Disabled=1

[mWeatherTemp]
Measure=Plugin
Plugin=JsonParser.dll
Source=[mWeather]
Query="currently.temperature"
Group=Weather
Disabled=1

[mWeatherTempRd]
Measure=Calc
Formula=Round(mWeatherTemp)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWeatherTempUnit]
Measure=String
String=#units#
Substitute="us":"[\x00B0]F","(\w+)":"[\x00B0]C"
RegExpSubstitute=1
Group=Weather
Disabled=1


; Weather Daily Measures -------------------------------------------------

[mWD1SummaryIcon]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[0].icon"
Group=Weather
Disabled=1

[mWD1TempH]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[0].temperatureHigh"
Group=Weather
Disabled=1

[mWD1TempHRd]
Measure=Calc
Formula=Round(mWD1TempH)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWD1TempL]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[0].temperatureLow"
Group=Weather
Disabled=1

[mWD1TempLRd]
Measure=Calc
Formula=Round(mWD1TempL)
Substitute="-0":"0"
Group=Weather
Disabled=1


[mWD2SummaryIcon]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[1].icon"
Group=Weather
Disabled=1

[mWD2TempH]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[1].temperatureHigh"
Group=Weather
Disabled=1

[mWD2TempHRd]
Measure=Calc
Formula=Round(mWD2TempH)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWD2TempL]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[1].temperatureLow"
Group=Weather
Disabled=1

[mWD2TempLRd]
Measure=Calc
Formula=Round(mWD2TempL)
Substitute="-0":"0"
Group=Weather
Disabled=1


[mWD3SummaryIcon]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[2].icon"
Group=Weather
Disabled=1

[mWD3TempH]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[2].temperatureHigh"
Group=Weather
Disabled=1

[mWD3TempHRd]
Measure=Calc
Formula=Round(mWD3TempH)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWD3TempL]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[2].temperatureLow"
Group=Weather
Disabled=1

[mWD3TempLRd]
Measure=Calc
Formula=Round(mWD3TempL)
Substitute="-0":"0"
Group=Weather
Disabled=1


[mWD4SummaryIcon]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[3].icon"
Group=Weather
Disabled=1

[mWD4TempH]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[3].temperatureHigh"
Group=Weather
Disabled=1

[mWD4TempHRd]
Measure=Calc
Formula=Round(mWD4TempH)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWD4TempL]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[3].temperatureLow"
Group=Weather
Disabled=1

[mWD4TempLRd]
Measure=Calc
Formula=Round(mWD4TempL)
Substitute="-0":"0"
Group=Weather
Disabled=1


[mWD5SummaryIcon]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[4].icon"
Group=Weather
Disabled=1

[mWD5TempH]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[4].temperatureHigh"
Group=Weather
Disabled=1

[mWD5TempHRd]
Measure=Calc
Formula=Round(mWD5TempH)
Substitute="-0":"0"
Group=Weather
Disabled=1

[mWD5TempL]
Measure=Plugin
Plugin=JsonParser
Source=[mWeather]
Query="daily.data[4].temperatureLow"
Group=Weather
Disabled=1

[mWD5TempLRd]
Measure=Calc
Formula=Round(mWD5TempL)
Substitute="-0":"0"
Group=Weather
Disabled=1


; Weather Currently Meters -------------------------------------------------

[WMeterStyle]
Container=OverlayContainer
Group=Tween | Welcome
DynamicVariables=1
Hidden=1

[WTemp]
Meter=String
MeasureName=mWeatherTempRd
MeasureName2=mWeatherTempUnit
Text=%1%2
X=(#page1GroupX#-#iconSize#-5)
Y=120
FontSize=20
FontWeight=300
StringAlign=Right
InlineSetting=Size | 14
InlinePattern=\D?\d+(.+)
MeterStyle=TitleStyle | WMeterStyle

[WLocation]
Meter=String
MeasureName=mWeatherLocation
X=0r
Y=-1R
FontColor=#Color#
FontSize=10
FontWeight=300
StringAlign=Right
MeterStyle=TitleStyle | WMeterStyle

[WSummary]
Meter=String
MeasureName=mWeatherSummary
X=0r
Y=0R
FontColor=#Color#
FontSize=8
FontWeight=300
StringAlign=Right
MeterStyle=TitleStyle | WMeterStyle

[WSummaryIcon]
Meter=Image
MeasureName=mWeatherSummaryIcon
ImageName=#@#Images/Icons/weather/%1.png
X=(#page1GroupX#-#iconSize#)
Y=120
W=#iconSize#
SolidColor=#Color2#
MeterStyle=WMeterStyle


; Weather Daily Meters -------------------------------------------------

[WDIconStyle]
ImageName=#@#Images/Icons/weather/%1.png
X=0R
Y=0r
W=#dailyIconSize#
ImageTint=#Color2#
Container=OverlayContainer
Group=Tween | Welcome
DynamicVariables=1
Hidden=1

[WDTextStyle]
Y=0r
Text=%1[\x00B0]
FontColor=#Color2#
FontFace=#Font2#
FontSize=11
FontWeight=300
StringAlign=Right
Container=OverlayContainer
Group=Tween | Welcome
AntiAlias=1
DynamicVariables=1
Hidden=1

[WD1SummaryIcon]
Meter=Image
MeasureName=mWD1SummaryIcon
X=(#page1GroupX#-(#width#-50))
Y=15R
ImageTint=
SolidColor=#Color2#
MeterStyle=WDIconStyle

[WD2SummaryIcon]
Meter=Image
MeasureName=mWD2SummaryIcon
MeterStyle=WDIconStyle

[WD3SummaryIcon]
Meter=Image
MeasureName=mWD3SummaryIcon
MeterStyle=WDIconStyle

[WD4SummaryIcon]
Meter=Image
MeasureName=mWD4SummaryIcon
MeterStyle=WDIconStyle

[WD5SummaryIcon]
Meter=Image
MeasureName=mWD5SummaryIcon
MeterStyle=WDIconStyle

[WD1TempBG]
Meter=Image
X=(#page1GroupX#-(#width#-50))
Y=0R
H=#dailyIconSize#
SolidColor=#Color2#
MeterStyle=WDIconStyle

[WD1TempH]
Meter=String
MeasureName=mWD1TempHRd
X=(#page1GroupX#-(#width#-50)+#dailyIconSize#-#dailyIconSize#/4)
Y=(#dailyIconSize#/8)r
FontColor=#Color3#
MeterStyle=WDTextStyle

[WD2TempH]
Meter=String
MeasureName=mWD2TempHRd
X=(#page1GroupX#-(#width#-50)+2*#dailyIconSize#-#dailyIconSize#/4)
MeterStyle=WDTextStyle

[WD3TempH]
Meter=String
MeasureName=mWD3TempHRd
X=(#page1GroupX#-(#width#-50)+3*#dailyIconSize#-#dailyIconSize#/4)
MeterStyle=WDTextStyle

[WD4TempH]
Meter=String
MeasureName=mWD4TempHRd
X=(#page1GroupX#-(#width#-50)+4*#dailyIconSize#-#dailyIconSize#/4)
MeterStyle=WDTextStyle

[WD5TempH]
Meter=String
MeasureName=mWD5TempHRd
X=(#page1GroupX#-(#width#-50)+5*#dailyIconSize#-#dailyIconSize#/4)
MeterStyle=WDTextStyle

[WD1TempL]
Meter=String
MeasureName=mWD1TempLRd
X=(#page1GroupX#-(#width#-50)+#dailyIconSize#-#dailyIconSize#/4)
Y=0R
FontColor=#Color3#
MeterStyle=WDTextStyle

[WD2TempL]
Meter=String
MeasureName=mWD2TempLRd
X=(#page1GroupX#-(#width#-50)+2*#dailyIconSize#-#dailyIconSize#/4)
FontColor=#Color#
MeterStyle=WDTextStyle

[WD3TempL]
Meter=String
MeasureName=mWD3TempLRd
X=(#page1GroupX#-(#width#-50)+3*#dailyIconSize#-#dailyIconSize#/4)
FontColor=#Color#
MeterStyle=WDTextStyle

[WD4TempL]
Meter=String
MeasureName=mWD4TempLRd
X=(#page1GroupX#-(#width#-50)+4*#dailyIconSize#-#dailyIconSize#/4)
FontColor=#Color#
MeterStyle=WDTextStyle

[WD5TempL]
Meter=String
MeasureName=mWD5TempLRd
X=(#page1GroupX#-(#width#-50)+5*#dailyIconSize#-#dailyIconSize#/4)
FontColor=#Color#
MeterStyle=WDTextStyle
