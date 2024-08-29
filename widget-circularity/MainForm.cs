
// <PackageReference Include = "IVSoftware.Portable.Disposable" Version="1.2.0" />
using IVSoftware.Portable.Disposable;

namespace widget_circularity
{
    public partial class MainForm : Form
    {
        internal static DisposableHost DHostInhibitGUI { get; } = new();
        public MainForm() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            buttonSetProgrammatically.Click += (sender, e) =>
            {
                using (DHostInhibitGUI.GetToken(sender: this))
                {
                    if(!DHostInhibitGUI.ContainsKey("Originator"))
                    {
                        DHostInhibitGUI["Originator"] = nameof(MainForm);
                    }
                    WidgetA.Entry = "Programmatic!";
                }
            };
            WidgetA.Location = new() { X = Right + 10, Y = Top };
            WidgetA.Show(this);
        }
        WidgetA WidgetA { get; } = new() { StartPosition = FormStartPosition.Manual };
        WidgetB WidgetB { get; } = new() { StartPosition = FormStartPosition.Manual };
    }
}
