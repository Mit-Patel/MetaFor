using GMap.NET;
using GMap.NET.MapProviders;

using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for GPSMap.xaml
    /// </summary>
    public partial class GPSMap : Window
    {
        string metadata = "gpstemp.txt";
        public GPSMap()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.DragButton = MouseButton.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 10;
            double lat = 28.64304924;
            double lon= 77.10689544666666;
            map.Position = new PointLatLng(lat, lon);
            map.ShowCenter = false;
            if (File.Exists(metadata))
            {
                string[] lines = File.ReadAllLines(metadata);

                foreach (string line in lines)
                {
                    string[] points = line.Split(',');
                    GMapMarker marker = new GMapMarker(new PointLatLng(Double.Parse(points[1]), Double.Parse(points[2])));
                    marker.Shape = new UserMarker(this, marker, points[0]);  
                    map.Markers.Add(marker);
                }
            }
        }
    }
}
