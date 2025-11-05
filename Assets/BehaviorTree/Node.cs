using System.Diagnostics;
using UnityEngine;

public abstract class Node
{
    protected BehaviorTree BT;
    protected Node parent;
    protected Condition[] conditions;
    protected bool interrupted = false;

    public Node(BehaviorTree BT, Condition[] conditions)
    {
        this.BT = BT;
        this.conditions = conditions;
    }

    public void SetParent(Node parent)
    {
        this.parent = parent;
    }
    virtual public void Tick(float deltaTime)
    {

    }


    public bool EvaluateConditions()
    {
        if (this.conditions == null)
            return true;
        foreach(Condition c in conditions)
        {
            if (!c.Evaluate())
                return false;
        }
        return true;
    }

    virtual public void EvaluateActions()
    {
        if (!EvaluateConditions())
            FinishAction(false);

        BT.activeNode = this;
    }

    virtual public void FinishAction(bool success)
    {
        if (!interrupted && parent != null)
            FinishAction(success);
        else
            BT.EvaluateTree();
    }

    virtual public void Interrupt()
    {
        if (parent != null)
            parent.Interrupt();
    }
}
