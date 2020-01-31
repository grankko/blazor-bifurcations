using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BlazorBifurcations.Calculations
{
    public class Bifurcator
    {
        private readonly double _initialPopulation;
        private readonly int _calculationDepth;
        private readonly int _acceptansDepth;

        public Bifurcator(double initialPopulation, int calculationDepth, int acceptansDepth)
        {
            _initialPopulation = initialPopulation;
            _calculationDepth = calculationDepth;
            _acceptansDepth = acceptansDepth;
        }

        public Dictionary<double, double[]> CalculateOverFertility(double startFertility, double endFertility, double increment)
        {
            var result = new Dictionary<double, double[]>();
            var currentFertility = startFertility;

            while (currentFertility <= endFertility)
            {
                currentFertility = Math.Round(currentFertility, _acceptansDepth);
                result.Add(currentFertility, Calculate(currentFertility));
                currentFertility += increment;
            }

            return result;
        }

        public double[] Calculate(double fertility)
        {
            var results = new List<double>();

            Dictionary<int, double> calculatedGenerations = new Dictionary<int, double>();
            calculatedGenerations.Add(0, _initialPopulation);

            var boundriesFound = false;
            var generationsCalculated = 0;
            while (!boundriesFound)
            {
                var previousGeneration = calculatedGenerations[generationsCalculated];
                var nextGeneration = previousGeneration * fertility * (1 - previousGeneration);

                nextGeneration = Math.Round(nextGeneration, _acceptansDepth);

                if (calculatedGenerations.Values.Contains(nextGeneration))
                {
                    boundriesFound = true;
                    var lastGeneration = generationsCalculated;

                    double traversedValue = calculatedGenerations[lastGeneration];
                    
                    if (traversedValue == nextGeneration)
                        results.Add(traversedValue);

                    while (traversedValue != nextGeneration)
                    {
                        results.Add(calculatedGenerations[lastGeneration]);
                        lastGeneration--;
                        traversedValue = calculatedGenerations[lastGeneration];
                        if (traversedValue == nextGeneration)
                            results.Add(traversedValue);
                    }

                }

                calculatedGenerations.Add(generationsCalculated + 1, nextGeneration);
                generationsCalculated++;

                if (generationsCalculated >= _calculationDepth)
                    boundriesFound = true;
            }

            return results.ToArray();

        }        
    }
}
