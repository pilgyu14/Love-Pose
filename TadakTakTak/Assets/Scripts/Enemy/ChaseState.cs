using UnityEngine;

[System.Serializable]
public class ChaseState : AIState
{
    [SerializeField]
    private float _attackDistance = 10f;
    [SerializeField]
    private float _chaseDistance = 30f;
    public override void UpdateState()
    {
        DecideState();
        enemy.ChaseTarget();
        enemy.isRotate = true;
    
    }

    protected override void DecideState()
    {
        if (enemy.CheckDistance(_attackDistance) == true)
        {
            enemy.ChangeState(_positiveState);
        }
        else if(enemy.CheckDistance(_chaseDistance) == false)
        {
            enemy.ChangeState(_nagativeState); 
        }
    }
}
