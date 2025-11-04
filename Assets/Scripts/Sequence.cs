using UnityEngine;

public class Sequence : Node
{
    Node[] children;
    int index = 0;

    public Sequence(Node[] children, Condition[] conditions, BehaviorTree BT): base(BT, conditions)
    {
        this.children = children;
        foreach (Node child in children)
            child.SetParent(this);
    }

    public override void EvaluateActions()
    {
        base.EvaluateActions();
        SequenceContinue(index);
    }
    
    public void SequenceContinue(int index)
    {
        children[index].EvaluateActions();
    }

    public override void FinishAction(bool success)
    {
        if (!success)
        {
            index = 0;
            base.FinishAction(success);
        }
        else if(index == children.Length - 1)
        {
            index = 0;
            base.FinishAction(true);
        }
        else
        {
            index++;
            children[index].EvaluateActions();
        }
    }
}
