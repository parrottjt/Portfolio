using TSCore.Utils;
using UnityEngine;

namespace FrikinCore.AI.Enemy
{
    public class SpriteControl : MonoBehaviour
    {
        [SerializeField] DataEnemy dataEnemy;

        [SerializeField] SpriteRenderer mainSpriteRenderer;
        [SerializeField] SpriteRenderer[] extraSpriteRenderers;

        enum SpriteController
        {
            XFlip_OneSprite,
            XFlip_MultipleSprites,
            YFlip_OneSprite,
            YFlip_MultipleSprites,
            Specific_GhostFlip,
            Specific_SwordFish
        }

        [SerializeField] SpriteController spriteController;

        public void SpriteControlPattern()
        {
            switch (spriteController)
            {
                case SpriteController.XFlip_OneSprite:
                    mainSpriteRenderer.flipX = !dataEnemy.Movement.facingLeft;
                    break;
                case SpriteController.XFlip_MultipleSprites:
                    mainSpriteRenderer.flipX = !dataEnemy.Movement.facingLeft;
                    foreach (var extraSpriteRenderer in extraSpriteRenderers)
                    {
                        extraSpriteRenderer.flipX = !dataEnemy.Movement.facingLeft;
                    }

                    break;
                case SpriteController.YFlip_OneSprite:
                    print($"{gameObject.name} Sprite Control hasn't been setup");
                    break;
                case SpriteController.YFlip_MultipleSprites:
                    print($"{gameObject.name} Sprite Control hasn't been setup");
                    break;
                case SpriteController.Specific_GhostFlip:
                    mainSpriteRenderer.flipX = !dataEnemy.Movement.facingLeft;
                    extraSpriteRenderers[0].flipY = dataEnemy.Movement.facingLeft;
                    break;
                case SpriteController.Specific_SwordFish:
                    foreach (var extraSpriteRenderer in extraSpriteRenderers)
                    {
                        extraSpriteRenderer.flipY = dataEnemy.Movement.facingLeft;
                    }

                    break;
                default:
                    DebugScript.LogError($"{gameObject.name}",
                        "SpriteControlPattern() reverted to default, a case was not reached");
                    break;
            }
        }
    }
}