using System.Collections.Generic;
using Maze.Generation.Separation;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation
{
    public class Cell
    {
        public Vector2Int TopRight { get; private set; }
        public Vector2Int BottomLeft { get; private set; }
        public List<Gate> Gates { get; private set; }
        public List<Cell> SubCells { get; private set; }
        public Vector2Int Size => TopRight - BottomLeft;

        public string SeparatingTag
        {
            get => _separatingTag;
            set
            {
                if (string.IsNullOrEmpty(_separatingTag))
                    _separatingTag = value;
                else
                    _separatingTag += " " + value;
            }
        }

        public bool Separable { get; set; }
        public bool IsFinish { get; set; }
        private string _separatingTag;
        
        public Cell(Vector2Int bottomLeft, Vector2Int topRight, Cell parent, string tag)
        {
            SubCells = new List<Cell>();
            TopRight = topRight;
            BottomLeft = bottomLeft;
            Gates = new List<Gate>();
            Separable = true;
            IsFinish = true;
            if(parent != null)
            {
                parent.SubCells.Add(this);
                SeparatingTag = parent.SeparatingTag;
                parent.AddGatesToChildren(new []{this});
                parent.IsFinish = false;
                parent.Separable = false;
            }

            SeparatingTag = tag;
        }

        public static Cell FromCoords(int bottom, int top, int left, int right, Cell parent, string tag)
        {
            return new Cell(new Vector2Int(right, top), new Vector2Int(left, bottom), parent, tag);
        }
    }
}