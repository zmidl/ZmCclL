using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ZmCclL.Data;

namespace ZmCclL.Controls.Waiting
{
    public abstract class WaitingBase : ContentControl
    {
        protected Storyboard Storyboard;

        public bool IsRunning { get => (bool)GetValue(IsRunningProperty); set => SetValue(IsRunningProperty, ValueBoxes.BooleanBox(value)); }
        public static readonly DependencyProperty IsRunningProperty = DependencyProperty.Register(nameof(IsRunning), typeof(bool), typeof(WaitingBase), new PropertyMetadata(ValueBoxes.TrueBox, (o, args) =>
           {
               var control = (WaitingBase)o;
               if ((bool)args.NewValue) control.Storyboard?.Resume();
               else control.Storyboard?.Pause();
           }));

        public int DotCount { get => (int)GetValue(DotCountProperty); set => SetValue(DotCountProperty, value); }
        public static readonly DependencyProperty DotCountProperty = DependencyProperty.Register(nameof(DotCount), typeof(int), typeof(WaitingBase), new FrameworkPropertyMetadata(ValueBoxes.Int5Box, FrameworkPropertyMetadataOptions.AffectsRender));

        public double DotInterval { get => (double)GetValue(DotIntervalProperty); set => SetValue(DotIntervalProperty, value); }
        public static readonly DependencyProperty DotIntervalProperty = DependencyProperty.Register(nameof(DotInterval), typeof(double), typeof(WaitingBase), new FrameworkPropertyMetadata(ValueBoxes.Double10Box, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush DotBorderBrush { get => (Brush)GetValue(DotBorderBrushProperty); set => SetValue(DotBorderBrushProperty, value); }
        public static readonly DependencyProperty DotBorderBrushProperty = DependencyProperty.Register(nameof(DotBorderBrush), typeof(Brush), typeof(WaitingBase), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        public double DotBorderThickness { get => (double)GetValue(DotBorderThicknessProperty); set => SetValue(DotBorderThicknessProperty, value); }
        public static readonly DependencyProperty DotBorderThicknessProperty = DependencyProperty.Register(nameof(DotBorderThickness), typeof(double), typeof(WaitingBase), new FrameworkPropertyMetadata(ValueBoxes.Double0Box, FrameworkPropertyMetadataOptions.AffectsRender));

        public double DotDiameter { get => (double)GetValue(DotDiameterProperty); set => SetValue(DotDiameterProperty, value); }
        public static readonly DependencyProperty DotDiameterProperty = DependencyProperty.Register(nameof(DotDiameter), typeof(double), typeof(WaitingBase), new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double DotSpeed { get => (double)GetValue(DotSpeedProperty); set => SetValue(DotSpeedProperty, value); }
        public static readonly DependencyProperty DotSpeedProperty = DependencyProperty.Register(nameof(DotSpeed), typeof(double), typeof(WaitingBase), new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double DotDelayTime { get => (double)GetValue(DotDelayTimeProperty); set => SetValue(DotDelayTimeProperty, value); }
        public static readonly DependencyProperty DotDelayTimeProperty = DependencyProperty.Register(nameof(DotDelayTime), typeof(double), typeof(WaitingBase), new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsRender));

        protected readonly Canvas PrivateCanvas = new Canvas { ClipToBounds = true };

        protected WaitingBase() => Content = PrivateCanvas;

        protected abstract void UpdateDots();

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            UpdateDots();
        }

        protected Ellipse CreateEllipse(int index)
        {
            var ellipse = new Ellipse();
            ellipse.SetBinding(WidthProperty, new Binding(DotDiameterProperty.Name) { Source = this });
            ellipse.SetBinding(HeightProperty, new Binding(DotDiameterProperty.Name) { Source = this });
            ellipse.SetBinding(Shape.FillProperty, new Binding(ForegroundProperty.Name) { Source = this });
            ellipse.SetBinding(Shape.StrokeThicknessProperty, new Binding(DotBorderThicknessProperty.Name) { Source = this });
            ellipse.SetBinding(Shape.StrokeProperty, new Binding(DotBorderBrushProperty.Name) { Source = this });
            return ellipse;
        }
    }
}
