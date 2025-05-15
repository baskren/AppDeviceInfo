using System.Reflection;
using System.Text;

namespace AppDeviceInfo;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();

        //Loaded += MainPage_Loaded;
        //MainPage_Loaded(null,null);
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        // textBlock.Text = AppInfo.Get();
        textBlock.Text = DeviceInfo.Get();
    }

    private void refreshButton_Click(object sender, RoutedEventArgs e)
    {
        MainPage_Loaded(sender,e);
    }
}

