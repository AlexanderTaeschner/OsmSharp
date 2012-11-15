﻿// OsmSharp - OpenStreetMap tools & library.
// Copyright (C) 2012 Abelshausen Ben
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OsmSharp.Osm.UI.WinForms.Layers;

namespace OsmSharp.Osm.UI.WinForms.Layer
{
    public partial class LayerUserControl : UserControl
    {
        public LayerUserControl()
        {
            InitializeComponent();
        }

        private OsmSharp.Osm.Map.Map _map;

        public void SetMap(Osm.Map.Map map)
        {
            _map = map;

            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowCount = _map.Layers.Count;

            for (int layer_idx = 0; layer_idx < _map.Layers.Count; layer_idx++)
            {
                LayerDetailUserControl ctrl = new LayerDetailUserControl();
                ctrl.SetLayer(_map.Layers[layer_idx]);

                this.tableLayoutPanel1.Controls.Add(ctrl);
            }
        }
    }
}
