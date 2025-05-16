using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;

namespace AppDeviceInfo;
internal static class DeviceInfo
{
    static readonly EasClientDeviceInformation deviceInfo;
    static string currentIdiom;
    static string currentType = "Unknown";
    static string systemProductName;

    static DeviceInfo()
    {
        deviceInfo = new EasClientDeviceInformation();
        currentIdiom = "Unknown";
        try
        {
            systemProductName = deviceInfo.SystemProductName;
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get system product name. {ex.Message}");
        }
    }


    public static string Get()
    {
        var output = new StringBuilder();

        /*
        try
        {
            output.AppendLine($"EasClientDeviceInformation:");
            output.AppendLine($"FriendlyName:{deviceInfo.FriendlyName}");
            output.AppendLine($"SystemManufacturer:{deviceInfo.SystemManufacturer}");
            output.AppendLine($"SystemProductName:{deviceInfo.SystemProductName}");
            output.AppendLine($"Id:{deviceInfo.Id}");
            output.AppendLine($"OperatingSystem:{deviceInfo.OperatingSystem}");
            output.AppendLine($"SystemFirmwareVersion:{deviceInfo.SystemFirmwareVersion}");
            output.AppendLine($"SystemHardwareVersion:{deviceInfo.SystemHardwareVersion}");
            output.AppendLine("");
            output.AppendLine("Windows.System.Profile.AnalyticsInfo:");
            output.AppendLine($"DeviceForm:{AnalyticsInfo.DeviceForm}");
            output.AppendLine($"VersionInfo.DeviceFamily:{AnalyticsInfo.VersionInfo.DeviceFamily}");
            output.AppendLine($"VersionInfo.DeviceFamilyVersion:{GetVersionString()}");
            output.AppendLine($"VersionInfo.ProductName:{AnalyticsInfo.VersionInfo.ProductName}");
        }
        catch (Exception ex)
        {
            output.AppendLine(ex.ToString());
        }
        */

        var result = string.Empty;
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = true,
        };

        try
        {
            result += "DEFINE: \n";
            #if HAS_UNO_SKIA
            result += "\t HAS_UNO_SKIA\n";
            #endif
            
            #if HAS_UNO_SKIA_APPLE_UIKIT
            result += "\t HAS_UNO_SKIA_APPLE_UIKIT\n";
            #endif
            
            #if HAS_UNO
            result += "\t HAS_UNO\n";
            #endif
            
            #if HAS_UNO_WINUI
            result += "\t HAS_UNO_WINUI\n";
            #endif
            
            #if DESKTOP
            result += "\t DESKTOP\n";
            #endif
            
            #if WINDOWS
            result += "\t WINDOWS\n";
#endif
            
            #if HAS_UNO_SKIA_WIN32
            result += "\t HAS_UNO_SKIA_WIN32\n";
            #endif
            
            #if HAS_UNO_SKIA_LINUX_FB
            result += "\t HAS_UNO_SKIA_LINUX_FB\n";
            #endif
            
            #if HAS_UNO_SKIA_MACOS
            result += "\t HAS_UNO_SKIA_MACOS\n";
            #endif
            
            #if HAS_UNO_SKIA_WPF
            result += "\t HAS_UNO_SKIA_WPF\n";
            #endif
            
            #if HAS_UNO_SKIA_X11
            result += "\t HAS_UNO_SKIA_X11\n";
            #endif
            
            #if HAS_UNO_SKIA_ANDROID
            result += "\t HAS_UNO_SKIA_ANDROID\n";
#endif  
            
            #if HAS_UNO_SKIA_IOS
            result += "\t HAS_UNO_SKIA_IOS\n";
#endif
            #if HAS_UNO_SKIA_TVOS
            result += "\t HAS_UNO_SKIA_TVOS\n";
#endif
            #if HAS_UNO_SKIA_WATCHOS
            result += "\t HAS_UNO_SKIA_WATCHOS\n";
#endif
            #if HAS_UNO_SKIA_LINUX
            result += "\t HAS_UNO_SKIA_LINUX\n";
#endif
            #if HAS_UNO_SKIA_LINUX_X11
            result += "\t HAS_UNO_SKIA_LINUX_X11\n";
#endif
            
            #if __MACCATALYST__
            result += "\t __MACCATALYST__\n";
            #endif
            
            #if __IOS__
            result += "\t __IOS__\n";
            #endif
            
            #if __TVOS__
            result += "\t __TVOS__\n";
#endif
            #if __WATCHOS__
            result += "\t __WATCHOS__\n";
#endif
            #if __ANDROID__
            result += "\t __ANDROID__\n";
#endif
            
            #if __WINDOWS__
            result += "\t __WINDOWS__\n";
#endif
            #if __SKIA__
            result += "\t __SKIA__\n";
#endif
            #if MACCATALYST
            result += "\t MACCATALYST\n";
            #endif
            
            #if MACOS
            result += "\t MACOS\n";
#endif
            
            //result += JsonSerializer.Serialize(deviceInfo, options);
            //result += JsonSerializer.Serialize(Windows.System.Profile.AnalyticsInfo.VersionInfo);
            var opt = new DumpOptions
            {
                IndentChar = '\t',
                IndentSize = 1,
            };
            
            result += "\n";
            result += "System.OperatingSystem:\n";
            result += $"\t .IsBrowser: {System.OperatingSystem.IsBrowser()}\n";
            result += $"\t .IsWasi: {System.OperatingSystem.IsWasi()}\n";
            result += $"\t .IsLinux: {System.OperatingSystem.IsLinux()}\n";
            result += $"\t .IsFreeBSD: {System.OperatingSystem.IsFreeBSD()}\n";
            result += $"\t .IsAndroid: {System.OperatingSystem.IsAndroid()}\n";
            result += $"\t .IsIOS: {System.OperatingSystem.IsIOS()}\n";
            result += $"\t .IsMacCatalyst: {System.OperatingSystem.IsMacCatalyst()}\n";
            result += $"\t .IsMacOS: {System.OperatingSystem.IsMacOS()}\n";
            result += $"\t .IsTvOS: {System.OperatingSystem.IsTvOS()}\n";
            result += $"\t .IsWatchOS: {System.OperatingSystem.IsWatchOS()}\n";
            result += $"\t .IsWindows: {System.OperatingSystem.IsWindows()}\n";
            result += "\n";
            result += "EasClientDeviceInformation:\n";
            result += ObjectDumper.Dump(deviceInfo, opt)+"\n";
            result += "\n";
            result += "Windows.System.Profile.AnalyticsInfo:\n";
            result += ObjectDumper.Dump(Windows.System.Profile.AnalyticsInfo.VersionInfo, opt)+"\n";
            result += $"\t .OsVersion: {GetVersionString()}"+"\n";
            result += $"\t .DeviceForm: {AnalyticsInfo.DeviceForm}\n";
            result += "\n";
            result += "Environment:\n";
            result += $"\t .MachineName: {Environment.MachineName}\n";
            result += ObjectDumper.Dump(Environment.OSVersion, opt)+"\n";
            result += "\n";
            result += "System.Runtime.InteropServices.RuntimeInformation:\n";
            result += $"\t .OSArchitecture: {RuntimeInformation.OSArchitecture}\n";
            result += $"\t .OSDescription: {RuntimeInformation.OSDescription}\n";
            result += $"\t .ProcessArchitecture: {RuntimeInformation.ProcessArchitecture}\n";
            result += $"\t .FrameworkDescription: {RuntimeInformation.FrameworkDescription}\n";
            result += $"\t .RuntimeIdentifier: {RuntimeInformation.RuntimeIdentifier}\n";
            result += "\n";

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            if (System.OperatingSystem.IsMacOS() || System.OperatingSystem.IsMacCatalyst() || System.OperatingSystem.IsTvOS() || System.OperatingSystem.IsWatchOS())
            // ✅ Desktop: macos
            // ✅ MacCatalyst
            // ❌ iOS
            {
                result += $"> system_profiler SPHardwareDataType\n";
                result += ExecuteCommand("system_profiler -json SPHardwareDataType");
                result += "\n";
                
                
            }
            
            if (System.OperatingSystem.IsLinux())
            {
                result += $"> lsb_release -a\n";
                result += ExecuteCommand("lsb_release -a");
                result += "\n";
            }
            
            if (System.OperatingSystem.IsBrowser())
            {

                var script = """
                             require([`${config.uno_app_base}/es5.js`], c => Bowser = c);
                             const browser = Bowser.getParser(window.navigator.userAgent);
                             JSON.stringify(browser.parse());
                             """;
                
                #if BROWSERWASM
                result += "> Bowser.getParser(window.navigator.userAgent)\n";
                var x = Uno.Foundation.WebAssemblyRuntime.InvokeJS(script);
                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(x);
                result += JsonSerializer.Serialize(dict, options);
                #endif
                result += "\n";
            }
            
            
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        return result;
    }

    static string GetVersionString()
    {
        var version = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;

        if (ulong.TryParse(version, out var v))
        {
            var v1 = (v & 0xFFFF000000000000L) >> 48;
            var v2 = (v & 0x0000FFFF00000000L) >> 32;
            var v3 = (v & 0x00000000FFFF0000L) >> 16;
            var v4 = v & 0x000000000000FFFFL;
            return $"{v1}.{v2}.{v3}.{v4}";
        }

        return version;
    }

    static string ExecuteCommand(string command)
    {
        // Create a new process
        using (Process process = new Process())
        {
            // Configure the process
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{command}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Start the process
            process.Start();
            
            // Capture output
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Command failed with exit code {process.ExitCode}: {error}");
            }

            return output;
        }
    }
}
