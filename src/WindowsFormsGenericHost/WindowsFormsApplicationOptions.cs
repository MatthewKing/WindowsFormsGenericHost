using System.Windows.Forms;

namespace WindowsFormsGenericHost
{
    public class WindowsFormsApplicationOptions
    {
#if NETCOREAPP

        public HighDpiMode HighDpiMode { get; set; }
#endif
        public bool EnableVisualStyles { get; set; }
        public bool CompatibleTextRenderingDefault { get; set; }

        public WindowsFormsApplicationOptions()
        {
#if NETCOREAPP
            HighDpiMode = HighDpiMode.SystemAware;
#endif
            EnableVisualStyles = true;
            CompatibleTextRenderingDefault = false;
        }
    }
}
