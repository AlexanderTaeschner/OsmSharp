﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using OsmSharp.Collections.Tags;
using OsmSharp.Math.Geo;
using OsmSharp.Osm.Streams.Filters;
using OsmSharp.Osm.Xml.Streams;
using OsmSharp.Routing;
using OsmSharp.Routing.Graph;
using OsmSharp.Routing.Instructions;
using OsmSharp.Routing.Osm.Graphs;
using OsmSharp.Routing.Osm.Interpreter;
using OsmSharp.Routing.Osm.Streams.Graphs;
using OsmSharp.Routing.BasicRouter;
using OsmSharp.Routing.BasicRouter.Dykstra;

namespace OsmSharp.Test.Unittests.Routing.Instructions
{
    /// <summary>
    /// Holds some instruction generation regression tests.
    /// </summary>
    public abstract class InstructionRegressionTestsBase
    {
        /// <summary>
        /// Creates a router based on the resource.
        /// </summary>
        /// <param name="interpreter"></param>
        /// <param name="manifestResourceName"></param>
        /// <returns></returns>
        protected Router CreateReferenceRouter(IOsmRoutingInterpreter interpreter, string manifestResourceName)
        {
            TagsTableCollectionIndex tagsIndex = new TagsTableCollectionIndex();

            // do the data processing.
            DynamicGraphRouterDataSource<LiveEdge> memoryData =
                new DynamicGraphRouterDataSource<LiveEdge>(tagsIndex);
            LiveGraphOsmStreamTarget target_data = new LiveGraphOsmStreamTarget(
                memoryData, interpreter, tagsIndex);
            XmlOsmStreamSource dataProcessorSource = new XmlOsmStreamSource(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResourceName));
            OsmStreamFilterSort sorter = new OsmStreamFilterSort();
            sorter.RegisterSource(dataProcessorSource);
            target_data.RegisterSource(sorter);
            target_data.Pull();

            IBasicRouter<LiveEdge> basicRouter = new DykstraRoutingLive();
            return Router.CreateLiveFrom(memoryData, basicRouter, interpreter);
        }

        /// <summary>
        /// Creates a router based on the resource.
        /// </summary>
        /// <param name="interpreter"></param>
        /// <param name="manifestResourceName"></param>
        /// <returns></returns>
        protected abstract Router CreateRouter(IOsmRoutingInterpreter interpreter, string manifestResourceName);

        /// <summary>
        /// Compares the two given routes.
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="route"></param>
        protected void CompareRoutes(Route reference, Route route)
        {
            double delta = 0.0001;

            if (reference.Entries == null)
            {
                Assert.IsNull(route.Entries);
            }
            else
            {
                Assert.AreEqual(reference.Entries.Length, route.Entries.Length);
                for (int idx = 0; idx < reference.Entries.Length; idx++)
                {
                    Assert.AreEqual(reference.Entries[idx].Distance,
                        route.Entries[idx].Distance);
                    Assert.AreEqual(reference.Entries[idx].Latitude,
                        route.Entries[idx].Latitude);
                    Assert.AreEqual(reference.Entries[idx].Longitude,
                        route.Entries[idx].Longitude);
                    Assert.AreEqual(reference.Entries[idx].Time,
                        route.Entries[idx].Time);
                    Assert.AreEqual(reference.Entries[idx].Type,
                        route.Entries[idx].Type);
                    Assert.AreEqual(reference.Entries[idx].WayFromName,
                        route.Entries[idx].WayFromName);

                    // something that is allowed to be different in this case!
                    // route3540.Entries[idx].Points != null

                    // check sidestreets.
                    if (reference.Entries[idx].SideStreets != null &&
                        reference.Entries[idx].SideStreets.Length > 0)
                    { // check if the sidestreets represent the same information.
                        Assert.AreEqual(reference.Entries[idx].SideStreets.Length,
                            route.Entries[idx].SideStreets.Length);
                        for (int metricIdx = 0; metricIdx < reference.Entries[idx].SideStreets.Length; metricIdx++)
                        {
                            Assert.AreEqual(reference.Entries[idx].SideStreets[metricIdx].WayName,
                                route.Entries[idx].SideStreets[metricIdx].WayName);
                            Assert.AreEqual(reference.Entries[idx].SideStreets[metricIdx].Latitude,
                                route.Entries[idx].SideStreets[metricIdx].Latitude);
                            Assert.AreEqual(reference.Entries[idx].SideStreets[metricIdx].Longitude,
                                route.Entries[idx].SideStreets[metricIdx].Longitude);
                        }
                    }
                    else
                    { // check if the sidestreets represent the same information.
                        Assert.IsTrue(route.Entries[idx].SideStreets == null ||
                            route.Entries[idx].SideStreets.Length == 0);
                    }

                    if (reference.Entries[idx].Tags != null &&
                        reference.Entries[idx].Tags.Length > 0)
                    { // check if the Tags represent the same information.
                        Assert.AreEqual(reference.Entries[idx].Tags.Length,
                            route.Entries[idx].Tags.Length);
                        for (int metricIdx = 0; metricIdx < route.Entries[idx].Tags.Length; metricIdx++)
                        {
                            Assert.AreEqual(reference.Entries[idx].Tags[metricIdx].Key,
                                route.Entries[idx].Tags[metricIdx].Key);
                            Assert.AreEqual(reference.Entries[idx].Tags[metricIdx].Value,
                                route.Entries[idx].Tags[metricIdx].Value);
                        }
                    }
                    else
                    { // check if the sidestreets represent the same information.
                        Assert.IsTrue(route.Entries[idx].Tags == null ||
                            route.Entries[idx].Tags.Length == 0);
                    }

                    Assert.AreEqual(reference.Entries[idx].Distance, route.Entries[idx].Distance);
                }
                if (reference.Tags != null &&
                    reference.Tags.Length > 0)
                {
                    for (int tagIdx = 0; tagIdx < reference.Tags.Length; tagIdx++)
                    {
                        if (reference.Tags[tagIdx].Key != "debug_route")
                        {
                            Assert.AreEqual(reference.Tags[tagIdx].Key,
                                route.Tags[tagIdx].Key);
                            Assert.AreEqual(reference.Tags[tagIdx].Value,
                                route.Tags[tagIdx].Value);
                        }
                    }
                }
                else
                {
                    Assert.IsTrue(route.Tags == null ||
                        route.Tags.Length == 0);
                }
                if (reference.Metrics != null)
                {
                    for (int metricIdx = 0; metricIdx < reference.Entries.Length; metricIdx++)
                    {
                        Assert.AreEqual(reference.Metrics[metricIdx].Key,
                            route.Metrics[metricIdx].Key);
                        Assert.AreEqual(reference.Metrics[metricIdx].Value,
                            route.Metrics[metricIdx].Value);
                    }
                }
                else
                {
                    Assert.IsNull(route.Metrics);
                }
            }

            Assert.AreEqual(reference.TotalDistance, route.TotalDistance, delta);
            Assert.AreEqual(reference.TotalTime, route.TotalTime, delta);
            //Assert.AreEqual(reference.Vehicle, route.Vehicle, delta);
        }

        /// <summary>
        /// Issue with generation instructions but where streetnames seem to be stripped.
        /// Some streetnames are missing from the instructions.
        /// </summary>
        protected void DoInstructionRegressionTest1()
        {
            OsmRoutingInterpreter interpreter = new OsmRoutingInterpreter();

            Router router = this.CreateRouter(interpreter, "OsmSharp.Test.Unittests.test_routing_regression1.osm");

            // resolve the three points in question.
            GeoCoordinate point35 = new GeoCoordinate(51.01257, 4.000753);
            RouterPoint point35resolved = router.Resolve(Vehicle.Car, point35, true);
            GeoCoordinate point45 = new GeoCoordinate(51.01315, 3.999588);
            RouterPoint point45resolved = router.Resolve(Vehicle.Car, point45, true);
            GeoCoordinate point40 = new GeoCoordinate(51.01250, 4.000013);
            RouterPoint point40resolved = router.Resolve(Vehicle.Car, point40, true);

            // calculate two smaller routes.
            Route route3545 = router.Calculate(Vehicle.Car, point35resolved, point45resolved);
            Route route4540 = router.Calculate(Vehicle.Car, point45resolved, point40resolved);
            Route route3540concatenated = Route.Concatenate(route3545, route4540, true);

            Route route3540 = router.Calculate(Vehicle.Car, point35resolved, point40resolved);

            // check if both routes are equal.
            Assert.AreEqual(route3540.Entries.Length, route3540concatenated.Entries.Length);
            for (int idx = 0; idx < route3540.Entries.Length; idx++)
            {
                Assert.AreEqual(route3540.Entries[idx].Distance, route3540concatenated.Entries[idx].Distance);
                Assert.AreEqual(route3540.Entries[idx].Latitude, route3540concatenated.Entries[idx].Latitude);
                Assert.AreEqual(route3540.Entries[idx].Longitude, route3540concatenated.Entries[idx].Longitude);
                Assert.AreEqual(route3540.Entries[idx].Time, route3540concatenated.Entries[idx].Time);
                Assert.AreEqual(route3540.Entries[idx].Type, route3540concatenated.Entries[idx].Type);
                Assert.AreEqual(route3540.Entries[idx].WayFromName, route3540concatenated.Entries[idx].WayFromName);

                // something that is allowed to be different in this case!
                // route3540.Entries[idx].Points != null

            //    // check sidestreets.
            //    if (route3540.Entries[idx].SideStreets != null &&
            //        route3540.Entries[idx].SideStreets.Length > 0)
            //    { // check if the sidestreets represent the same information.
            //        for (int metricIdx = 0; metricIdx < route3540concatenated.Entries[idx].SideStreets.Length; metricIdx++)
            //        {
            //            Assert.AreEqual(route3540.Entries[idx].SideStreets[metricIdx].WayName,
            //                route3540concatenated.Entries[idx].SideStreets[metricIdx].WayName);
            //            Assert.AreEqual(route3540.Entries[idx].SideStreets[metricIdx].Latitude,
            //                route3540concatenated.Entries[idx].SideStreets[metricIdx].Latitude);
            //            Assert.AreEqual(route3540.Entries[idx].SideStreets[metricIdx].Longitude,
            //                route3540concatenated.Entries[idx].SideStreets[metricIdx].Longitude);
            //        }
            //    }
            //    else
            //    {
            //        Assert.IsTrue(route3540concatenated.Entries[idx].SideStreets == null ||
            //            route3540concatenated.Entries[idx].SideStreets.Length == 0);
            //    }


            //    if (route3540.Entries[idx].Tags != null &&
            //        route3540.Entries[idx].Tags.Length > 0)
            //    { // check if the Tags represent the same information.
            //        for (int metricIdx = 0; metricIdx < route3540concatenated.Entries[idx].Tags.Length; metricIdx++)
            //        {
            //            Assert.AreEqual(route3540.Entries[idx].Tags[metricIdx].Key,
            //                route3540concatenated.Entries[idx].Tags[metricIdx].Key);
            //            Assert.AreEqual(route3540.Entries[idx].Tags[metricIdx].Value,
            //                route3540concatenated.Entries[idx].Tags[metricIdx].Value);
            //        }
            //    }
            //    else
            //    {
            //        Assert.IsTrue(route3540concatenated.Entries[idx].Tags == null ||
            //            route3540concatenated.Entries[idx].Tags.Length == 0);
            //    }

            //    Assert.AreEqual(route3540.Entries[idx].Distance, route3540concatenated.Entries[idx].Distance);
            }
            if (route3540.Tags != null &&
                route3540.Tags.Length > 0)
            {
                for (int tagIdx = 0; tagIdx < route3540.Tags.Length; tagIdx++)
                {
                    if (route3540.Tags[tagIdx].Key != "debug_route")
                    {
                        Assert.AreEqual(route3540.Tags[tagIdx].Key, route3540concatenated.Tags[tagIdx].Key);
                        Assert.AreEqual(route3540.Tags[tagIdx].Value, route3540concatenated.Tags[tagIdx].Value);
                    }
                }
            }
            else
            {
                Assert.IsTrue(route3540concatenated.Tags == null ||
                    route3540concatenated.Tags.Length == 0);
            }
            if (route3540.Metrics != null)
            {
                for (int metricIdx = 0; metricIdx < route3540concatenated.Entries.Length; metricIdx++)
                {
                    Assert.AreEqual(route3540.Metrics[metricIdx].Key, route3540concatenated.Metrics[metricIdx].Key);
                    Assert.AreEqual(route3540.Metrics[metricIdx].Value, route3540concatenated.Metrics[metricIdx].Value);
                }
            }
            else
            {
                Assert.IsNull(route3540concatenated.Metrics);
            }

            // remove the point in between, the only difference between the regular and the concatenated route.
            route3540concatenated.Entries[7].Points = null;

            // create the language generator.
            var languageGenerator = new LanguageTestGenerator();

            // generate the instructions.
            List<Instruction> instructions =
                InstructionGenerator.Generate(route3540, interpreter, languageGenerator);
            List<Instruction> instructionsConcatenated =
                InstructionGenerator.Generate(route3540concatenated, interpreter, languageGenerator);

            Assert.AreEqual(instructions.Count, instructionsConcatenated.Count);
            for (int idx = 0; idx < instructions.Count; idx++)
            {
                Assert.AreEqual(instructions[idx].Location.Center,
                    instructionsConcatenated[idx].Location.Center);
                Assert.AreEqual(instructions[idx].Text,
                    instructionsConcatenated[idx].Text);
            }
        }

        /// <summary>
        /// Calculates routes, generates instructions and compares instructions.
        /// </summary>
        /// <param name="embeddedXml"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        protected void DoInstructionComparisonTest(string embeddedXml, GeoCoordinate point1, GeoCoordinate point2)
        {
            OsmRoutingInterpreter interpreter = new OsmRoutingInterpreter();

            Router router = this.CreateRouter(interpreter,
                embeddedXml);
            Router referenceRouter = this.CreateReferenceRouter(interpreter,
                embeddedXml);

            // resolve the three points in question.
            RouterPoint point1resolved = router.Resolve(Vehicle.Car, point1, true);
            RouterPoint point2resolved = router.Resolve(Vehicle.Car, point2, true);

            // calculate two smaller routes.
            Route route12 = router.Calculate(Vehicle.Car,
                point1resolved, point2resolved);

            // resolve the three points in question.
            RouterPoint pointReference1resolved = referenceRouter.Resolve(Vehicle.Car, point1, true);
            RouterPoint pointReference2resolved = referenceRouter.Resolve(Vehicle.Car, point2, true);

            // calculate two smaller routes.
            Route routeReference12 = referenceRouter.Calculate(Vehicle.Car,
                pointReference1resolved, pointReference2resolved);

            // compares the two routes.
            this.CompareRoutes(routeReference12, route12);

            // create the language generator.
            var languageGenerator = new LanguageTestGenerator();

            // generate the instructions.
            List<Instruction> instructions =
                InstructionGenerator.Generate(route12, interpreter, languageGenerator);
            List<Instruction> instructionsReference =
                InstructionGenerator.Generate(routeReference12, interpreter, languageGenerator);

            Assert.AreEqual(instructions.Count, instructionsReference.Count);
            for (int idx = 0; idx < instructions.Count; idx++)
            {
                Assert.AreEqual(instructions[idx].Location.Center,
                    instructionsReference[idx].Location.Center);
                Assert.AreEqual(instructions[idx].Text,
                    instructionsReference[idx].Text);
            }
        }
    }
}