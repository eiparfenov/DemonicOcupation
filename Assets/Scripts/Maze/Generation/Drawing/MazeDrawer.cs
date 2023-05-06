using Maze.Generation.Drawing.DrawersData;
using Maze.Generation.Drawing.Infrastructure.Factories;
using Zenject;

namespace Maze.Generation.Drawing
{
    public class MazeDrawer
    {
        private readonly DrawerFactory.PlaceholderFactory _factory;
        private readonly TilemapsForDrawing _tilemapsForDrawing;

        [Inject]
        public MazeDrawer(DrawerFactory.PlaceholderFactory factory, TilemapsForDrawing tilemapsForDrawing)
        {
            _factory = factory;
            _tilemapsForDrawing = tilemapsForDrawing;
        }

        public void DrawCell(Cell cell, DrawerBaseData drawerBaseData)
        {
            var drawer = _factory.Create(drawerBaseData);
            drawer.Draw(cell);
        }

        public void ClearCell(Cell cell)
        {
            foreach (var poseInside in cell.AllPosesInside())
            {
                _tilemapsForDrawing.ClearTile(poseInside);
            }
        }
    }
}