using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.DrawersData
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/PatternFillDrawer")]

    public class PatternFillDrawer: DrawerBaseData
    {
        [SerializeField] private TilemapDataProvider pattern;
        public override void Draw(Cell cell, Dictionary<int, Tilemap> tilemaps)
        {
            var drawingStartPoint = - new Vector3Int(
                 Mathf.CeilToInt(cell.Size.x / 2f / pattern.Size.x),
                 Mathf.CeilToInt(cell.Size.y / 2f / pattern.Size.y)
                ) * pattern.Size.ToVector3Int();
            
            var drawingsCount = new Vector3Int(
                Mathf.CeilToInt((float) cell.Size.x / pattern.Size.x) + 1,
                Mathf.CeilToInt((float) cell.Size.y / pattern.Size.y) + 1
            );

            for (int x = 0; x < drawingsCount.x; x ++)
            {
                for (int y = 0; y < drawingsCount.y; y ++)
                {
                    foreach (var tileInfo in pattern.GetTiles())
                    {
                        var poseX = cell.Center.x + x * pattern.Size.x + drawingStartPoint.x + tileInfo.Position.x;
                        if(poseX < cell.BottomLeft.x || cell.TopRight.x <= poseX) continue;
                        
                        var poseY = cell.Center.y + y * pattern.Size.y + drawingStartPoint.y + tileInfo.Position.y;
                        if(poseY < cell.BottomLeft.y || cell.TopRight.y <= poseY) continue;
                        
                        SetTile(tilemaps, 
                            tileInfo.Layer, 
                            drawingStartPoint + tileInfo.Position + 
                            (cell.Center + new Vector2Int(x, y) * (Vector2Int)pattern.Size).ToVector3Int(), 
                            tileInfo.Tile);
                    }
                }
            }
        }
    }
}