using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UICore
{
    public class UIAddTransparentToSection : MonoBehaviour
    {
        public enum UIElement
        {
            BossHealthBar,
            LaserSlider,
            ScoreArea,
            BossExtra
        }

        public void SetTransparencyToSection(UIElement uiElement, float transparency)
        {
            switch (uiElement)
            {
                case UIElement.BossHealthBar:
                    SetTransImage(UIManager.instance.BossHealthBar.BossBackground, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.bossFill, transparency);
                    SetTransText(UIManager.instance.InfoHolder.bossText, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.bossBackground, transparency);
                    break;

                case UIElement.LaserSlider:
                    SetTransImage(UIManager.instance.InfoHolder.laserFill, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.laserBackground, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.laserNameBackground, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.ammoBorderBackground, transparency);
                    SetTransText(UIManager.instance.InfoHolder.laserName, transparency);
                    break;

                case UIElement.ScoreArea:
                    SetTransImage(UIManager.instance.InfoHolder.upRightBackgroundImage, transparency);
                    SetTransImage(UIManager.instance.InfoHolder.teethImage, transparency);
                    SetTransText(UIManager.instance.InfoHolder.teethText, transparency);
                    break;
            }
        }

        void SetTransImage(Image imageToSet, float transNumber)
        {
            Color _alpha = imageToSet.color;
            _alpha.a = transNumber;
            imageToSet.color = _alpha;
        }

        void SetTransText(Text textToSet, float transNumber)
        {
            Color _alpha = textToSet.color;
            _alpha.a = transNumber;
            textToSet.color = _alpha;
        }

        void SetTransText(TMP_Text textToSet, float transNumber)
        {
            Color _alpha = textToSet.color;
            _alpha.a = transNumber;
            textToSet.color = _alpha;
        }
    }
}
