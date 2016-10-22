using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Aima.Domain.Wells;
using Aima.Search;
using Aima.Search.Methods.Genetic;
using Aima.Search.Methods.Genetic.CrossoverOperators;
using Aima.Search.Methods.Genetic.MutationOperators;
using Aima.Search.Methods.Genetic.SelectionOperators;
using GeneticSample.Annotations;
using NCalc;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GeneticSample
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private GeneticAlgorithm<int, WellsState> _ga;
        private WellsProblem _problem;
        private WellsFitnessFunction _fitnessFunction;
        private WellsMetric _metric;
        private Random _random = new Random();

        private List<Annotation> _annotations;
        private Thread _thread;
        private NCalc.Expression _expression;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void Run()
        {
            _expression = new NCalc.Expression(this.Expression.Text, EvaluateOptions.IgnoreCase);
            var width = Convert.ToInt32(this.FieldWidth.Text);
            var height = Convert.ToInt32(this.FieldHeight.Text);
            var number = Convert.ToInt32(this.WellsNumber.Text);
            var maxPopulations = Convert.ToInt32(this.MaxPopulations.Text);
            var mutationChance = Convert.ToDouble(this.MutationChance.Text);
            var individualsInPopulation = Convert.ToInt32(this.IndividualsInPopulation.Text);
            var targetFitness = Convert.ToDouble(this.TargetFitness.Text);

            var model = new PlotModel
            {
                Title = "Field",
                TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinPlotArea,
            };

            // Color axis (the X and Y axes are generated automatically)
            model.Axes.Add(new LinearColorAxis()
            {
                Palette = OxyPalettes.Hot64,
                IsAxisVisible = true,
                Position = AxisPosition.Right,
            });


            // generate 2d normal distribution
            var data = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    data[x, y] = FieldFunction(x, y);
                }
            }

            var heatMapSeries = new HeatMapSeries
            {
                Title = "Field",
                X0 = 0,
                X1 = width - 1,
                Y0 = 0,
                Y1 = height - 1,
                Data = data,
                Interpolate = false,
                //RenderMethod = HeatMapRenderMethod.Rectangles
            };

            model.Series.Add(heatMapSeries);
            //PlotModel = model;



            _problem = new WellsProblem(number, width, height, FieldFunction);

            _fitnessFunction = new WellsFitnessFunction(_problem.WellsNumber * GetMax());
            _metric = new WellsMetric();
            var rnd = new Random();

            _ga = new GeneticAlgorithm<int, WellsState>(
                new WellsGeneticRepresentation(_problem),
                new FitnessProportionateSelection<int, WellsState>(),
                new RandomValueMutationOperator<int>(RandomMutation),
                _fitnessFunction, targetFitness, new DistinctCrossoverOperator<int>())
            {
                PopulationSize = individualsInPopulation,
                MaxPopulations = maxPopulations,
                MutationChance = mutationChance,
            };

            _ga.SearchNodeChanged += GeneticAlgorithmOnSearchNodeChanged;

            _annotations = new List<Annotation>();

            for (var i = 0; i < number; i++)
            {
                var annotation = new EllipseAnnotation()
                {
                    X = _random.Next(width),
                    Y = _random.Next(height),
                    Width = 0.7,
                    Height = 0.7,
                    Fill = OxyColors.White,
                    Stroke = OxyColors.Black,
                    StrokeThickness = 2.0,

                    //Shape = MarkerType.Diamond
                };
                _annotations.Add(annotation);
                model.Annotations.Add(annotation);
            }

            //OnPropertyChanged(nameof(PlotModel));
            Plot1.Model = model;
            Plot1.InvalidatePlot();

            if (_thread != null)
                _thread.Abort();
            _thread = new Thread(() => {
                var solution = _ga.Search(_problem);
            });
            _thread.Start();
        }

        private void GeneticAlgorithmOnSearchNodeChanged(ITreeNode<WellsState> treeNode)
        {
            Thread.Sleep(10);
            Application.Current.Dispatcher.Invoke(() =>
            {
                var annotationIndex = 0;
                foreach (var wellsPosition in treeNode.State.WellsPositions)
                {
                    var x = 0;
                    var y = 0;
                    _problem.FromIndex(wellsPosition, out x, out y);
                    ((EllipseAnnotation) _annotations[annotationIndex]).X = x;
                    ((EllipseAnnotation) _annotations[annotationIndex]).Y = y;
                    annotationIndex++;
                }
                
                Plot1.InvalidatePlot(false);

                CurrentValue.Text = $"{_problem.Value(treeNode.State):F1}";
                CurrentPopulation.Text = $"{_ga.CurrentPopulation}";
                FitnessValue.Text = $"{_ga.FitnessFunction.Compute(_problem, treeNode.State):F3}";
            });
        }

        private double FieldFunction(int x, int y)
        {
            _expression.Parameters["x"] = x;
            _expression.Parameters["y"] = y;
            return (double)_expression.Evaluate();
            //return y * y * (Math.Cos(x) + 1) * (Math.Sin(y) * 2 + 1);
            //return Math.Cos(x/2.0) + Math.Sin(y/2.0) + 2.0;
        }

        private int RandomMutation(int[] genome)
        {
            const int delta = 2;

            while (true)
            {
                //(genome) => _problem.ToIndex(rnd.Next(_problem.Width), rnd.Next(_problem.Height))
                var rndIndex = _random.Next(_problem.WellsNumber);


                var x = 0;
                var y = 0;
                _problem.FromIndex(genome[rndIndex], out x, out y);

                //x += _random.Next(-delta, delta);
                //y += _random.Next(-delta, delta);

                x = _random.Next(_problem.Width);
                y = _random.Next(_problem.Height);


                x = Math.Min(_problem.Width - 1, x);
                x = Math.Max(0, x);
                y = Math.Min(_problem.Height - 1, y);
                y = Math.Max(0, y);

                var index = _problem.ToIndex(x, y);
                if(genome.Contains(index))
                    continue;

                return index;
            }
        }

        private double GetMax()
        {
            var max = 0.0;
            for (var x = 0; x < _problem.Width; x++)
            {
                for (var y = 0; y < _problem.Height; y++)
                {
                    var val = FieldFunction(x, y);
                    if (val > max)
                        max = val;
                }
            }

            return max;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
        }
    }
}

