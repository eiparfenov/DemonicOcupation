using System;
using System.Collections.Generic;
using Shared.Sides;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/GatesDrawer")]
    public class GatesDrawer: DrawerBaseData
    {
        [SerializeField] private GateDataProvider gateTiles;

        public override void Draw(Cell cell, Dictionary<int, Tilemap> tilemaps)
        {
            foreach (var gate in cell.Gates)
            {
                var side = GetSideOfGate(cell, gate);
                DrawGateBorder(side, gate, tilemaps);
            }
        }

        private void DrawGateBorder(Side side, Gate gate, Dictionary<int, Tilemap> tilemaps)
        {
            var rotation = side switch
            {
                Side.Left => 0,
                Side.Top => 1,
                Side.Right => 2,
                Side.Bottom => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
            var center = gate.Center;
            foreach (var tileInfo in gateTiles.GetTiles(rotation))
            {
                SetTile(tilemaps, tileInfo.Layer, tileInfo.Position + center.ToVector3Int(), tileInfo.Tile);
            }
        }

        private Side GetSideOfGate(Cell cell, Gate gate)
        {
            return gate.Direction == Direction.Horizontal
                ? gate.TopRight.x == cell.TopRight.x
                    ? Side.Right
                    : Side.Left
                : gate.TopRight.y == cell.TopRight.y
                    ? Side.Top
                    : Side.Bottom;
        }
    }
}