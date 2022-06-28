using UnityEngine;

[System.Serializable]
public class AttackState : AIState
{
    [SerializeField]
    private float _attackDistance = 10f;

    public override void UpdateState()
    {
        DecideState();
        enemy.PerformAttack();
        enemy.isRotate = true;
    }

    protected override void DecideState()
    {
        if (enemy.CheckDistance(_attackDistance) == false)
        {
            enemy.ChangeState(_nagativeState);
        }
    }
}
