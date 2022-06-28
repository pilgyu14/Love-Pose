using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main.Event;

public class PoseUI : MonoBehaviour
{
    [SerializeField]
    private Image _poseImage;
    [SerializeField]
    private Sprite[] _poseSprites;

    private void Start()
    {
        EventManager.Instance.StartListening(EventsType.SetPoseImage, (x) =>SetPoseImage((int)x));
    }
    public void SetPoseImage(int poseType)
    {
        _poseImage.sprite = _poseSprites[poseType];
    }
}
