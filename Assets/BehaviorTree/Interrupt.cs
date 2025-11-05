using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Interrupt
{
    Condition[] conditions;
    BehaviorTree BT;
    bool[] conditionState;

    CancellationTokenSource cts;

    public Interrupt(Condition[] conditions, BehaviorTree BT)
    {
        this.conditions = conditions;
        this.BT = BT;
        conditionState = new bool[conditions.Length];

        Start();
    }

    async private void CheckConditions(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            for (int index = 0; index < conditions.Length; index++)
            {
                if (conditions[index].Evaluate() != conditionState[index])
                {
                    BT.Interrupt();
                    UpdateState();
                    break;
                }
            }
            await Task.Delay(100);
        }
    }

    private void UpdateState()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditionState[i] = conditions[i].Evaluate();
        }
    }

    public void Start()
    {
        cts = new CancellationTokenSource();
        UpdateState();
        CheckConditions(cts.Token);
    }

    public void Stop()
    {
        cts.Cancel();
    }
}
