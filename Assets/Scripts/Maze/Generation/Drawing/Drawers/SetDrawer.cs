using Maze.Generation.Drawing.DrawersData;
using Maze.Generation.Drawing.Infrastructure.Factories;

namespace Maze.Generation.Drawing.Drawers
{
    public class SetDrawer: DrawerBase<SetDrawersData>
    {
        private readonly DrawerFactory.PlaceholderFactory _factory;
        public SetDrawer(SetDrawersData data, TilemapsForDrawing tilemapsForDrawing, DrawerFactory.PlaceholderFactory factory) : base(data, tilemapsForDrawing)
        {
            _factory = factory;
        }

        public override void Draw(Cell cell)
        {
            foreach (var drawerBaseData in Data.DrawersToApply)
            {
                var drawer = _factory.Create(drawerBaseData);
                drawer.Draw(cell);
            }
        }
    }
}