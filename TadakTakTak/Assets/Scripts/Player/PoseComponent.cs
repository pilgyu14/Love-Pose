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
    private bool isPosing; // 포즈 취하는 중 
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private FireComponent _fireComponent;

    [SerializeField]
    private List<AbstractPose> _poseList = new List<AbstractPose>(); // 보유 포즈 
    private AbstractPose _curPose; // 현재 포즈
    public AbstractPose CurPose => _curPose; 
    protected AbstractPose _prePose; // 이전 포즈 

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
    /// Q 와 E 입력을 통해 포즈 취하기와 해제 
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
    /// 포즈 애니메이션 실행 
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
    /// 포즈 변경 
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
        EventManager.Instance.TriggerEvent(EventsType.SetPoseImage, (int)_curPoseType + 1); // 포즈 UI 변경 
        AnimatePose(true); 
    }

    /// <summary>
    /// 현재 포즈 찾기 
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
