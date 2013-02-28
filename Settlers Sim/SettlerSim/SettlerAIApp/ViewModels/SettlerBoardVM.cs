using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SettlerSimLib;

namespace SettlerAIApp.ViewModels
{
    class SettlerBoardVM : INotifyPropertyChanged
    {
        public SettlerBoardVM()
        {
            model = SettlerBoard.Instance;
            hexTiles = new ObservableCollection<HexVM>();
            locationPoints = new ObservableCollection<LocationPointVM>();
            edges = new ObservableCollection<EdgeVM>();
            foreach (IHex hex in model.GameBoard)
            {
                HexVM hexVM = new HexVM(hex, model);
                hexTiles.Add(hexVM);
                foreach (ILocationPoint locPoint in hex.LocationPointsEnum)
                {
                    if (!LocationPoints.Where((point) => point.LocationPoint == locPoint).Any())
                    {
                        if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.Left) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX, hexVM.OffsetY + hexVM.r));
                        else if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.TopLeft) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h, hexVM.OffsetY));
                        else if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.TopRight) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h + hexVM.s, hexVM.OffsetY));
                        else if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.Right) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.b, hexVM.OffsetY + hexVM.r));
                        else if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.BottomRight) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h + hexVM.s, hexVM.OffsetY + (2 * hexVM.r)));
                        else if (hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.BottomLeft) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h, hexVM.OffsetY + (2 * hexVM.r)));
                    }
                }
            }
            foreach (LocationPointVM locPoint in this.LocationPoints)
            {
                foreach (IEdge edge in locPoint.LocationPoint.Edges)
                {
                    if (!Edges.Any((edgeAny) => edgeAny.Edge == edge))
                    {
                        LocationPointVM pointVM = this.LocationPoints.First((findPoint) => findPoint.LocationPoint == edge.GetOppositePoint(locPoint.LocationPoint));
                        EdgeVM edgeVM = new EdgeVM(edge, locPoint.OffsetX, locPoint.OffsetY, pointVM.OffsetX, pointVM.OffsetY);
                        Edges.Add(edgeVM);
                    }
                }
            }
        }

        private SettlerBoard model;

        private ObservableCollection<HexVM> hexTiles;
        public ObservableCollection<HexVM> HexTiles
        {
            get
            {
                return hexTiles;
            }
            set
            {
                hexTiles = value;
                RaiseChange("HexTiles");
            }
        }

        private ObservableCollection<LocationPointVM> locationPoints;
        public ObservableCollection<LocationPointVM> LocationPoints
        {
            get
            {
                return locationPoints;
            }
            set
            {
                locationPoints = value;
                RaiseChange("LocationPoints");
            }
        }

        private ObservableCollection<EdgeVM> edges;
        public ObservableCollection<EdgeVM> Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = value;
                RaiseChange("Edges");
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
