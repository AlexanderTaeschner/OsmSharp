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

using System;
using System.Collections.Generic;

namespace OsmSharp.Routing.Graph
{
    /// <summary>
    /// An implementation of an in-memory dynamic graph.
    /// </summary>
    public class MemoryDynamicGraph<TEdgeData> : IDynamicGraph<TEdgeData>
        where TEdgeData : IDynamicGraphEdgeData
    {
        /// <summary>
        /// Holds the next id.
        /// </summary>
        private uint _nextId;

        /// <summary>
        /// Holds all graph data.
        /// </summary>
        private KeyValuePair<uint, TEdgeData>[][] _vertices;
        
        /// <summary>
        /// Creates a new in-memory graph.
        /// </summary>
        public MemoryDynamicGraph()
        {
            _nextId = 1;
            _vertices = new KeyValuePair<uint, TEdgeData>[1000][];
        }

        /// <summary>
        /// Increases the memory allocation for this dynamic graph.
        /// </summary>
        private void IncreaseSize()
        {
            Array.Resize<KeyValuePair<uint, TEdgeData>[]>(ref _vertices, _vertices.Length + 1000);
        }

        /// <summary>
        /// Returns an enumerable of all vertices.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<uint> GetVertices()
        {
            if (_nextId > 1)
            {
                return Range.UInt32(1, (uint)_nextId - 1, 1U);
            }
            return new List<uint>();
        }

        /// <summary>
        /// Adds and arc to an existing vertex.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="data"></param>
        /// <param name="comparer">Comparator to compare edges and replace obsolete ones.</param>
        public void AddArc(uint from, uint to, TEdgeData data, IDynamicGraphEdgeComparer<TEdgeData> comparer)
        {
            if (_vertices.Length > from)
            {
                KeyValuePair<uint, TEdgeData>[] arcs =
                    _vertices[from];
                int idx = -1;
                if (arcs != null)
                { // check for an existing edge first.
                    if (comparer != null)
                    { // there is no comparer, just skip this check.
                        for (int arcIdx = 0; arcIdx < arcs.Length; arcIdx++)
                        {
                            if (arcs[arcIdx].Key == to &&
                                comparer != null && comparer.Overlaps(data, arcs[arcIdx].Value))
                            { // an arc was found that represents the same directional information.
                                arcs[arcIdx] = new KeyValuePair<uint, TEdgeData>(
                                    to, data);
                                return;
                            }
                        }
                    }
                    
                    // if here: there did not exist an edge yet!
                    idx = arcs.Length;
                    Array.Resize<KeyValuePair<uint, TEdgeData>>(ref arcs, arcs.Length + 1);
                    _vertices[from] = arcs;
                }
                else
                { // create an arcs array.
                    arcs = new KeyValuePair<uint, TEdgeData>[1];
                    idx = 0;
                    _vertices[from] = arcs;
                }

                // set the arc.
                arcs[idx] = new KeyValuePair<uint, TEdgeData>(
                    to, data);

                return;
            }
            throw new ArgumentOutOfRangeException("from");
        }

        /// <summary>
        /// Removes all arcs starting at from ending at to.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void DeleteArc(uint from, uint to)
        {
            if (_vertices.Length > from)
            {
                KeyValuePair<uint, TEdgeData>[] arcs =
                    _vertices[from];
                if (arcs != null && arcs.Length > 0)
                {
                    var arcsList =
                        new List<KeyValuePair<uint, TEdgeData>>(arcs);
                    foreach (KeyValuePair<uint, TEdgeData> arc in arcs)
                    {
                        if (arc.Key == to)
                        {
                            arcsList.Remove(arc);
                        }
                    }
                    _vertices[from] = arcsList.ToArray();
                }
                return;
            }
            throw new ArgumentOutOfRangeException("from");
        }

        /// <summary>
        /// Returns all arcs starting at the given vertex.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public KeyValuePair<uint, TEdgeData>[] GetArcs(uint vertexId)
        {
            if (_vertices.Length > vertexId)
            {
                if (_vertices[vertexId] == null)
                {
                    return new KeyValuePair<uint, TEdgeData>[0];
                }
                return _vertices[vertexId];
            }
            return new KeyValuePair<uint, TEdgeData>[0]; // return empty data if the vertex does not exist!
        }

        /// <summary>
        /// Returns true if the given vertex has neighbour as a neighbour.
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="neighbour"></param>
        /// <returns></returns>
        public bool HasArc(uint vertexId, uint neighbour)
        {
            if (_vertices.Length > vertexId)
            {
                if (_vertices[vertexId] == null)
                {
                    return false;
                }
                foreach(KeyValuePair<uint, TEdgeData> arc in  _vertices[vertexId])
                {
                    if (arc.Key == neighbour)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the number of vertices in this graph.
        /// </summary>
        public uint VertexCount
        {
            get { return _nextId - 1; }
        }
    }
}