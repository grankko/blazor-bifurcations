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
        /// Finds stable values for a given <paramref name="fertility"/> in x(n+1) = r*x(n)*(1-x(n)).
        /// </summary>
        public CalculationStepResults Calculate(double fertility)
        {
            var results = new CalculationStepResults(fertility);
            Stack<double> calculatedGenerationPopulations = new Stack<double>();
            calculatedGenerationPopulations.Push(_initialPopulation);

            var boundriesFound = false;
            var noOfCalculatedGenerations = 0;

            while (!boundriesFound)
            {
                var previousGenerationPopulation = calculatedGenerationPopulations.Peek();

                // Get next value in the set based on previous generation
                var nextGenerationPopulation = previousGenerationPopulation * fertility * (1 - previousGenerationPopulation);
                nextGenerationPopulation = Math.Round(nextGenerationPopulation, _acceptansDepth);

                // If we find a value that is present in a previous generation, we've found a repeating pattern
                if (calculatedGenerationPopulations.Contains(nextGenerationPopulation))
                {
                    boundriesFound = true;
                    results.StableValues.AddRange(ExtractStableValuesFromSetWithRepeatingPattern(calculatedGenerationPopulations, nextGenerationPopulation));
                }

                calculatedGenerationPopulations.Push(nextGenerationPopulation);

                noOfCalculatedGenerations++;
                if (noOfCalculatedGenerations >= _calculationDepth)
                    boundriesFound = true; // Give up, this is not going to repeat for the given fertility..
            }

            return results;
        }

        /// <summary>
        /// Extracts repeating pattern of values in <paramref name="calculatedGenerations"/>
        /// </summary>
        /// <param name="calculatedGenerations">Set of values with a repeating pattern.</param>
        /// <returns></returns>
        public static double[] ExtractStableValuesFromSetWithRepeatingPattern(Stack<double> calculatedGenerations, double firstRepeatingValue)
        {
            var stableValues = new List<double>();
            var patternFound = false;
            while (!patternFound)
            {
                stableValues.Add(calculatedGenerations.Pop());
                if (stableValues.Contains(firstRepeatingValue))
                    patternFound = true;
            }

            return stableValues.ToArray();
        }
    }
}
