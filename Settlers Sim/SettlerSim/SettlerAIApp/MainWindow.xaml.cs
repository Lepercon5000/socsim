using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;
using System.ComponentModel;
using SettlerSimLib;
using SettlerSimLib.Building;

namespace SettlerAIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private BackgroundWorker bgWorker;

        public MainWindow()
        {
            InitializeComponent();
            //bgWorker = new BackgroundWorker();
            //bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            //bgWorker.RunWorkerAsync();
        }

        //void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    SettlerBoard testing = SettlerSimLib.SettlerBoard.Instance;
        //    BuildingInterface buildTest = BuildingInterface.Instance;
        //    Player testPlayer = new Player();
        //    System.Threading.Thread.Sleep(5 * 1000);
        //    ILocationPoint locPoint = testing.GameBoard[0].LocationPointsEnum.First();
        //    IEdge edge = locPoint.Edges.First();
        //    locPoint = edge.GetOppositePoint(locPoint);
        //    edge = locPoint.Edges.First((ed) => ed != edge);
        //    locPoint = edge.GetOppositePoint(locPoint);
        //    buildTest.BuildSettlement(locPoint, testPlayer);
        //    System.Threading.Thread.Sleep(5 * 1000);
        //    buildTest.BuildCity(locPoint, testPlayer);
        //}
    }
}
