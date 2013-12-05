﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Routing.CH.Routing;
using OsmSharp.Routing.Graph;
using OsmSharp.Routing.BasicRouter;

namespace OsmSharp.Routing.CH.PreProcessing.Witnesses
{
    /// <summary>
    /// Does witness calculations using the CHRouter implementation.
    /// </summary>
    public class CHRouterWitnessCalculator : INodeWitnessCalculator
    {
        /// <summary>
        /// Holds the data target.
        /// </summary>
        private CHRouter _router;

        /// <summary>
        /// Creates a new witness calculator.
        /// </summary>
        public CHRouterWitnessCalculator()
        {
            _router = new CHRouter();
        }

        /// <summary>
        /// Returns true if the given vertex has a witness calculator.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="via"></param>
        /// <param name="weight"></param>
        /// <param name="max_settles"></param>
        /// <returns></returns>
        public bool Exists(IBasicRouterDataSource<CHEdgeData> graph, uint from, uint to, uint via, float weight, int max_settles)
        {
            return _router.CalculateWeight(graph, from, to, via, weight, max_settles) <= weight;
        }
    }
}