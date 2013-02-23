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
            model = new SettlerBoard();
            hexTiles = new ObservableCollection<HexVM>();
            locationPoints = new ObservableCollection<LocationPointVM>();
            foreach (IHex hex in model.GameBoard)
            {
                HexVM hexVM = new HexVM(hex, model);
                hexTiles.Add(hexVM);
                foreach (ILocationPoint locPoint in hex.LocationPointsEnum)
                {
                    if (!LocationPoints.Where((point) => point.LocationPoint == locPoint).Any())
                    {
                        if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.Left) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX, hexVM.OffsetY + hexVM.r));
                        else if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.TopLeft) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h, hexVM.OffsetY));
                        else if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.TopRight) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h + hexVM.s, hexVM.OffsetY));
                        else if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.Right) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.b, hexVM.OffsetY + hexVM.r));
                        else if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.BottomRight) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h + hexVM.s, hexVM.OffsetY + (2*hexVM.r)));
                        else if(hex.LocationPointsEnum.ElementAt((int)SettlerSimLib.LocationPoints.BottomLeft) == locPoint)
                            LocationPoints.Add(new LocationPointVM(locPoint, hexVM.OffsetX + hexVM.h, hexVM.OffsetY + (2*hexVM.r)));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseChange(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
