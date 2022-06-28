using UnityEngine;
public class EnemyAnimation : MonoBehaviour
{ 
    private readonly int _hashRun = Animator.StringToHash("IsRun");
    private readonly int _hashHit = Animator.StringToHash("Hit");

    private Animator _anim; 
    private void Start()
    {
        _anim = GetComponent<Animator>(); 
    }

    public void AnimateHit()
    {
        _anim.SetTrigger(_hashHit);
    }

    public void AnimateRun(bool isRun)
    {
        _anim.SetBool(_hashRun, isRun);
    }
}
