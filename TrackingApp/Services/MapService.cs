using GMap.NET.WindowsPresentation;
using GMap.NET;

namespace TrackingApp.Services
{
    public class MapService
    {
        private GMapControl _mapControl;

        public void SetMapControl(GMapControl mapControl)
        {
            _mapControl = mapControl;
        }

        public void UpdatePosition(PointLatLng position)
        {
            if (_mapControl != null)
            {
                _mapControl.Position = position;
            }
        }
    }
}
