using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Maze;
using Maze.Generation.Drawing.Drawers;
using NaughtyAttributes;
using Shared.Sides;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tests
{
    public class RoomTest: MonoBehaviour
    {
        [SerializeField] private Grid tilemapsGrid;
        [SerializeField] private DrawerBase drawerBase;
        [SerializeField] private Vector2Int bottomLeft;
        [SerializeField] private Vector2Int topRight;
        [SerializeField] private GateDescriptor[] gates;

        [Button]
        private void Generate()
        {
            Clear();
            var cell = Cell.FromCoords(bottomLeft, topRight, null);
            cell.Gates.AddRange(gates.Select(gate => new Gate()
            {
                Direction = gate.direction,
                Length = gate.length,
                TopRight = gate.center + gate.direction.VectorCross() * gate.length / 2
            }));
            
            drawerBase.Draw(cell, GetTilemaps());
        }
        [Button()]
        private void Clear()
        {
            foreach (var tilemap in GetTilemaps().Select(x => x.Value))
            {
                tilemap.ClearAllTiles();
            }
        }
        private Dictionary<int, Tilemap> GetTilemaps()
        {
            var tilemaps = new Dictionary<int, Tilemap>();
            var tilemapsData = tilemapsGrid.GetComponentsInChildren<Tilemap>()
                .Select(tilemap => (tilemap, tilemap.GetComponent<TilemapRenderer>().sortingOrder));
            foreach (var tilemapData in tilemapsData)
            {
                tilemaps.Add(tilemapData.sortingOrder, tilemapData.tilemap);
            }

            return tilemaps;
        }
        
    }

    [Serializable]
    public class GateDescriptor
    {
        public Vector2Int center;
        public Direction direction;
        public int length;
    }
}