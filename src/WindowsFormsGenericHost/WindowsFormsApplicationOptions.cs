using System.Windows.Forms;

namespace WindowsFormsGenericHost
{
    public class WindowsFormsApplicationOptions
    {
#if !NETFRAMEWORK

        public HighDpiMode HighDpiMode { get; set; }
#endif
        public bool EnableVisualStyles { get; set; }
        public bool CompatibleTextRenderingDefault { get; set; }

        public WindowsFormsApplicationOptions()
        {
#if !NETFRAMEWORK
            HighDpiMode = HighDpiMode.SystemAware;
#endif
            EnableVisualStyles = true;
            CompatibleTextRenderingDefault = false;
        }
    }
}
