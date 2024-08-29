using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static widget_circularity.MainForm;

namespace widget_circularity
{
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
}
