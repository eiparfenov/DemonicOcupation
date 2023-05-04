using System.Collections.Generic;
using Shared.Sides;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/GatesDrawer")]
    public class GatesDrawer: DrawerBase
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private TileBase borderTile;
        [SerializeField] private int borderLayer;
        
        public override void Draw(Cell cell, Dictionary<int, Tilemap> tilemaps)
        {
            foreach (var gate in cell.Gates)
            {
                var side = GetSideOfGate(cell, gate);
                DrawGateBorder(side, gate, GetTilemap(tilemaps, borderLayer));
            }
        }

        private void DrawGateBorder(Side side, Gate gate, Tilemap tilemap)
        {
            var gateCenter = gate.Direction.MagnitudeCross(gate.TopRight) - gate.Length / 2;

            var topRight = gate.Direction.VectorCross() * (gateCenter + width / 2 - 1)
                + gate.Direction.Project(gate.TopRight)
                + gate.Direction.Vector() * (side.Correction() - 1);
            var bottomLeft = gate.Direction.VectorCross() * (gateCenter - width / 2)
                + gate.Direction.Project(gate.TopRight)
                + gate.Direction.Vector() * (side.Correction() - 1);
            for (int border = 0; border < height; border++)
            {
                tilemap.SetTile((topRight - side.Direction() * border).ToVector3Int(), borderTile);
                tilemap.SetTile((bottomLeft - side.Direction() * border).ToVector3Int(), borderTile);
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