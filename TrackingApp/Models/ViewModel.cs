using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackingApp.Logic;
using TrackingApp.Domain.Entities;
using TrackingApp.Commands;
using GMap.NET;
using System;
using GMap.NET.WindowsPresentation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;


namespace TrackingApp.Models
{
    public class ViewModel : ViewModelBase
    {
        private readonly TraineeApiService _traineeApiService;
        private readonly TrainingApiService _trainingApiService;


        public ObservableCollection<Trainee> Trainees { get; set; }
        public ObservableCollection<GMapMarker> Markers { get; }
        public ICommand LoadTraineesCommand { get; }
        public ICommand ShowPositionCommand { get; }
        public ICommand ShowMovementCommand { get; }
        public Trainee SelectedTrainee { get; set; }
        public event Action<PointLatLng> UpdateMapPosition;

        public ViewModel(TraineeApiService traineeApiService, TrainingApiService trainingApiService)
        {
            _traineeApiService = traineeApiService;
            _trainingApiService = trainingApiService;

            Trainees = new ObservableCollection<Trainee>();
            Markers = new ObservableCollection<GMapMarker>();

            LoadTraineesCommand = new RelayCommand(async () => await LoadTraineesAsync());
            ShowPositionCommand = new RelayCommand(async () => await ShowCurrentPositionAsync());
            ShowMovementCommand = new RelayCommand(async () => await ShowMovementAsync());
        }

        private async Task LoadTraineesAsync()
        {
            var trainees = await _traineeApiService.GetAllTraineesAsync();
            if (trainees != null)
            {
                Trainees.Clear();
                foreach (var trainee in trainees)
                {
                    Trainees.Add(trainee);
                }
            }
        }

        private async Task ShowCurrentPositionAsync()
        {
            if (SelectedTrainee != null)
            {
                var position = await _trainingApiService.GetCurrentPositionAsync(SelectedTrainee.Id);
                if (position != null)
                {
                    Markers.Clear();

                    SetMarker(position.Latitude.Value, position.Longitude.Value);

                    UpdateMapPosition(new PointLatLng(position.Latitude.Value, position.Longitude.Value));
                }
            }
        }

        private async Task ShowMovementAsync()
        {
            if (SelectedTrainee != null)
            {
                var positions = await _trainingApiService.GetTrainingDataAsync(SelectedTrainee.Id);
                if (positions != null)
                {
                    Markers.Clear();

                    foreach (var position in positions)
                    {
                        SetMarker(position.Latitude.Value, position.Longitude.Value);
                        UpdateMapPosition(new PointLatLng(position.Latitude.Value, position.Longitude.Value));
                        await Task.Delay(2000);
                    }
                }
            }
        }

        private UIElement CreateMarkerShape(Trainee selectedTrainee)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var markerShape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Fill = Brushes.Red
            };

            var markerTextBlock = new TextBlock
            {
                Text = $"{selectedTrainee.Name}, Rank: {selectedTrainee.Rank.Name}, Country: {selectedTrainee.Country.Name}",
                Foreground = Brushes.Black,
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            stackPanel.Children.Add(markerTextBlock);
            stackPanel.Children.Add(markerShape);

            return stackPanel;
        }

        private void SetMarker(double lat, double longit)
        {
            var point = new PointLatLng(lat, longit);
            var marker = new GMapMarker(point)
            {
                Shape = CreateMarkerShape(SelectedTrainee),
                ZIndex = int.MaxValue
            };

            Markers.Add(marker);
        }
    }
}
