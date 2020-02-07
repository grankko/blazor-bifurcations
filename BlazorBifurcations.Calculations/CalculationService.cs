using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBifurcations.Calculations
{
    /// <summary>
    /// Calculates logistic map and keeps state across generational steps.
    /// </summary>
    public class CalculationService
    {
        private const double Increment = 0.002;
        private const double EndFertility = 4;
        private const double InitialPopulation = 0.5;
        private const int CalculationDepth = 500;
        private const int AcceptansDepth = 4;

        public double? FigenbaumsConstant { get; private set; }
        public double CurrentFertility { get; private set; }
        public List<CalculationStepResults> Results { get; private set; }

        private Bifurcator _bifurcator;
        public CalculationService()
        {
            Reset();
        }

        public void Reset()
        {
            CurrentFertility = 0;
            Results = new List<CalculationStepResults>();
            _bifurcator = new Bifurcator(InitialPopulation, CalculationDepth, AcceptansDepth);
        }

        public bool CalculateStep()
        {
            if (CurrentFertility <= EndFertility)
            {
                var stepResults = _bifurcator.Calculate(CurrentFertility);
                Results.Add(stepResults);
                CurrentFertility += Increment;
                return false;
            }

            return true;
        }
    }
}
