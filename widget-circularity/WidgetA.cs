using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    break;
            }
        }
    }
}
