using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET.WindowsPresentation;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for UserMarker.xaml
    /// </summary>
    public partial class UserMarker : UserControl
    {
        Popup _popup;
        Label _label;
        GMapMarker _marker;
        GPSMap _mainWindow;
        public UserMarker(GPSMap window, GMapMarker marker, string title)
        {
            InitializeComponent();
            this._mainWindow = window;
            this._marker = marker;

            //_popup = new Popup();
            //_label = new Label();
            Icon.ToolTip = title;
            this.Unloaded += new RoutedEventHandler(CustomMarkerDemo_Unloaded);
            this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            //this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

            /*_popup.Placement = PlacementMode.Mouse;
            {
                _label.Background = Brushes.Transparent;
                _label.Foreground = Brushes.Black;
                _label.BorderBrush = Brushes.WhiteSmoke;
                _label.BorderThickness = new Thickness(2);
                _label.Padding = new Thickness(2);
                _label.FontSize = 22;
                _label.Content = title;
            }
            _popup.Child = _label;*/
        }
        void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Icon.Source.CanFreeze)
            {
                Icon.Source.Freeze();
            }
        }

        void CustomMarkerDemo_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= new RoutedEventHandler(CustomMarkerDemo_Unloaded);
            this.Loaded -= new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged -= new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            this.MouseEnter -= new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave -= new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove -= new MouseEventHandler(CustomMarkerDemo_MouseMove);
            //this.MouseLeftButtonUp -= new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            //this.MouseLeftButtonDown -= new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

            _marker.Shape = null;
            Icon.Source = null;
            Icon = null;
            _popup = null;
            _label = null;
        }

        void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Point p = e.GetPosition(_mainWindow.map);
                _marker.Position = _mainWindow.map.FromLocalToLatLng((int)(p.X), (int)(p.Y));
            }
        }

        void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {
                Mouse.Capture(this);
            }
        }

        void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //_marker.ZIndex -= 10000;
            //_popup.IsOpen = false;
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //_marker.ZIndex += 10000;
            //_popup.IsOpen = true;
        }
    }
}
