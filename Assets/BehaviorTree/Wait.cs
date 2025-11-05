using UnityEngine;

public class Wait : Node
{
    float secondsToWait;
    float timer;

    public Wait(Condition[] conditions, float secondsToWait, BehaviorTree BT) : base(BT, conditions)
    {
        this.secondsToWait = secondsToWait;
    }

    public override void EvaluateActions()
    {
        timer = 0;
        base.EvaluateActions();
    }

    public override void Tick(float deltaTime)
    {
        if (interrupted)
            return;

        timer += deltaTime;
        if(timer > secondsToWait)
        {
            FinishAction(true);
        }
    }
}
