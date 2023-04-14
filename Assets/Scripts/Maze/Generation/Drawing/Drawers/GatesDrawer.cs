using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation.Drawing.Drawers
{
    [CreateAssetMenu(menuName = "DemonicOcupation/Maze/Drawers/GatesDrawer", fileName = "GatesDrawer")]
    public class GatesDrawer: BaseDrawer
    {
        [SerializeField] private TileBase tileForEntrance;
        [SerializeField] private TileBase tileForSide;
        [SerializeField] private int borderLength;
        [SerializeField] private int borderDistance;
        public override void Draw(Cell cell, Tilemap tilemap)
        {
            foreach (var gate in cell.Gates)
            {
                foreach (var pos in gate.Border(borderLength, borderDistance))
                {
                    tilemap.SetTile(pos.ToVector3Int(), tileForSide);
                }
            }
        }
    }
}