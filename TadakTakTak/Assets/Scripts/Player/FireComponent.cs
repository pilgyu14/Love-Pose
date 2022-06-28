using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class FireComponent : IComponent
{
    [SerializeField]
    private List<AbstractGun> _guns;

    private GunComponent _gunComponent;
    private InputComponent _inputComponent;
    private PoseComponent _poseComponent; 
    private PlayerMoveComponent _playerMoveComponent; 

    private float _shootDelay; 
    private bool _isEmpty; // ¸ðµç ÃÑÀÇ ÃÑ¾ËÀÌ ºñ¾ú´Â°¡ 
    private bool _isShotable;

    public bool IsShotable => _isShotable;

    [SerializeField]
    private Vector3 _reboundDir;
    [SerializeField]
    private float _reboundPower;
    [SerializeField]
    private int a = 0; 

    [SerializeField]
    private float _reboundPowerMultiple =0 ; // ¹Ýµ¿ °öÇÏ±â 
    

    [SerializeField]
    private PoseSO _poseSO = null; 
    public void Initialize(GunComponent gunComponent, InputComponent inputComponent,PlayerMoveComponent playerMoveComponent,PoseComponent poseComponent)
    {
        _gunComponent = gunComponent;
        _inputComponent = inputComponent;
        _playerMoveComponent = playerMoveComponent;
        _poseComponent = poseComponent;
        SetGunList();

    }

    public void UpdateSomething()
    {
        if(_inputComponent.IsFire == true)
        {
            Shot();
           
            _reboundDir = _poseComponent.CurPose._poseSO.poseInfo.reboundDir;
            _reboundPowerMultiple = _poseComponent.CurPose._poseSO.poseInfo.reboundPowerMultiple;

            _playerMoveComponent.Rebound(_reboundDir, reboundPower * _reboundPowerMultiple);
        }
    }

    public void SetGunData(Vector3 dir, float power,PoseInfo poseInfo, PoseType poseType)
    {

        _guns.ForEach(x => x.SetGunData(poseInfo));


        _reboundDir = dir;
        _reboundPowerMultiple = power;

        //_reboundDir = poseInfo.reboundDir;
        //_reboundPowerMultiple = poseInfo.reboundPowerMultiple;

        //Debug.LogError("reboundDir " + _reboundDir);
        //Debug.LogError("reboundPower " + _reboundPowerMultiple);
    }
    float reboundPower = 1f; 
    /// <summary>
    /// ÃÑ ½î±â 
    /// </summary>
    public void Shot()
    {
        _reboundPower = 0; 
        reboundPower = 0f; 
        _guns.ForEach(x => reboundPower += x.Fire());




        _reboundPower = reboundPower * _reboundPowerMultiple; 
    }

    /// <summary>
    /// ÇöÀç ÀåÂøÁßÀÎ ÃÑµé °¡Á®¿À±â 
    /// </summary>
    private void SetGunList()
    {
        Transform[] gunPosList = _gunComponent._gunPosList; 
        foreach(Transform gunPosParent in gunPosList)
        {
            int gunCount = gunPosParent.childCount;
            for(int i = 0; i < gunCount; i++)
            {
                if (gunPosParent.GetChild(i).childCount == 0) continue; 
                AbstractGun gun = gunPosParent.GetChild(i).GetChild(0)?.GetComponent<AbstractGun>();
                if(gun != null)
                {
                    _guns.Add(gun);
                }
            }
        }
        //for(int i =0; i < _gunParent.childCount; i++)
        //{
        //    _guns.Add(_gunParent.Find("GunPos").GetChild(i).GetComponent<AbstractGun>());
        //}
    }

}
