using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPose 
{
    public PoseSO _poseSO; 
    public PoseType _poseType;
    public PoseType PoseType => _poseType;

    public abstract void Posing();
    public abstract void Cancel();
    public abstract void SetPose(); 
}
