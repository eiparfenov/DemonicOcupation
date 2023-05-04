﻿using System.Collections.Generic;
using System.Linq;
using Maze.Generation.Drawing;
using Maze.Generation.Helpers;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Generation
{
    public class MazeGenerator: MonoBehaviour
    {
        [SerializeField] private MazeGeneratorSettings settings;
        [SerializeField] private Grid tilesGrid;
        [SerializeField] private RoomVisualizer debugVisualizerPref;

        [SerializeField] private RoomVisualizer[] visualizers;
        [SerializeField] private int cellToDraw;

        private List<Cell> _cellsToDraw;
        private Dictionary<int, Tilemap> _tilemaps;
        [Button()] 
        private void Generate()
        {
            Clear();
            _tilemaps = GetTilemaps();
            var startCell = Cell.FromCoords(
                Vector2Int.one * -settings.Size,
                Vector2Int.one * settings.Size, 
                null);
            _cellsToDraw = Separate(startCell);
            ShowVisualizers(_cellsToDraw);
        }
        [Button()]
        private void DrawAllCells()
        {
            foreach (var cell in _cellsToDraw)
            {
                settings.DebugDrawer.Draw(cell, _tilemaps);
                settings.DebugGatesDrawer.Draw(cell, _tilemaps);
            }
        }
        [Button()]
        private void DrawCell()
        {
            var cell = _cellsToDraw[cellToDraw];
            settings.DebugDrawer.Draw(cell, _tilemaps);
            settings.DebugGatesDrawer.Draw(cell, _tilemaps);
        }


        [Button()]
        private void Clear()
        {
            foreach (var tilemap in GetTilemaps())
            {
                tilemap.Value.ClearAllTiles();
            }
        }

        private Dictionary<int, Tilemap> GetTilemaps()
        {
            var tilemaps = new Dictionary<int, Tilemap>();
            var tilemapsData = tilesGrid
                .GetComponentsInChildren<Tilemap>()
                .Select(tilemap => (tilemap, tilemap.GetComponent<TilemapRenderer>().sortingOrder));
            foreach (var tilemapData in tilemapsData)
            {
                tilemaps.Add(tilemapData.sortingOrder, tilemapData.tilemap);
            }

            return tilemaps;
        }

        private List<Cell> Separate(Cell startCell)
        {
            var result = settings.ToTest.Separate(startCell);
            result.AddRange(settings.ToTest2.Separate(result[0]) ?? new List<Cell>{result[0]});
            result.RemoveAt(0);
            return result;
        }

        private void ShowVisualizers(List<Cell> cells)
        {
            foreach (var roomVisualizer in visualizers)
            {
                DestroyImmediate(roomVisualizer.gameObject);
            }

            visualizers = new RoomVisualizer[cells.Count];
            for (int i = 0; i < visualizers.Length; i++)
            {
                visualizers[i] = Instantiate(debugVisualizerPref, transform);
                visualizers[i].ShowCell(cells[i]);
            }
        }
    }
}