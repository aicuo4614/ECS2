@echo off
set CS_TARGET_PATH=..\..\ECS2\Assets\Scripts\Net\Protocol
for %%i in (*.proto) do (
    protoc --csharp_out=%CS_TARGET_PATH% %%i
    rem 从这里往下都是注释，可忽略
    echo From %%i To %%~ni.cs Successfully!  
)
pause