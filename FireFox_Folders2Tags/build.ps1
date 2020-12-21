& dotnet publish -c Release  -r win10-x64 --self-contained=true  /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true  -o output_file
