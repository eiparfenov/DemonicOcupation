using System.Collections.Generic;
using Maze.Generation.Drawing.Drawers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Generation.Drawing
{
    [CreateAssetMenu(menuName = "DemonicOccupation/Maze/Drawers/DrawerSet")]
    public class DrawersSet: DrawerBase
    {
        [SerializeField] private DrawerBase[] drawersToApply;
        public override void Draw(Cell cell, Dictionary<int, Tilemap> tilemaps)
        {
            foreach (var drawerBase in drawersToApply)
            {
                drawerBase.Draw(cell, tilemaps); 
            }
        }
    }
}