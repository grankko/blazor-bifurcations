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

namespace BlazorBifurcations.WpfTestClient
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var bifurcator = new BlazorBifurcations.Calculations.Bifurcator(0.5, 2000, 4);
            var results = bifurcator.CalculateOverFertility(0, 4, 0.01);

            foreach (var result in results)
            {
                foreach (var point in result.Value)
                {
                    var newX = result.Key * 200;
                    var newY = 300 - (point * 200);


                    Ellipse myEllipse = new Ellipse();
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Colors.Red;
                    myEllipse.Fill = mySolidColorBrush;
                    myEllipse.StrokeThickness = 1;
                    myEllipse.Stroke = Brushes.Red;
                    myEllipse.Width = 1;
                    myEllipse.Height = 1;
                    Canvas.SetTop(myEllipse, newY);
                    Canvas.SetLeft(myEllipse, newX);
                    DiagramCanvas.Children.Add(myEllipse);
                }
            }

        }
    }
}
