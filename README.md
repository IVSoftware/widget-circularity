As I understand it, your main goal is to determine whether the GUI is responding to user interaction or to a programmatic command. What has worked for me in production code _and_ has the simplicity of a bool like `InhibitGUI` but is more robust in terms of threading, is to leverage `System.IDisposable` (so one could say that it's built-in in that sense) along with `using` blocks.

[![User input v programmatic][1]][1]

You could roll your own `IDisposable` object, or use a third-party NuGet as shown here. This particular ref count package allows reentrant calls to `GetToken()` and this means that even if different widgets have requested tokens in different using blocks, you can still inspect the tokens to see the order in which they occurred. It also features a `properties` dictionary that persists until the final token dispose. I've demonstrated this here by setting an "Originator" property if one is not already present in this cycle.

```

// <PackageReference Include = "IVSoftware.Portable.Disposable" Version="1.2.0" />
using IVSoftware.Portable.Disposable;
public partial class MainForm : Form
{
    // <PackageReference Include = "IVSoftware.Portable.Disposable" Version="1.2.0" />
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
```

___

This demonstrates how a client might check the reference count to determine whether or not the source is programmatic.

```

using static widget_circularity.MainForm;
public partial class WidgetA : Form, INotifyPropertyChanged
{   
    .
    .
    .

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        switch (propertyName)
        {
            case nameof(Entry):
                textBox.Text = Entry;

                // Check to see whether any using blocks have checked out a token.
                if(DHostInhibitGUI.IsZero())
                {
                    // If none have been checked out:
                    Text = "Origin: User Interaction";
                }
                else
                {
                    // But if one or more refcount tokens have been checked out:
                    Text = $"Origin: {DHostInhibitGUI["Originator"]}";
                    BeginInvoke(() => textBox.Focus());
                    int tokenCount = 0;
                    // If there are multiple using blocks in play, you 
                    // can determine the order that it happened. Usually,
                    // the sender of the first token will determine your
                    // processing order.
                    foreach (var token in DHostInhibitGUI.Tokens)
                    {
                        Debug.WriteLine($"Sender {tokenCount++}: {token.Sender.GetType().Name}");
                    }
                }
                break;
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}
```


  [1]: https://i.sstatic.net/O9XFKBV1.png