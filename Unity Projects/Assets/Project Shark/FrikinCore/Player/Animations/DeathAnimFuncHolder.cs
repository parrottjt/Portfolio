using UnityEngine;

namespace FrikinCore.Player.Animation
{
    public class DeathAnimFuncHolder : MonoBehaviour
    {

        [SerializeField] private Animator frikinAnim;

        public void FadeOut()
        {
            frikinAnim.SetBool("DeathFadeOut", true);
        }

        public void FadeIn()
        {
            frikinAnim.SetBool("DeathFadeOut", false);
            frikinAnim.SetBool("DeathFadeIn", true);
        }

        public void GetBlinkyWithIt()
        {
            frikinAnim.SetBool("isStunned", false);
        }

        public void SetDeathFadeInFalse()
        {
            frikinAnim.SetBool("DeathFadeIn", false);
        }
    }
}
