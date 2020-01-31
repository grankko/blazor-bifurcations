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

        /// <summary>
        /// It bifurcates..
        /// </summary>
        /// <param name="initialPopulation">Population that calculation starts with.</param>
        /// <param name="calculationDepth">Number of iterations calculated per generation before accepting no stable values are found.</param>
        /// <param name="acceptansDepth">Precision of values in calculation before they are considered equal.</param>
        public Bifurcator(double initialPopulation, int calculationDepth, int acceptansDepth)
        {
            _initialPopulation = initialPopulation;
            _calculationDepth = calculationDepth;
            _acceptansDepth = acceptansDepth;
        }

        /// <summary>
        /// Calculates stable values of x(n+1) = r*x(n)*(1-x(n)) for all conditions between <paramref name="startFertility"/> and <paramref name="endFertility"/>
        /// when fertility is incremented by <paramref name="increment"/>.
        /// </summary>
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

        /// <summary>
        /// Finds stable values for a given <paramref name="fertility"/>.
        /// </summary>
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
                
                // Get next value in the set based on previous generation
                var nextGeneration = previousGeneration * fertility * (1 - previousGeneration);
                nextGeneration = Math.Round(nextGeneration, _acceptansDepth);

                // If we find a value that is present in a previous generation, we've found a repeating pattern
                if (calculatedGenerations.Values.Contains(nextGeneration))
                {
                    boundriesFound = true;

                    // Collect all values from the end of the results until we hit the repeating pattern
                    var lastGeneration = generationsCalculated;
                    double traversedValue = calculatedGenerations[lastGeneration];
                    
                    if (traversedValue == nextGeneration)
                        results.Add(traversedValue); // The first value that repeats, should be part of the result

                    while (traversedValue != nextGeneration)
                    {
                        // Step backwards in calculated values, add to result until we hit the first repeating value in the set
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
                    boundriesFound = true; // Give up, this is not going to repeat for the given fertility..
            }

            return results.ToArray();

        }        
    }
}
