using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[Serializable]
public class TPose : AbstractPose
{
    public override void Cancel()
    {

    }

    public override void Posing()
    {

    }

    public override void SetPose()
    {
        _poseType = PoseType.TPose; 
    }
}
