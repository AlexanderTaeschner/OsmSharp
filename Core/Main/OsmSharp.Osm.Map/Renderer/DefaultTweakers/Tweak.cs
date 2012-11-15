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
using System.Linq;
using System.Text;

namespace OsmSharp.Osm.Renderer.Tweakers
{
    public abstract class Tweak
    {
        /// <summary>
        /// Tweaks the width 
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public abstract float TweakWidth(float zoom_factor, float width);
        /// <summary>
        /// Tweaks the font size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public abstract float TweakFontSize(float zoom_factor, float size);
        /// <summary>
        /// Tweaks the color.
        /// </summary>
        /// <param name="argb"></param>
        /// <returns></returns>
        public abstract int TweakColor(float zoom_factor, int argb);
    }
}