using System;
using Maze.Generation.Drawing.DrawersData;
using Shared.Sides;
using Zenject;

namespace Maze.Generation.Drawing.Drawers
{
    public class GateDrawer: DrawerBase<GatesDrawerData>
    {
        [Inject]
        public GateDrawer(GatesDrawerData data, TilemapsForDrawing tilemapsForDrawing) : base(data, tilemapsForDrawing)
        {
        }

        public override void Draw(Cell cell)
        {
            foreach (var gate in cell.Gates)
            {
                var side = GetSideOfGate(cell, gate);
                DrawGateBorder(side, gate);
            }
        }

        private void DrawGateBorder(Side side, Gate gate)
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
            foreach (var tileInfo in Data.GateTiles.GetTiles(rotation))
            {
                TilemapsForDrawing.SetTile(tileInfo.Tile, tileInfo.Position + center, tileInfo.Layer);
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