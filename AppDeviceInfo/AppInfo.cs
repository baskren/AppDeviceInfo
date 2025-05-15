using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppDeviceInfo;
internal static class AppInfo
{
    public static string Get()
    {
        var asm = typeof(App).GetType().Assembly;

        var output = new StringBuilder();

        //var appInstallerInfo = Package.Current.GetAppInstallerInfo();
        var packageId = Package.Current.Id;

        var date = DateTime.Now;

        try
        {

            output.AppendLine($"ASSEMBLY:");
            output.AppendLine($"    FullName: {asm.FullName} <<<");
            output.AppendLine($"    EntryPoint: {asm.EntryPoint} ");
            output.AppendLine($"    ImageRuntimeVersion: {asm.ImageRuntimeVersion}");
            output.AppendLine($"    HostContext: {asm.HostContext}");
            output.AppendLine($">>> GetName().Name: {asm.GetName().Name} <<<<");
            output.AppendLine($">>> GetName().Version: {asm.GetName().Version} <<<");
            output.AppendLine($"    GetName().CultureName: {asm.GetName().CultureName}");
            output.AppendLine($"    GetName().FullName: {asm.GetName().FullName}      ");
            output.AppendLine($"    GetName().ContentType: {asm.GetName().ContentType}");
            output.AppendLine($"    GetName().CultureInfo: {asm.GetName().CultureInfo}");
            output.AppendLine($"    GetName().CultureName: {asm.GetName().CultureName}");
            output.AppendLine($"    GetName().Flags: {asm.GetName().Flags}");
            output.AppendLine($"    Location: {asm.Location}");
            output.AppendLine($"");
            output.AppendLine($"PACKAGE");
            output.AppendLine($"    DisplayName: {Windows.ApplicationModel.Package.Current.DisplayName}");
            output.AppendLine($">>> PublisherDisplayName: {Package.Current.PublisherDisplayName} <<< !WASM, !iOS");
            output.AppendLine($">>> Description: {Package.Current.Description} <<< !WASM, !iOS");

#if !__IOS__
            output.AppendLine($">>> InstalledDate:{Package.Current.InstalledDate} <<<");
#endif

#if !WINDOWS && !__IOS__
            output.AppendLine($">>> InstallDate:{Package.Current.InstallDate} <<< ");
#endif

            output.AppendLine($"PackageId: ");
            output.AppendLine($"    Name: {packageId.Name} ");
            output.AppendLine($"    VERSION:");
            output.AppendLine($">>>     Major: {packageId.Version.Major}  !WASM");
            output.AppendLine($">>>     Minor: {packageId.Version.Minor}  !WASM");
            output.AppendLine($">>>     Build: {packageId.Version.Build}   !WASM");
            output.AppendLine($">>>     Revsn: {packageId.Version.Revision} MAX:65535  !WASM, !iOS");
            output.AppendLine($"    Architecture: {packageId.Architecture} ");
            output.AppendLine($"    FamilyName: {packageId.FamilyName} ");
            output.AppendLine($"    FullName: {packageId.FullName} (iOS Build Appended)");
            output.AppendLine($">>> Publisher: {packageId.Publisher} <<<  !WASM ");
            output.AppendLine($"    PublisherId: {packageId.PublisherId} ");
            output.AppendLine($"    ResourceId: {packageId.ResourceId}             ");

#if !WINDOWS

            output.AppendLine($"    Author: {packageId.Author} ");
            output.AppendLine($"    ProductId: {packageId.ProductId} ");

#endif
            output.AppendLine($"APP CONTEXT");
            output.AppendLine($"    BaseDirectory: {System.AppContext.BaseDirectory}");
            output.AppendLine($"    TargetFrameworkName: {AppContext.TargetFrameworkName}");
            output.AppendLine($"APP DOMAIN");
            output.AppendLine($"    FriendlyName: {AppDomain.CurrentDomain.FriendlyName}");
            output.AppendLine($"    BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
            output.AppendLine($"    DynamicDirectory: {AppDomain.CurrentDomain.DynamicDirectory}");
            output.AppendLine($"    Id: {AppDomain.CurrentDomain.Id}");
            output.AppendLine($"    SetupInformation.ApplicationBase: {AppDomain.CurrentDomain.SetupInformation.ApplicationBase}");


            output.AppendLine($"");
            var informationalVersionString = asm
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            output.AppendLine($"ASM.InformationalVersion: {informationalVersionString}");

            var build = asm.GetName().Version.Build * ushort.MaxValue + asm.GetName().Version.Revision;
            output.AppendLine($"asm.Version.DecodedBuild: {build}");
        }
        catch (Exception ex)
        {
            output.AppendLine(ex.ToString());

        }

        return output.ToString();
    }
}
