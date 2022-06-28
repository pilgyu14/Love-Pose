using UnityEngine;

[System.Serializable]
public abstract class AIState
{
    [SerializeField]
    protected Enemy enemy; 

    protected State state;

    [SerializeField]
    protected State _positiveState;
    [SerializeField]
    protected State _nagativeState;

    public abstract void UpdateState();

    protected abstract void DecideState();
}
