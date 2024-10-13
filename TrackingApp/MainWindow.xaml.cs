using GMap.NET.MapProviders;
using GMap.NET;
using System.Windows;
using TrackingApp.Models;

namespace TrackingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.UpdateMapPosition += ViewModel_UpdateMapPosition;

            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MainMap.MapProvider = GMapProviders.OpenStreetMap;
            MainMap.Position = new PointLatLng(47.6062, -122.3321);
        }

        private void ViewModel_UpdateMapPosition(PointLatLng position)
        {
            MainMap.Position = position;

            MainMap.ZoomAndCenterMarkers(null);
        }
    }
}
