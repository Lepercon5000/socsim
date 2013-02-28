using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SettlerSimLib;
using SettlerSimLib.Building;

namespace SettlerAIApp.ViewModels
{
    public class EdgeVM : INotifyPropertyChanged
    {
        private IEdge edge;
        public IEdge Edge
        {
            get
            {
                return edge;
            }
        }

        public EdgeVM(IEdge model, double startX, double startY, double endX, double endY)
        {
            edge = model;
            centerX = startX;
            centerY = startY;

            rotateAngle = (180.0 / Math.PI)*Math.Atan((centerY - endY) / (centerX - endX));
            if (rotateAngle == 0.0)
            {
                if (centerX - endX > 0)
                    rotateAngle = 180.0;
                else
                    rotateAngle = 0;
            }
            if (rotateAngle < 0)
            {
                if ((centerY - endY) > 0)
                    rotateAngle = 360 + rotateAngle;
                else
                    rotateAngle = 180 - (-1 * rotateAngle);
            }
            BuildingInterface.Instance.RoadWasBuilt += new RoadBuiltHandler(RoadWasBuilt);
        }

        void RoadWasBuilt(object sender, RoadBuiltArgs args)
        {
            if (args.Road == this.edge)
                RaiseChange("PlayerOwner");
        }

        private double centerX;
        public double CenterX
        {
            get
            {
                return centerX;
            }
        }

        private double centerY;
        public double CenterY
        {
            get
            {
                return centerY;
            }
        }

        private double rotateAngle;
        public double RotateAngle
        {
            get
            {
                return rotateAngle;
            }
        }

        public int PlayerOwner
        {
            get
            {
                return this.edge.PlayerOwner;
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
