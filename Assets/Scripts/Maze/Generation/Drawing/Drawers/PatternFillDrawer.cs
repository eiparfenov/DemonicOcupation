using Maze.Generation.Drawing.DrawersData;
using UnityEngine;
using Zenject;

namespace Maze.Generation.Drawing.Drawers
{
    public class PatternFillDrawer: DrawerBase<PatternFillDrawerData>
    {
        [Inject]
        public PatternFillDrawer(PatternFillDrawerData data, TilemapsForDrawing tilemapsForDrawing) : base(data, tilemapsForDrawing)
        {
        }

        public override void Draw(Cell cell)
        {
            var drawingStartPoint = - new Vector2Int(
                Mathf.CeilToInt(cell.Size.x / 2f / Data.Pattern.Size.x),
                Mathf.CeilToInt(cell.Size.y / 2f / Data.Pattern.Size.y)
            ) * Data.Pattern.Size;
            
            var drawingsCount = new Vector2Int(
                Mathf.CeilToInt((float) cell.Size.x / Data.Pattern.Size.x) + 1,
                Mathf.CeilToInt((float) cell.Size.y / Data.Pattern.Size.y) + 1
            );

            for (int x = 0; x < drawingsCount.x; x ++)
            {
                for (int y = 0; y < drawingsCount.y; y ++)
                {
                    foreach (var tileInfo in Data.Pattern.GetTiles())
                    {
                        var poseX = cell.Center.x + x * Data.Pattern.Size.x + drawingStartPoint.x + tileInfo.Position.x;
                        if(poseX < cell.BottomLeft.x || cell.TopRight.x <= poseX) continue;
                        
                        var poseY = cell.Center.y + y * Data.Pattern.Size.y + drawingStartPoint.y + tileInfo.Position.y;
                        if(poseY < cell.BottomLeft.y || cell.TopRight.y <= poseY) continue;
                        
                        TilemapsForDrawing.SetTile(
                            tileInfo.Tile, 
                            drawingStartPoint + tileInfo.Position + 
                            (cell.Center + new Vector2Int(x, y) * Data.Pattern.Size), 
                            tileInfo.Layer);
                    }
                }
            }
        }
    }
}