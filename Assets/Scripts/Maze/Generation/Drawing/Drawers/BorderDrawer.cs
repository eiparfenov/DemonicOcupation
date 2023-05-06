using Maze.Generation.Drawing.DrawersData;
using UnityEngine;
using Zenject;

namespace Maze.Generation.Drawing.Drawers
{
    public class BorderDrawer: DrawerBase<BorderDrawerData>
    {
        [Inject]
        public BorderDrawer(BorderDrawerData data, TilemapsForDrawing tilemapsForDrawing) : base(data, tilemapsForDrawing)
        {
        }

        public override void Draw(Cell cell)
        {
            for (int x = 0; x < cell.Size.x; x++)
            for (int y = 0; y < Data.BorderSize; y++)
            {
                var tileCoords = cell.BottomLeft + new Vector2Int(x, y);
                TilemapsForDrawing.SetTile(Data.BorderTile, tileCoords, Data.Layer);
                tileCoords = cell.BottomLeft + new Vector2Int(x, cell.Size.y - y - 1);
                TilemapsForDrawing.SetTile(Data.BorderTile, tileCoords, Data.Layer);
            }
            for (int y = 0; y < cell.Size.y; y++)
            for (int x = 0; x < Data.BorderSize; x++)
            {
                var tileCoords = cell.BottomLeft + new Vector2Int(x, y);
                TilemapsForDrawing.SetTile(Data.BorderTile, tileCoords, Data.Layer);
                tileCoords = cell.BottomLeft + new Vector2Int(cell.Size.x - x - 1, y);
                TilemapsForDrawing.SetTile(Data.BorderTile, tileCoords, Data.Layer);
            }
        }
    }
}