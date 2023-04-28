﻿using System.Collections.Generic;
using UnityEngine;

namespace Maze.Generation.Separation
{
    public abstract class SeparatorBase: ScriptableObject
    {
        public abstract List<Cell> Separate(Cell parentCell);
    }
}