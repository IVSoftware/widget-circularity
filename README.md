This might be helpful, but it's less of a "built-in" and more of a pattern or best practice. Let's make it so that whenever the text changes in WidgetA, the text in WidgetB will track. And then, also, whenever the text changes in WidgetB, widget A will track. What prevents this from being circular?

#### Main Form

```csharp
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
```

___

A common pattern is to have the widgets (for which we'll go ahead and implement `INotifyProperyChanged`) check the current value and fire the event only if the value really _changes_.

public partial class WidgetA : Form, INotifyPropertyChanged
{
    public WidgetA()
    {
        InitializeComponent();
        textBox.TextChanged += (sender, e) => Entry = textBox.Text;
        textBox.KeyDown += (sender, e) =>
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    BeginInvoke(()=>textBox.SelectAll());
                    break;
            }
        };
    }

    public string Entry
    {
        get => _entry;
        set
        {
            if (!Equals(_entry, value))
            {
                _entry = value;
                OnPropertyChanged();
            }
        }
    }
    string _entry = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        switch (propertyName)
        {
            case nameof(Entry):
                textBox.Text = Entry;
                break;
        }
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        switch (e.CloseReason)
        {
            case CloseReason.UserClosing:
                e.Cancel = true;
                Hide();
                break;
        }
    }
}

