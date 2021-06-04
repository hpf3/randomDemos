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
    /// Interaction logic for Dictionary.xaml
    /// </summary>
    public partial class Dictionary : UserControl
    {

        public Dictionary()
        {
            InitializeComponent();
        }

        public Dictionary<string,string> GetDictionary()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            foreach (DictionaryItem item in DictionaryList.Items)
            {
                pairs.Add(item.txtKey.Text,item.txtValue.Text);
            }
            return pairs;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            DictionaryList.Items.Add(new DictionaryItem());
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (DictionaryList.Items.Count == 0){return;}//no need to reset an empty list

            DictionaryList.Items.Clear();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DictionaryList.SelectedIndex == -1) { return; }//sadly we cant delete nothingness

            DictionaryList.Items.Remove(DictionaryList.SelectedItem);
        }
    }
}
