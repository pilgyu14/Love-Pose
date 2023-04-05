using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Main.Event; 
public enum PoseType
{
    TPose,
    PushUp,
    Powerless, 

}
[System.Serializable]
public class PoseComponent : IComponent
{
    private bool isPosing; // ���� ���ϴ� �� 
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private FireComponent _fireComponent;

    [SerializeField]
    private List<AbstractPose> _poseList = new List<AbstractPose>(); // ���� ���� 
    private AbstractPose _curPose; // ���� ����
    public AbstractPose CurPose => _curPose; 
    protected AbstractPose _prePose; // ���� ���� 

    [SerializeField]
    private PoseType _curPoseType;
    public PoseType CurPoseType => _curPoseType;
    [SerializeField]
    private int _poseIndex = 0;

    private Vector3 _reboundDir;
    private float _reboundPowerMultiple; 

    [SerializeField]
    private TPose _tPose;
    [SerializeField]
    private PushUpPose _pushUpPose;
    [SerializeField]
    private PowerlessPose _powerlessPose;

    public UnityEvent<bool> TposeEvent = null; 
    public UnityEvent<bool> PushUpEvent = null; 
    public UnityEvent<bool> PowerlessEvent = null;
    public void Initialize()
    {

        _poseList.Add(_tPose);
        _poseList.Add(_pushUpPose);
        _poseList.Add(_powerlessPose);
        
        for(int i = 0; i < _poseList.Count; i++)
        {
            _poseList[i].SetPose(); 
        }
    }

    /// <summary>
    /// Q �� E �Է��� ���� ���� ���ϱ�� ���� 
    /// </summary>
    public void UpdateSomething()
    {
        if (_playerController.IsPosing == true)
        {
            _curPose.Posing();
        }

        //_curPose.Posing(); 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangePose(); 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CancelPose(); 
        }
    }
    
    /// <summary>
    /// ���� �ִϸ��̼� ���� 
    /// </summary>
    /// <param name="isPose"></param>
    private void AnimatePose(bool isPose)
    {
        switch (_curPoseType)
        {
            case PoseType.TPose:
                TposeEvent?.Invoke(isPose); 
                break;
            case PoseType.PushUp:
                PushUpEvent?.Invoke(isPose); 
                break;
            case PoseType.Powerless:
                PowerlessEvent?.Invoke(isPose); 
                break;
        }
    }
    int a = 0; 
    /// <summary>
    /// ���� ���� 
    /// </summary>
    private void ChangePose()
    {
        if (_poseIndex >= _poseList.Count )
        {
            _poseIndex = 0;
        }

        _curPose = _poseList[_poseIndex++];
        _reboundDir = _curPose._poseSO.poseInfo.reboundDir;
        _reboundPowerMultiple = _curPose._poseSO.poseInfo.reboundPowerMultiple;

        _curPoseType = _curPose.PoseType;
        _fireComponent.SetGunData(_reboundDir, _reboundPowerMultiple, _curPose._poseSO.poseInfo, _curPoseType);

        _playerController.IsPosing = true;
        EventManager.Instance.TriggerEvent(EventsType.SetPoseImage, (int)_curPoseType + 1); // ���� UI ���� 
        AnimatePose(true); 
    }

    /// <summary>
    /// ���� ���� ã�� 
    /// </summary>
    /// <returns></returns>
    public AbstractPose FIndCurPose()
    {
        return _poseList.Find((x) => x._poseType == _curPoseType);
    }
    private void CancelPose()
    {
        _playerController.IsPosing = false;
        AnimatePose(false);
        EventManager.Instance.TriggerEvent(EventsType.SetPoseImage, 0);
    }
    public void AddPose(AbstractPose newPose)
    {
        _poseList.Add(newPose); 
    }

    public void RemovePose(AbstractPose removePose)
    {
        _poseList.Remove(removePose);
    }

}
