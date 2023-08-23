using System;
using Game.Systems;
using UnityEngine;

public class Cube
{
    private int _size;
    public Action<Cube> Callback { get; set; }

    ~Cube()
    {
        Callback(null);
        Callback = null;
    }
    
    public Cube(int size)
    {
        _size = size;
        LeakTracker.Register(this);
    }
}
