using UnityEngine;

[System.Serializable]
public class IdleState : AIState
{

    [SerializeField]
    private float _chaseDistance = 30f;
    public override void UpdateState()
    {
        DecideState();
        enemy.SetIdle();
        enemy.isRotate = false;

    }

    protected override void DecideState()
    {
        //Debug.Log("IDLE");
        if (enemy.CheckDistance(_chaseDistance) == true)
        {
            enemy.ChangeState(_positiveState);
        }
    }

}

