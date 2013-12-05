﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Routing.BasicRouters;
using OsmSharp.Math.Geo;

namespace OsmSharp.Routing.VRP.WithDepot.MaxTime
{
    /// <summary>
    /// A wrapper for all the RouterMaxTime implementations.
    /// </summary>
    public class MaxTimeRouterWrapper : RouterVRPWrapper<RouterMaxTime>
    {
        /// <summary>
        /// Creates a new RouterMaxTime wrapper.
        /// </summary>
        /// <param name="routerVRP"></param>
        /// <param name="router"></param>
        public MaxTimeRouterWrapper(RouterMaxTime routerVRP, Router router)
            : base(routerVRP, router)
        {

        }

        /// <summary>
        /// Calculates the solution to the No-depot DVRP starting from the raw data.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="points"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        public Route[] CalculateNoDepot(Vehicle vehicle, RouterPoint[] points, double[][] weights)
        {
            // build the points array.
            var locations = new GeoCoordinate[points.Length];
            for (int idx = 0; idx < points.Length; idx++)
            {
                locations[idx] = points[idx].Location;
            }

            // calculate the No-depot DVRP solution.
            int[][] solution = this.RouterVRP.CalculateDepot(weights, locations);

            // convert the solution.
            return this.ConvertSolution(vehicle, solution, points);
        }
    }
}
