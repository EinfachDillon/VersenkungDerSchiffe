using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VersenkungDerSchiffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void AddButton(Button button)
        {
            raster.Children.Add(button);
        }
        public void RemoveButton(Button button)
        {
            raster.Children.Remove(button);
        }
        public void addRow(RowDefinition rowDef)
        {
            raster.RowDefinitions.Add(rowDef);
        }

    }
}