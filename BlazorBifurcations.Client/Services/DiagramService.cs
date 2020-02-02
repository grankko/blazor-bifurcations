using BlazorBifurcations.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace BlazorBifurcations.Client.Services
{
    public class DiagramService
    {
        private readonly double _increment = 0.002;
        private readonly double _endFertility = 4;
        private readonly double _initialPopulation = 0.5;
        private readonly double _canvasCellSize = 0.1;
        private readonly int _calculationDepth = 500;
        private readonly int _acceptansDepth = 4;
        private readonly int _zoomLevel = 400;
        private readonly int _yOffset = 600;
        private readonly int _repaintInterval = 50;

        private Bifurcator _bifurcator;
        private Timer _timer;        
        private readonly JavaScriptService _javaScriptService;

        public List<CalculationStepResults> Results { get; private set; }
        public double CurrentFertility { get; private set; }
        public DiagramService(JavaScriptService javaScriptService)
        {
            CurrentFertility = 0;
            Results = new List<CalculationStepResults>();

            _bifurcator = new Bifurcator(_initialPopulation, _calculationDepth, _acceptansDepth);
            _timer = new Timer(1);
            _timer.Elapsed += OnTimerElapsed;
            _javaScriptService = javaScriptService;
        }

        public event EventHandler StateChanged;

        public void Start()
        {
            _timer.Start();
        }

        protected void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {

            if (CurrentFertility < _endFertility)
            {
                for (int i = 0; i < _repaintInterval; i++)
                {
                    var result = _bifurcator.Calculate(CurrentFertility);
                    CurrentFertility += _increment;
                    CurrentFertility = Math.Round(CurrentFertility, _acceptansDepth);
                    Results.Add(result);
                }

                DrawResults();
                StateChanged?.Invoke(this, EventArgs.Empty);

            }
            else
            {
                _timer.Stop();
            }
        }

        private void DrawResults()
        {
            foreach (var result in Results)
            {
                double x = (result.Fertility * _zoomLevel);

                foreach (var stableValue in result.StableValues)
                {
                    double y = _yOffset - (stableValue * _zoomLevel);
                    _javaScriptService.DrawCellOnCanvas("diagramCanvas", x, y, _canvasCellSize);
                }
            }
        }
    }
}
