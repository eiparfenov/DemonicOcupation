using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace Maze.Generation.Helpers
{
    public class RoomVisualizer: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        public void ShowCell(Cell cell)
        {
            rectTransform.position = ((Vector3)(cell.BottomLeft.ToVector3Int() + cell.TopRight.ToVector3Int())) / 2;
            rectTransform.sizeDelta = cell.Size;
            image.color = Random.ColorHSV(0, 1, 0, 1, .5f, 1, .3f, .3f);
            //text.text = cell.SeparatingTag;
        }
    }
}