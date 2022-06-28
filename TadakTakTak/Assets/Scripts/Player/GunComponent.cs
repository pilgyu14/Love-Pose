using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public enum GunPosType
{ 
    Head,
    LNeck,
    RNeck,
    Body,
    LShoulder,
    RShoulder,
    LArm,
    RArm,
    LLeg,
    RLeg,
}

[Serializable]
public class GunComponent 
{
    public Transform[] _gunPosList; 


}
