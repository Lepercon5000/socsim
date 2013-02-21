using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SettlerSimLib;
using System.Windows.Media;
using System.Windows;

namespace SettlerAIApp.ViewModels
{
    public class HexVM : INotifyPropertyChanged
    {
        private double h;
        private double r;
        private double a;
        private double b;
        private double z;
        private double s;
        private void CalulateHexagonValues()
        {
            s = 50;
            h = Math.Sin(Math.PI / 6.0) * s;
            r = Math.Cos(Math.PI / 6.0) * s;
            b = s + 2 * h;
            a = 2 * r;
            z = 0;
        }


        private double offsetX;
        public double OffsetX
        {
            get
            {
                return offsetX;
            }
        }

        private double offsetY;
        public double OffsetY
        {
            get
            {
                return offsetY;
            }
        }

        public HexVM(IHex HexModel, SettlerBoard board)
        {
            hexModel = HexModel;
            CalulateHexagonValues();
            // Calculate how much it needs to shift
            if (board.GameBoard.GetRange(0, 3).Contains(HexModel))
            {
                offsetX = b + s;
                int indexValue = board.GameBoard.IndexOf(HexModel);
                offsetX += indexValue * (h + s);
                offsetY = indexValue * r;
            }
            else if (board.GameBoard.GetRange(3,4).Contains(HexModel))
            {
                offsetX = s + h;
                int indexValue = board.GameBoard.IndexOf(HexModel) - 3;
                offsetX += indexValue * (h + s);
                offsetY = (indexValue + 1) * r;
            }
            else if (board.GameBoard.GetRange(7, 5).Contains(HexModel))
            {
                int indexValue = board.GameBoard.IndexOf(HexModel) - 7;
                offsetX += indexValue * (h + s);
                offsetY = (indexValue + 2) * r;
            }
            else if (board.GameBoard.GetRange(12, 4).Contains(HexModel))
            {
                int indexValue = board.GameBoard.IndexOf(HexModel) - 12;
                offsetX += indexValue * (h + s);
                offsetY = (indexValue + 4) * r;
            }
            else if (board.GameBoard.GetRange(16, 3).Contains(HexModel))
            {
                int indexValue = board.GameBoard.IndexOf(HexModel) - 16;
                offsetX += indexValue * (h + s);
                offsetY = (indexValue + 6) * r;
            }
        }

        private IHex hexModel;

        public LandType TileType
        {
            get
            {
                return hexModel.LandType;
            }
        }

        public int DiceRollValue
        {
            get
            {
                return hexModel.DiceRollValue;
            }
        }

        public Geometry DataGeometry
        {
            get
            {
                PathSegmentCollection pathCollection = new PathSegmentCollection();
                pathCollection.Add(new LineSegment(new Point(h,a),false));
                pathCollection.Add(new LineSegment(new Point(h+s,a),false));
                pathCollection.Add(new LineSegment(new Point(b,r), false));
                pathCollection.Add(new LineSegment(new Point(h + s,z), false));
                pathCollection.Add(new LineSegment(new Point(h,z), false));
                PathFigure figure = new PathFigure(new Point(z,r), pathCollection, true);
                PathFigureCollection figures = new PathFigureCollection();
                figures.Add(figure);
                return new PathGeometry(figures);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseChange(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

}
