using Maze.Generation.Drawing.Drawers;
using Maze.Generation.Drawing.DrawersData;
using Maze.Generation.Drawing.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Maze.Generation.Drawing.Infrastructure
{
    public class DrawingMonoInstaller: MonoInstaller
    {
        [SerializeField] private Grid toDraw;
        [SerializeField] private GameObject tilemapPref;
        public override void InstallBindings()
        {
            Container.BindInstance(tilemapPref).AsCached().WhenInjectedInto<TilemapFactory>();
            Container.BindInstance(toDraw.transform).AsCached().WhenInjectedInto<TilemapFactory>();
            Container.BindFactory<int, Tilemap, TilemapFactory.PlaceholderFactory>()
                .FromFactory<TilemapFactory>();

            Container.Bind<TilemapsForDrawing>().AsCached();

            Container.BindFactory<DrawerBaseData, IDrawer, DrawerFactory.PlaceholderFactory>()
                .FromFactory<DrawerFactory>();

            Container.Bind<MazeDrawer>().AsCached();
        }
    }
}