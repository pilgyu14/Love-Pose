using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : PoolableMono
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot); 
        if(_audioSource.clip != null)
        {
            _audioSource.Play(); 
        }
    }
    public override void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity; 
    }
}
