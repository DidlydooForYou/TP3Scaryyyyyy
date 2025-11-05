using UnityEngine;

public class Selector : Node
{
    Node[] children;
    int index = 0;

    public Selector(Node[] Children, Condition[] condition, BehaviorTree BT) : base(BT,condition)
    {
        this.children = Children;
        foreach (Node child in children)
        {
            child.SetParent(this);
        }
    }
    public override void EvaluateActions()
    {
        base.EvaluateActions();
        children[index].EvaluateActions();
    }
    public override void FinishAction(bool result)
    {
        base.FinishAction(result);
        if (result)
        {
            base.FinishAction(true);
        }
        else if (index == children.Length - 1)
        {
            base.FinishAction(false);
        }
        else
        {
            index++;
            children[index].EvaluateActions();
        }
    }
}