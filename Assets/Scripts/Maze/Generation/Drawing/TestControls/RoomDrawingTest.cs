using System;
using System.Linq;
using Maze.Generation.Drawing.DrawersData;
using NaughtyAttributes;
using Shared.Sides;
using UnityEngine;
using Zenject;

namespace Maze.Generation.Drawing.TestControls
{
    public class RoomDrawingTest: MonoBehaviour
    {
        [SerializeField] private DrawerBaseData drawerBaseData;
        [SerializeField] private Vector2Int bottomLeft;
        [SerializeField] private Vector2Int topRight;
        [SerializeField] private GateDescriptor[] gates;

        private MazeDrawer _mazeDrawer;

        [Inject]
        public void Construct(MazeDrawer mazeDrawer)
        {
            _mazeDrawer = mazeDrawer;
        }

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
            _mazeDrawer.DrawCell(cell, drawerBaseData);
        }
        [Button()]
        private void Clear()
        {
            var cell = Cell.FromCoords(bottomLeft, topRight, null);
            _mazeDrawer.ClearCell(cell);
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