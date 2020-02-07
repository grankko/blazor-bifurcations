using System.Collections.Generic;

namespace BlazorBifurcations.Calculations
{
    public class CalculationStepResults
    {
        public double Fertility { get; private set; }
        public List<double> StableValues { get; private set; }

        public int Period { get { return StableValues.Count; } }

        public CalculationStepResults(double fertility)
        {
            Fertility = fertility;
            StableValues = new List<double>();
        }

        public override string ToString()
        {
            return $"Fertility {Fertility} - Period {Period}";
        }
    }
}