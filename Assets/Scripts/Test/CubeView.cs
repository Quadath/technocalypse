using System;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    [SerializeField] private Transform t;
    private Cube _cube;
    
    void Start()
    {
        _cube = new Cube(10)
        {
            Callback = (cube => DebugUtil.Log("CubeView", "Cube has been destroyed"))
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (t.position.y <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        
        _cube = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Destroy(gameObject);
    }
}
