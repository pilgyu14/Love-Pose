using System;
using UnityEngine;

[Serializable]
public class PushUpPose : AbstractPose
{

    public override void Cancel()
    {
    }

    public override void Posing()
    {
    }

    public override void SetPose()
    {
        _poseType = PoseType.PushUp; 
    }
}

