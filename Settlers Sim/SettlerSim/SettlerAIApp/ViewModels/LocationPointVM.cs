using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SettlerSimLib;
using SettlerSimLib.Building;

namespace SettlerAIApp.ViewModels
{
    class LocationPointVM : INotifyPropertyChanged
    {
        public LocationPointVM(ILocationPoint pointModel, double x, double y)
        {
            locationPoint = pointModel;
            offsetX = x - (ElipseSize/2.0);
            offsetY = y - (ElipseSize/2.0);
            BuildingInterface.Instance.CityWasBuilt += new CityBuiltHandler(CityWasBuilt);
            BuildingInterface.Instance.SettlementWasBuilt += new SettlementBuiltHandler(SettlementWasBuilt);
        }

        void SettlementWasBuilt(object sender, SettlementBuiltArgs args)
        {
            if (args.Settlement == this.locationPoint)
            {
                RaiseChange("PlayerOwner");
                RaiseChange("IsASettlement");
                RaiseChange("IsACity");
            }
        }

        void CityWasBuilt(object sender, CityBuiltArgs args)
        {
            if (args.City == this.locationPoint)
            {
                RaiseChange("IsASettlement");
                RaiseChange("IsACity");
            }
        }

        private ILocationPoint locationPoint;
        public ILocationPoint LocationPoint
        {
            get
            {
                return locationPoint;
            }
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

        public int ElipseSize
        {
            get
            {
                return 10;
            }
        }

        public bool IsAHarbor
        {
            get
            {
                return locationPoint.Harbor != SeaHarbor.NotHarbor;
            }
        }

        public int PlayerOwner
        {
            get
            {
                return this.locationPoint.PlayerOwner;
            }
        }

        public bool IsACity
        {
            get
            {
                return locationPoint.IsACity;
            }
        }

        public bool IsASettlement
        {
            get
            {
                return !IsACity;
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
