
namespace widget_circularity
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            #region L O O K S    C I R C U L A R    B U T    I T ' S    N O T
            WidgetA.PropertyChanged += (sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(WidgetA.Entry):
                        WidgetB.Entry = WidgetA.Entry;
                        break;
                }
            };           
            WidgetB.PropertyChanged += (sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(WidgetB.Entry):
                        WidgetA.Entry = WidgetB.Entry;
                        break;
                }
            };
            #endregion L O O K S    C I R C U L A R    B U T    I T ' S    N O T

            WidgetA.Location = new() { X = Right + 10, Y = Top };
            WidgetB.Location = new() { X = Right + 10, Y = Top + WidgetA.Height + 10};
            WidgetA.Show(this);
            WidgetB.Show(this);
        }
        WidgetA WidgetA { get; } = new() { StartPosition = FormStartPosition.Manual };
        WidgetA WidgetB { get; } = new() { StartPosition = FormStartPosition.Manual };
    }
}
