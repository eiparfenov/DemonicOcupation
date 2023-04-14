using System.Collections.Generic;
using Maze.Generation.Helpers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utils.Extensions;

namespace Maze.Generation
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private GeneratorSettings settings;
        [SerializeField] private Tilemap tilemap;

        [SerializeField] private RoomVisualizer visualizerPref; 
        [SerializeField] private List<GameObject> createdVisualizers;
        [SerializeField] private bool visualize;
        [Button()]
        private void Run()
        {
            Clear();
            var cell = new Cell(-settings.InitialSize, settings.InitialSize, null, "root");
            settings.Separator.Separate(cell);
            foreach (var room in cell.AllFinishCells())
            {
                if (visualize)
                {
                    var visualizer = Instantiate(visualizerPref, transform);
                    visualizer.ShowCell(room);
                    createdVisualizers.Add(visualizer.gameObject);
                }

                foreach (var drawer in settings.DrawersToTest)
                {
                    drawer.Draw(room, tilemap);
                }
            }
            //var room = cell.AllFinishCells().RandomOrDefault();
            //print(room.TopRight);
            //print(room.BottomLeft);
            //foreach (var drawer in settings.DrawersToTest)
            //{                                             
            //    drawer.Draw(room, tilemap);               
            //}                                             
        }

        [Button()]
        private void Clear()
        {
            foreach (var createdVisualizer in createdVisualizers)
            {
                DestroyImmediate(createdVisualizer);
            }
            createdVisualizers.Clear();
            tilemap.ClearAllTiles();
        }
    }
}