using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace string_replacement
{
    /// <summary>
    /// Interaction logic for DictionaryItem.xaml
    /// </summary>
    public partial class DictionaryItem : UserControl
    {
        public DictionaryItem()
        {
            InitializeComponent();
        }
        public DictionaryItem(string key, string value)
        {
            InitializeComponent();
            txtKey.Text = key;
            txtValue.Text = value;
        }
    }
}
