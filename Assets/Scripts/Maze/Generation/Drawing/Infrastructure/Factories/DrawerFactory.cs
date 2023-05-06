using System.Linq;
using System.Reflection;
using Maze.Generation.Drawing.Drawers;
using Maze.Generation.Drawing.DrawersData;
using Zenject;

namespace Maze.Generation.Drawing.Infrastructure.Factories
{
    public class DrawerFactory: IFactory<DrawerBaseData, IDrawer>
    {
        public class PlaceholderFactory : PlaceholderFactory<DrawerBaseData, IDrawer> { }
        private readonly DiContainer _container;

        public DrawerFactory(DiContainer container)
        {
            _container = container;
        }

        public IDrawer Create(DrawerBaseData drawerData)
        {
            var drawerDisplayTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(IDrawer).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface && type.BaseType != null).ToList();
            var drawerType = drawerDisplayTypes
                .FirstOrDefault(type => type.BaseType!.GetGenericArguments().Length == 1 &&
                                        type.BaseType!.GetGenericArguments()[0] == drawerData.GetType());
            return (IDrawer) _container.Instantiate(drawerType, new object[]{drawerData});
        }
    }
}