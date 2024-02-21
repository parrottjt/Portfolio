using TMPro;
using UnityEngine;

namespace TSCore.Utils
{
    public static class GameObjectUtils
    {
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
            Color color, TextAnchor textAnchor, TextAlignment textAlignment, Vector3 rot = default)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localRotation = Quaternion.Euler(rot);
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            return textMesh;
        }

        public static TextMeshProUGUI CreateUIText(Transform parent, string text, Vector2 localPostion, Vector2 cellSize,
            int fontSize, Color color, TextAlignmentOptions textAlignment, string objectName = "UI_Text")
        {
            GameObject gameObject = new GameObject(objectName, typeof(TextMeshProUGUI));
            RectTransform transform = (RectTransform)gameObject.transform;
            transform.sizeDelta = cellSize;
            transform.SetParent(parent, false);
            transform.localPosition = localPostion;
            TextMeshProUGUI textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.alignment = textAlignment;
            textMeshProUGUI.text = text;
            textMeshProUGUI.fontSize = fontSize;
            textMeshProUGUI.color = color;
            return textMeshProUGUI;
        }
    }
}
