using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NalkaTools.WPF
{
    public static class BorderHelper
    {
        public static Square GetBorderBrushes(DependencyObject obj)
        {
            return (Square)obj.GetValue(BorderBrushesProperty);
        }

        public static void SetBorderBrushes(DependencyObject obj, Square value)
        {
            obj.SetValue(BorderBrushesProperty, value);
        }

        //Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushesProperty = DependencyProperty.RegisterAttached("BorderBrushes", typeof(Square), typeof(BorderHelper), new PropertyMetadata(OnBorderBrushesChanged));

        private static void OnBorderBrushesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control && e.NewValue is Square newValue)
            {
                control.SizeChanged += (sender, args) => RedrawBorders(sender as Control, GetBorderBrushes(sender as DependencyObject));
            }
        }

        private static void RedrawBorders(Control control, Square newValue)
        {
            string verticalPath = "M 0 0 l 0 10";
            string horizontalPath = "M 0 10 l 10 0";
            var geometryConverter = new GeometryConverter();
            var futureBrush = new VisualBrush();
            var futureBrushRoot = new Canvas
            {
                Width = control.ActualWidth + control.BorderThickness.Left + control.BorderThickness.Right,
                Height = control.ActualHeight + control.BorderThickness.Top + control.BorderThickness.Bottom
            };
            //futureBrushRoot.RowDefinitions.Add(new RowDefinition());
            //futureBrushRoot.RowDefinitions.Add(new RowDefinition());
            //futureBrushRoot.ColumnDefinitions.Add(new ColumnDefinition());
            //futureBrushRoot.ColumnDefinitions.Add(new ColumnDefinition());
            var leftBorder = new Path
            {
                //StrokeEndLineCap = PenLineCap.Square,
                //Data = (Geometry)geometryConverter.ConvertFromString(verticalPath),
                Stroke = newValue.Left,
                StrokeThickness = control.BorderThickness.Left,
                Data = new LineGeometry(new Point(0, 0), new Point(0, futureBrushRoot.Height))
            };
            leftBorder.SetValue(Grid.RowSpanProperty, 2);
            futureBrushRoot.Children.Add(leftBorder);
            var topBorder = new Path
            {
                //StrokeEndLineCap = PenLineCap.Square,
                //Data = (Geometry)geometryConverter.ConvertFromString(horizontalPath),
                Stroke = newValue.Top,
                StrokeThickness = control.BorderThickness.Top,
                Data = new LineGeometry(new Point(0, control.BorderThickness.Top), new Point(futureBrushRoot.Width, control.BorderThickness.Top))
            };
            topBorder.SetValue(Grid.ColumnSpanProperty, 2);
            futureBrushRoot.Children.Add(topBorder);
            var rightBorder = new Path
            {
                //StrokeEndLineCap = PenLineCap.Square,
                //Data = (Geometry)geometryConverter.ConvertFromString(verticalPath),
                Stroke = newValue.Right,
                StrokeThickness = control.BorderThickness.Right,
                Data = new LineGeometry(new Point(control.ActualWidth + control.BorderThickness.Left * 1.5, 0), new Point(control.ActualWidth + control.BorderThickness.Left * 1.5, futureBrushRoot.Height))
            };
            rightBorder.SetValue(Grid.RowSpanProperty, 2);
            rightBorder.SetValue(Grid.ColumnProperty, 1);
            futureBrushRoot.Children.Add(rightBorder);
            var bottomBorder = new Path
            {
                //StrokeEndLineCap = PenLineCap.Square,
                //Data = (Geometry)geometryConverter.ConvertFromString(horizontalPath),
                Stroke = newValue.Bottom,
                StrokeThickness = control.BorderThickness.Bottom,
                Data = new LineGeometry(new Point(0, control.ActualHeight + control.BorderThickness.Top / 2), new Point(futureBrushRoot.Width, control.ActualHeight + control.BorderThickness.Top / 2))
            };
            bottomBorder.SetValue(Grid.ColumnSpanProperty, 2);
            bottomBorder.SetValue(Grid.RowProperty, 1);
            futureBrushRoot.Children.Add(bottomBorder);

            futureBrush.Visual = futureBrushRoot;
            control.BorderBrush = futureBrush;
        }
    }
}