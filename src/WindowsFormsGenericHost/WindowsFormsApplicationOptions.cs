namespace WindowsFormsGenericHost;

public class WindowsFormsApplicationOptions
{
#if !NETFRAMEWORK
    public System.Windows.Forms.HighDpiMode HighDpiMode { get; set; }
#endif
    public bool EnableVisualStyles { get; set; }
    public bool CompatibleTextRenderingDefault { get; set; }

    public WindowsFormsApplicationOptions()
    {
#if !NETFRAMEWORK
        HighDpiMode = System.Windows.Forms.HighDpiMode.SystemAware;
#endif
        EnableVisualStyles = true;
        CompatibleTextRenderingDefault = false;
    }
}
