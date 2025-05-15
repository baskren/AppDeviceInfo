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
            //result += JsonSerializer.Serialize(deviceInfo, options);
            //result += JsonSerializer.Serialize(Windows.System.Profile.AnalyticsInfo.VersionInfo);

            result += "\n";
            result += ObjectDumper.Dump(deviceInfo);
            result += "\n";
            result += ObjectDumper.Dump(Windows.System.Profile.AnalyticsInfo.VersionInfo);
            result += $"\nOsVersion: {GetVersionString()}";
            result += "\nAnalyticsInfo.DeviceForm: ";
            result += ObjectDumper.Dump(Windows.System.Profile.AnalyticsInfo.DeviceForm);
            result += "\n";
            result += "\nEnvironment.MachineName: ";
            result += ObjectDumper.Dump(Environment.MachineName);
            result += "\nEnvironment.OSVersion: ";
            result += ObjectDumper.Dump(Environment.OSVersion);
            result += "\nEnvironment.OSVersion.Platform: ";
            result += ObjectDumper.Dump(Environment.OSVersion.Platform);
            result += "\nEnvironment.OSVersion.ServicePack: ";
            result += ObjectDumper.Dump(Environment.OSVersion.ServicePack);
            result += "\nEnvironment.OSVersion.VersionString: ";
            result += ObjectDumper.Dump(Environment.OSVersion.VersionString);
            result += "\n";
            result += "\nRuntimeInformation.OSArchitecture: ";
            result += ObjectDumper.Dump(RuntimeInformation.OSArchitecture);
            result += "\nRuntimeInformation.OSDescription: ";
            result += ObjectDumper.Dump(RuntimeInformation.OSDescription);
            result += "\nRuntimeInformation.ProcessArchitecture: ";
            result += ObjectDumper.Dump(RuntimeInformation.ProcessArchitecture);
            result += "\nRuntimeInformation.FrameworkDescription: ";
            result += ObjectDumper.Dump(RuntimeInformation.FrameworkDescription);
            result += "\nRuntimeInformation.RuntimeIdentifier: ";
            result += ObjectDumper.Dump(RuntimeInformation.RuntimeIdentifier);
            result += "\n";
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

}
