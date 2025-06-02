using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp1;

namespace TRIGONFUNC
{
    public partial class MainWindow : Window
    {
        class TFData
        {
            public double X { get; set; }
            public double SinX { get; set; }
            public double CosX { get; set; }
            public double TgX { get; set; }
            public double CtgX { get; set; }

            public TFData(double arg)
            {
                X = arg;
                SinX = Math.Sin(X);
                CosX = Math.Cos(X);
                TgX = SinX / CosX;
                CtgX = 1 / TgX;
            }
        }

        List<TFData> tf = new List<TFData>();
        Window1 win1 = new Window1();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutUpdated -= Window_LayoutUpdated;
            Mouse.OverrideCursor = Cursors.AppStarting;
            win1.Owner = this;
            win1.Show();

            int n = 7, nMax = 1000001;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                try
                {
                    int n0 = int.Parse(args[1]);
                    if (n0 < 2 || n0 > nMax) throw new Exception();
                    else n = n0;
                }
                catch
                {
                    MessageBox.Show($"Неправильний параметр: {args[1]}\nДопустимі значення: від 2 до {nMax}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }
            }

            double step = 1.0 / (n - 1);
            for (int i = 0; i < n; i++)
            {
                double value = 100.0 * i / (n - 1);
                if (win1.progressBar1.Value != value)
                {
                    win1.progressBar1.Value = value;
                    DoEvents();
                }
                tf.Add(new TFData(Math.PI * i * step));
            }

            listView1.ItemsSource = tf;
            System.Threading.Thread.Sleep(1000);
            win1.Hide();
            Mouse.OverrideCursor = null;
            LayoutUpdated += Window_LayoutUpdated;
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            double maxWidth = gridView1.Columns.Select(c => c.ActualWidth).Max() + 10;
            for (int i = 1; i < gridView1.Columns.Count; i++)
                gridView1.Columns[i].Width = maxWidth;
            LayoutUpdated -= Window_LayoutUpdated;
            DoEvents();
            Left = (SystemParameters.WorkArea.Width - ActualWidth) / 2;
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
        }
    }
}
