using UnityEngine;

public abstract class Condition
{
    protected bool reverseConditions;

    abstract public bool Evaluate();

    public bool CheckForConditions(bool result)
    {
        if (reverseConditions)
            result = !result;
        return result;
    }
}
