using UnityEngine;
using System; 

[Serializable]
public class PowerlessPose : AbstractPose
{
    public override void Cancel()
    {
    }

    public override void Posing()
    {
    }

    public override void SetPose()
    {
        _poseType = PoseType.Powerless; 
    }
}
