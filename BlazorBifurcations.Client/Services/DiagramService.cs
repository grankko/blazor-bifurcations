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
        private readonly double _canvasCellSize = 0.2;
        private readonly int _zoomLevel = 400;
        private readonly int _yOffset = 450;
        private readonly int _repaintInterval = 50; // Number of calculations to perform before updating diagram

        private CalculationService _calculationService;
        private readonly JavaScriptService _javaScriptService;
        private Timer _timer;

        public string CurrentFertility { get { return _calculationService.CurrentFertility.ToString("0.00"); } }
        public List<CalculationStepResults> Results { get { return _calculationService.Results; } }

        public DiagramService(JavaScriptService javaScriptService, CalculationService calculationService)
        {
            _javaScriptService = javaScriptService;
            _calculationService = calculationService;

            _timer = new Timer(1);
            _timer.Elapsed += OnTimerElapsed;

        }

        public event EventHandler StateChanged;

        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Calculates and draws diagram in steps
        /// </summary>
        protected void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {

            for (int i = 0; i < _repaintInterval; i++)
            {
                bool isDone = _calculationService.CalculateStep();

                if (isDone)
                {
                    _timer.Stop();
                    break;
                }
            }

            DrawResults();
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DrawResults()
        {
            int generation = 0;
            Queue<int> lastPeriods = new Queue<int>();

            foreach (var result in Results)
            {
                string periodColorRepresentation = "#FFFFFF";

                // Calculate average period over past 10 results and use for color coding.
                // Naive attempt at correcting precision errors by using average.
                lastPeriods.Enqueue(result.Period);
                if (generation > 10)
                    lastPeriods.Dequeue();

                var averageTrailingPeriod = (int)lastPeriods.Average();

                // Color code diagram by period
                if (averageTrailingPeriod >= 2 && averageTrailingPeriod < 4)
                    periodColorRepresentation = "#FF6666";
                else if (averageTrailingPeriod >= 4 && averageTrailingPeriod < 8)
                    periodColorRepresentation = "#66FF66";
                else if (averageTrailingPeriod >= 8 && averageTrailingPeriod < 16)
                    periodColorRepresentation = "#6666FF";
                else if (averageTrailingPeriod >= 16)
                    periodColorRepresentation = "#FFFF66";

                // Draw diagram on canvas
                double x = ((result.Fertility - CalculationService.StartFertility) * _zoomLevel);           
                foreach (var stableValue in result.StableValues)
                {
                    double y = _yOffset - (stableValue * _zoomLevel);
                    _javaScriptService.DrawCellOnCanvas("diagramCanvas", x, y, _canvasCellSize, periodColorRepresentation);
                }

                generation++;
            }
        }
    }
}
