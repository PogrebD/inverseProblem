using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using practika;
using System;
using System.Drawing;
using System.Windows;


namespace OxyplotGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GridIniter initer = new();
            grid = initer.Init(0, 1, 0);
            GraphModel = CreatePlotModel();
            this.DataContext = this;
            Solution _solution;

           _solution = new(grid);
            //_solution.stationary();
            /*_solution.Run();
            //_solution.stationary();




            grid = initer.Init(0, 2, 0);
            Solution _solution1;

            _solution1 = new(grid);
            _solution1.Run();
            //_solution1.stationary();



            grid = initer.Init(0, 0, 0);
            Solution _solution2;

            _solution2 = new(grid);
            _solution2.Run();
            //_solution2.stationary();



            grid = initer.Init(1, 1, 2);
            Solution _solution3;

            _solution3 = new(grid);
            _solution3.Run();
            //_solution3.stationary();



            grid = initer.Init(1, 2, 1);
            Solution _solution4;

            _solution4 = new(grid);
            _solution4.Run();
            //_solution4.stationary();*/


            int u = 0;
        }

        Grid grid = new();



        public PlotModel GraphModel { get; set; }
        public List<LineAnnotation> LineList { get; set; }
        public List<PointAnnotation> ReceiverList { get; set; }

        public PointAnnotation Spring;

        public LineAnnotation CreateVerLine(double X, double YBottom, double YTop)
        {
            return new()
            {
                StrokeThickness = 1,
                Color = OxyColor.FromArgb(70, 0, 0, 0),
                Type = LineAnnotationType.Vertical,
                X = X,
                MaximumY = YTop,
                MinimumY = YBottom,
                LineStyle = LineStyle.Solid,
            };
        }
        public LineAnnotation CreateHorLine(double Y, double XLeft, double XRight)
        {
            return new()
            {
                StrokeThickness = 1,
                Color = OxyColor.FromArgb(70, 0, 0, 0),
                Type = LineAnnotationType.Horizontal,
                Y = Y,
                MaximumX = XRight,
                MinimumX = XLeft,
                LineStyle = LineStyle.Solid
            };
        }
        public LineAnnotation CreateVerLineObj(double X, double YBottom, double YTop)
        {
            return new()
            {
                StrokeThickness = 3,
                Color = OxyColors.Red,
                Type = LineAnnotationType.Vertical,
                X = X,
                MaximumY = YTop,
                MinimumY = YBottom,
                LineStyle = LineStyle.Solid
            };
        }
        public LineAnnotation CreateHorLineObj(double Y, double XLeft, double XRight)
        {
            return new()
            {
                StrokeThickness = 3,
                Color = OxyColors.Red,
                Type = LineAnnotationType.Horizontal,
                Y = Y,
                MaximumX = XRight,
                MinimumX = XLeft,
                LineStyle = LineStyle.Solid
            };
        }

        public PointAnnotation CreatePointReciver(double R, double Z)
        {
            return new PointAnnotation()
            {
                Size = 4,
                Fill = OxyColors.Orange,
                Shape = MarkerType.Circle,
                X = R,
                Y = Z,
                Text = string.Format("({0};{1})", R, Z),
                TextMargin = 1,
                FontSize = 9,
            };
        }

        public PointAnnotation CreatePointSpring(double R, double Z)
        {
            return new PointAnnotation()
            {
                Size = 4,
                Fill = OxyColors.Blue,
                Shape = MarkerType.Circle,
                X = R,
                Y = Z,
                Text = string.Format("({0};{1})", R, Z),
                TextMargin = 1,
                FontSize = 7,
            };
        }

        private PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel();
            LineList = new List<LineAnnotation>();
            ReceiverList = new();


            //Рисуем оси координат
            var verticalAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = Config.y1, Maximum = Config.y2 };
            var horizontalAxis = new LinearAxis { Position = AxisPosition.Bottom, Minimum = Config.x1, Maximum = Config.x2 };
            plotModel.Axes.Add(verticalAxis);
            plotModel.Axes.Add(horizontalAxis);

            for (int i = 0; i < Config.Points.Length / 2; i++)
            {
                ReceiverList.Add(CreatePointReciver(Config.Points[i, 0], Config.Points[i, 1]));
            }

            //Добавляем Объект
            for (int i = 0; i < grid.objects.Count; i++)
            {
                LineList.Add(CreateHorLineObj(grid.objects[i].z1, grid.objects[i].r1, grid.objects[i].r2));
                LineList.Add(CreateHorLineObj(grid.objects[i].z2, grid.objects[i].r1, grid.objects[i].r2));

                LineList.Add(CreateVerLineObj(grid.objects[i].r1, grid.objects[i].z1, grid.objects[i].z2));
                LineList.Add(CreateVerLineObj(grid.objects[i].r2, grid.objects[i].z1, grid.objects[i].z2));
            }

            //Добавляем коненоэлементную сетку по вертикали
            foreach (var point in grid.dischargeFactor.pointsAllR)
            {
                LineList.Add(CreateVerLine(point, Config.y1, Config.y2));
            }

            //Добавляем коненоэлементную сетку по горизонтали
            foreach (var point in grid.dischargeFactor.pointsAllZ)
            {
                LineList.Add(CreateHorLine(point, Config.x1, Config.x2));
            }

            //Рисуем все линии
            foreach (var line in LineList)
            {
                plotModel.Annotations.Add(line);
            }


            foreach (var point in ReceiverList)
            {
                plotModel.Annotations.Add(point);
            }

            plotModel.Annotations.Add(CreatePointSpring(Config.MainSpring[0, 0], Config.MainSpring[0, 1]));

            return plotModel;
        }
    }
}