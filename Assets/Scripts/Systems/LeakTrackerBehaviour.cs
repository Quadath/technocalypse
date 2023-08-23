using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Systems
{
    public class LeakCheckBehaviour : MonoBehaviour
    {
        void Update()
        {
            if (Keyboard.current.lKey.isPressed)
            {
                DebugUtil.Log("LeakCheck", "Running leak check...", "magenta");
                LeakTracker.ForceCheck();
            }
        }
    }
}
