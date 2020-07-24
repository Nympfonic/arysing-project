  [PVBG]
Meter=Image
SolidColor=40,40,40,80
X=80
Y=80
W=400
H=640

[EventPVImage]
X=2r
Y=2r
W=160
H=120
ImageName=#@#Images/PVs/#CURRENTSECTION#.png
LeftMouseUpAction=["#@#playpv.exe" "#@#\PVs\#CURRENTSECTION#.mp4"]
MouseActionCursor=0

[EventPVImageBGOverlay]
W=164
H=124
SolidColor=0,0,0,255

[EventPVImageBG]
Shape=Path ProgressStroke | Fill Color 0,0,0,0 | StrokeColor 255,168,69 | StrokeWidth 2
ProgressStroke=0,0 | LineTo 164,0 | LineTo 164,124 | LineTo 0,124 | ClosePath 1


; OPERATION CUBE

[OperationCubeBGOverlay]
Meter=Image
X=98
Y=98
MeterStyle=EventPVImageBGOverlay

[OperationCubeBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=OperationCubeBGOverlay

[OperationCube]
Meter=Image
MeterStyle=EventPVImage


; ARCTIC WARFARE

[ArcticWarfareBGOverlay]
Meter=Image
X=298
Y=98
MeterStyle=EventPVImageBGOverlay

[ArcticWarfareBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=ArcticWarfareBGOverlay

[ArcticWarfare]
Meter=Image
MeterStyle=EventPVImage


; DEEP DIVE

[DeepDiveBGOverlay]
Meter=Image
X=98
Y=38R
MeterStyle=EventPVImageBGOverlay

[DeepDiveBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=DeepDiveBGOverlay

[DeepDive]
Meter=Image
MeterStyle=EventPVImage


; SINGULARITY

[SingularityBGOverlay]
Meter=Image
X=298
Y=-2r
MeterStyle=EventPVImageBGOverlay

[SingularityBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=SingularityBGOverlay

[Singularity]
Meter=Image
MeterStyle=EventPVImage


; CONTINUUM TURBULENCE

[ContinuumTurbulenceBGOverlay]
Meter=Image
X=98
Y=38R
MeterStyle=EventPVImageBGOverlay

[ContinuumTurbulenceBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=ContinuumTurbulenceBGOverlay

[ContinuumTurbulence]
Meter=Image
MeterStyle=EventPVImage


; ISOMER

[IsomerBGOverlay]
Meter=Image
X=298
Y=-2r
MeterStyle=EventPVImageBGOverlay

[IsomerBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=IsomerBGOverlay

[Isomer]
Meter=Image
MeterStyle=EventPVImage


; SHATTERED CONNEXION

[ShatteredConnexionBGOverlay]
Meter=Image
X=98
Y=38R
MeterStyle=EventPVImageBGOverlay

[ShatteredConnexionBG]
Meter=Shape
MeterStyle=EventPVImageBG
Container=ShatteredConnexionBGOverlay

[ShatteredConnexion]
Meter=Image
MeterStyle=EventPVImage
