using Maze.Generation.Drawing.TestControls;
using UnityEngine;
using Zenject;

namespace Maze.Generation.Drawing.Infrastructure.Installers
{
    public class RoomDrawingTestMonoInstaller: MonoInstaller
    {
        [SerializeField] private RoomDrawingTest roomDrawingTest;

        public override void InstallBindings()
        {
            Container.QueueForInject(roomDrawingTest);
        }
    }
}