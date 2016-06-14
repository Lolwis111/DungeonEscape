@echo off

set homeDIR=H:\levin\Documents\DungeonEscape
set releaseDIR="H:\levin\Documents\Visual Studio 2015\Projects\DungeonEscape\DungeonEscape\DungeonEscape\bin\x86\Release"

IF exist %homeDIR%\nul ( 
  rmdir /S /Q %homeDIR%
  echo cleaning 
 ) ELSE ( mkdir %homeDIR% )

echo root created

mkdir %homeDIR%\Content
mkdir %homeDIR%\Content\Audio
mkdir %homeDIR%\Content\Textures
mkdir %homeDIR%\Content\Textures\GUI
mkdir %homeDIR%\Content\Textures\Low
mkdir %homeDIR%\Content\Textures\Low\Blocks
mkdir %homeDIR%\Content\Textures\Low\Items
mkdir %homeDIR%\Content\Textures\Low\Sprites
mkdir %homeDIR%\Content\Textures\Normal
mkdir %homeDIR%\Content\Textures\Normal\Blocks
mkdir %homeDIR%\Content\Textures\Normal\Items
mkdir %homeDIR%\Content\Textures\Normal\Sprites
mkdir %homeDIR%\Content\Levels

IF exist %releaseDIR%\DungeonEscape.exe ( copy %releaseDIR%\DungeonEscape.exe %homeDIR%\DungeonEscape.exe ) ELSE ( goto error )

cd %releaseDIR%\Content
for %%i in (*.*) do copy "%%i" /B "%homeDIR%\Content\%%i"

cd %releaseDIR%\Content\Audio
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Audio\%%i"
cd %releaseDIR%\Content\Levels
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Levels\%%i"
cd %releaseDIR%\Content\Textures\GUI
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\GUI\%%i"

cd %releaseDIR%\Content\Textures\Low\Blocks
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Low\Blocks\%%i"
cd %releaseDIR%\Content\Textures\Low\Items
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Low\Items\%%i"
cd %releaseDIR%\Content\Textures\Low\Sprites
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Low\Sprites\%%i"

cd %releaseDIR%\Content\Textures\Normal\Blocks
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Normal\Blocks\%%i"
cd %releaseDIR%\Content\Textures\Normal\Items
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Normal\Items\%%i"
cd %releaseDIR%\Content\Textures\Normal\Sprites
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\Textures\Normal\Sprites\%%i"

echo ^<?xml version="1.0" encoding="UTF-8"?^>	 	> %homeDIR%\settings.xml 
echo ^<settings^> 										>> %homeDIR%\settings.xml
echo ^<volume^>1^,0^</volume^> 							>> %homeDIR%\settings.xml
echo ^<fullscreen^>false^</fullscreen^>	 				>> %homeDIR%\settings.xml
echo ^<resolution^>1024;768^</resolution^> 				>> %homeDIR%\settings.xml
echo ^<textures^>low^</textures^> 						>> %homeDIR%\settings.xml
echo ^</settings^> 								 		>> %homeDIR%\settings.xml

set releaseDIR="H:\levin\Documents\Visual Studio 2015\Projects\DungeonEscape\MapCreator2D\MapCreator2D\bin\x86\Release"

IF exist %releaseDIR%\MapCreator2D.exe ( copy %releaseDIR%\MapCreator2D.exe %homeDIR%\MapCreator2D.exe ) ELSE ( goto error )
cd %releaseDIR%
for %%i in (*.dll) do copy "%%i" "%homeDIR%\%%i"

mkdir %homeDIR%\Content\MapCreator
mkdir %homeDIR%\Content\MapCreator\Textures
mkdir %homeDIR%\Content\MapCreator\Textures\Blocks
mkdir %homeDIR%\Content\MapCreator\Textures\Items
mkdir %homeDIR%\Content\MapCreator\Textures\Sprites

cd %releaseDIR%\Content\MapCreator\Textures
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\MapCreator\Textures\%%i"

cd %releaseDIR%\Content\MapCreator\Textures\Blocks
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\MapCreator\Textures\Blocks\%%i"

cd %releaseDIR%\Content\MapCreator\Textures\Items
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\MapCreator\Textures\Items\%%i"

cd %releaseDIR%\Content\MapCreator\Textures\Sprites
for %%i in (*.*) do copy "%%i" "%homeDIR%\Content\MapCreator\Textures\Sprites\%%i"

:ask

set /P c=Done. Create ZIP-Archive (Y/N)?
IF /I "%c%" EQU "Y" goto dozip
IF /I "%c%" EQU "N" goto exit

goto ask

:error
echo FILE NOT FOUND OR INVALID DATE
pause

:dozip
"H:\Program Files\7-zip\7z.exe" a -tzip "%homeDIR%\DungeonEscape.zip" "%homeDIR%"
:exit
cd "H:\levin\Documents\Visual Studio 2015\Projects\DungeonEscape\DungeonEscape\DungeonEscape\"
echo Done.