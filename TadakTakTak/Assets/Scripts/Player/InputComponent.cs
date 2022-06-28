using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[Serializable]
public class InputComponent : IComponent
{
    public bool IsFire { get; private set; }
    public Vector2 MoveDIr
    {
        get
        {
            return new Vector2(_moveX, _moveY);
        }
        private set
        {
        }
    }
    private float _moveX;
    private float _moveY;
    public void UpdateSomething()
    {
        _moveX = Input.GetAxis("Horizontal"); 
        _moveY = Input.GetAxis("Vertical"); 
        IsFire = Input.GetButton("Fire1");

    }

   
}
