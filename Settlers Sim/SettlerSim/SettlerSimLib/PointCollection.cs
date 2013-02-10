using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SettlerSimLib
{
    internal class PointCollection : IEnumerable<LocationPoint>
    {
        public PointCollection()
        {
            points = new LocationPoint[6];
            for (int i = 0; i < 6; ++i)
                points[i] = null;
        }
        private LocationPoint[] points;
        public LocationPoint this[int index]
        {
            get
            {
                return points[index];
            }
            set
            {
                points[index] = value;
            }
        }
        public LocationPoint this[LocationPoints index]
        {
            get
            {
                return this[(int)index];
            }
            set
            {
                this[(int)index] = value;
            }
        }

        public LocationPoint Left
        {
            get
            {
                return this[LocationPoints.Left];
            }
            set
            {
                this[LocationPoints.Left] = value;
            }
        }
        public LocationPoint TopLeft
        {
            get
            {
                return this[LocationPoints.TopLeft];
            }
            set
            {
                this[LocationPoints.TopLeft] = value;
            }
        }
        public LocationPoint TopRight
        {
            get
            {
                return this[LocationPoints.TopRight];
            }
            set
            {
                this[LocationPoints.TopRight] = value;
            }
        }
        public LocationPoint Right
        {
            get
            {
                return this[LocationPoints.Right];
            }
            set
            {
                this[LocationPoints.Right] = value;
            }
        }
        public LocationPoint BottomRight
        {
            get
            {
                return this[LocationPoints.BottomRight];
            }
            set
            {
                this[LocationPoints.BottomRight] = value;
            }
        }
        public LocationPoint BottomLeft
        {
            get
            {
                return this[LocationPoints.BottomLeft];
            }
            set
            {
                this[LocationPoints.BottomLeft] = value;
            }
        }

        public LocationPointEnum GetEnumerator()
        {
            return new LocationPointEnum(points);
        }

        IEnumerator<LocationPoint> IEnumerable<LocationPoint>.GetEnumerator()
        {
            return (IEnumerator<LocationPoint>)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<LocationPoint>)GetEnumerator();
        }
    }

    internal class LocationPointEnum : IEnumerator<LocationPoint>
    {
        private LocationPoint[] points;

        private int position = -1;

        public LocationPointEnum(LocationPoint[] list)
        {
            points = list;
        }

        public LocationPoint Current
        {
            get
            {
                try
                {
                    return points[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            ++position;
            return (position < points.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
        }
    }
}
