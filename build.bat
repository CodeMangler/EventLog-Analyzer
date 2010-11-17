SET SOLUTION_DIR=C:\Workspace\Development\C#\Visual Studio\EventLog Analyzer
SET CONSOLE_CLIENT=%SOLUTION_DIR%\Console Client
SET EVENT_LOG=%SOLUTION_DIR%\EventLog
SET OUTPUT_DIR=%SOLUTION_DIR%\Output
SET PATH="C:\Windows\Microsoft.NET\Framework\v2.0.50727";C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin;%PATH%

cd "%EVENT_LOG%"
csc /t:module /out:"%OUTPUT_DIR%\EventLog.netmodule" "%EVENT_LOG%\*.cs"
cd "%CONSOLE_CLIENT%"
resgen /str:cs /compile "%CONSOLE_CLIENT%\Properties\Resources.resx"
move "%CONSOLE_CLIENT%\Properties\Resources.resources" "%OUTPUT_DIR%"
csc /addmodule:"%OUTPUT_DIR%\EventLog.netmodule" /out:"%OUTPUT_DIR%\Analyzer.exe" /main:pal.EventLogAnalyzer.ConsoleClient.CLI /res:"%OUTPUT_DIR%\Resources.resources" /recurse:*.cs
cd "%SOLUTION_DIR%"