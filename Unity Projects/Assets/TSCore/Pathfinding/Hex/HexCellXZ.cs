using TSCore.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TSCore.Pathfinding.Hex
{
    public class HexCellXZ : MonoBehaviour
    {
        public HexCoordinatesXZ coordinates;
        public Color color;
        [SerializeField] HexCellXZ[] _neighbors = new HexCellXZ[6];
        public RectTransform uiRect;

        Image _highlight;
        public bool CellHighlighted => _highlight.IsNotNull() && _highlight.enabled;

        public HexCellXZ GetNeighbor(HexDirection direction) => _neighbors[(int)direction]; 
        public void SetNeighbor(HexDirection direction, HexCellXZ cell)
        {
            _neighbors[(int)direction] = cell;
            cell._neighbors[(int)direction.Opposite()] = this;
        }

        public void ToggleHighlight(Color highlightColor = default)
        {
            if (_highlight.enabled)
            {
                DisableHighlight();
            }
            else
            {
                EnableHighlight(highlightColor);
            }
        }
        
        
        public void DisableHighlight()
        {
            if (_highlight.IsNull()) _highlight = uiRect.GetChild(0).GetComponent<Image>();
            _highlight.enabled = false;
        }

        public void EnableHighlight(Color highlightColor = default)
        {
            if (_highlight.IsNull()) _highlight = uiRect.GetChild(0).GetComponent<Image>();
            _highlight.color = highlightColor;
            _highlight.enabled = true;
        }
    }
}
