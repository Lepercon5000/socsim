using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SettlerSimLib;

namespace SettlerAIApp.ViewModels
{
    class LocationPointVM : INotifyPropertyChanged
    {
        public LocationPointVM(ILocationPoint pointModel, double x, double y)
        {
            locationPoint = pointModel;
            offsetX = x - (ElipseSize/2.0);
            offsetY = y - (ElipseSize/2.0);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseChange(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
