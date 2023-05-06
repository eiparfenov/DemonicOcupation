using Maze.Generation.Drawing.DrawersData;
using Zenject;

namespace Maze.Generation.Drawing.Drawers
{
    public abstract class DrawerBase<TData>: IDrawer where TData: DrawerBaseData
    {
        protected readonly TData Data;
        protected readonly TilemapsForDrawing TilemapsForDrawing;

        [Inject]
        protected DrawerBase(TData data, TilemapsForDrawing tilemapsForDrawing)
        {
            Data = data;
            TilemapsForDrawing = tilemapsForDrawing;
        }

        public abstract void Draw(Cell cell);
    }
}