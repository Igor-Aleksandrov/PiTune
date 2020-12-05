using PiTuneIdent.Domain;
using PiTuneIdent.Metods;
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

namespace PiTuneIdent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ControllerCentumPID controller = new ControllerCentumPID();
        ObjectModels objectConrtol = new ObjectModels(2,5,60);
        double[] x, xD, y, yD;
        double[,] yModel;

        public MainWindow()
        {
            InitializeComponent();

            btTunP.Click += btTunP_Click;
            btTunPI.Click += btTunPI_Click;
            btTunPID.Click += btTunPID_Click;

            tbGp.Text = objectConrtol.Gp.ToString();
            tbDt.Text = objectConrtol.Dt.ToString();
            tbTau1.Text = objectConrtol.Tau1.ToString();

        }

        private void readModel()
        {
            objectConrtol.Gp = double.Parse(tbGp.Text);
            objectConrtol.Dt = double.Parse(tbDt.Text);
            objectConrtol.Tau1 = double.Parse(tbTau1.Text);

            x = new double[int.Parse(tbTau1.Text) * 3];
            xD = new double[int.Parse(tbTau1.Text) * 3];
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = 1;
                xD[i] = 2;
            }

            //y = objectConrtol.CalcTrend(x);
            yD = objectConrtol.CalcTrendD(xD,1,0);
            yModel = controller.CalcTrendD(objectConrtol.Gp, objectConrtol.Tau1);

            PointCollection points = new PointCollection();
            for (int i = 1; i < yD.Length; i++)
            {
                points.Add(new Point(i, yD[i]));
            }
            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 1;
            polyline.Stroke = Brushes.Red;
            polyline.Points = points;

            int j = yModel.GetUpperBound(0)+1;
            PointCollection pointsM = new PointCollection();
            for (int i = 1; i < yModel.Length/j; i++)
            {
                pointsM.Add(new Point(i, yModel[0, i]));
            }
            Polyline polylineM = new Polyline();
            polylineM.StrokeThickness = 1;
            polylineM.Stroke = Brushes.Green;
            polylineM.Points = pointsM;

            canGraph.Children.Add(polyline);
            canGraph.Children.Add(polylineM);
        }

        private void btTunP_Click(object sender, RoutedEventArgs e)
        {
            readModel();

            controller.Convert(ZieglerNicholsMetod.TuningP(objectConrtol));

            txP.Text = controller.P.ToString().Substring(0, 8);
            txI.Text = "";
            txD.Text = "";
        }

        private void btTunPI_Click(object sender, RoutedEventArgs e)
        {
            readModel();

            controller.Convert(ZieglerNicholsMetod.TuningPI(objectConrtol));

            txP.Text = controller.P.ToString().Substring(0, 8);
            txI.Text = controller.I.ToString();
            txD.Text = "";
        }

        private void btTunPID_Click(object sender, RoutedEventArgs e)
        {
            readModel();

            controller.Convert(ZieglerNicholsMetod.TuningPID(objectConrtol));

            txP.Text = controller.P.ToString().Substring(0, 8);
            txI.Text = controller.I.ToString();
            txD.Text = controller.D.ToString();
        }
    }
}
