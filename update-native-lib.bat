echo Updating submodules...
git submodule update --init --recursive

echo Checking if Yoga build fodler exists...
if not exist "yoga\yoga\build" (
  echo Yoga folder does not exist, creating...
  mkdir yoga\yoga\build
)

echo Building Yoga library...
cd yoga\yoga\build
cmake ..
msbuild yogacore.sln /t:Build /p:Configuration=Release /p:Platform=x64

echo Building UniFlexBox native Yoga wrapper...
cd ../../../uniflexbox-native
msbuild uniflexbox-native.sln /t:Build /p:Configuration=Release /p:Platform=x64

echo Copying DLL to project...
copy x64\Release\uniflexbox-native.dll ..\UniFlexBox\Runtime\libs\uniflexbox-native.dll
copy x64\Release\uniflexbox-native.pdb ..\UniFlexBox\Runtime\libs\uniflexbox-native.pdb

echo Done!
