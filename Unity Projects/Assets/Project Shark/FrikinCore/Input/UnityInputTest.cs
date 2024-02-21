using FrikinCore.Input;
using UnityEngine;

public class UnityInputTest : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    void Update()
    {
        if (InputManager.instance.OpenMenuInput())
        {
            _canvas.SetActive(true);
        }

        if (InputManager.instance.CloseMenuInput())
        {
            _canvas.SetActive(false);
        }

        if (InputManager.instance.AttackInput())
        {
            print("Fire");
        }
    }
}
