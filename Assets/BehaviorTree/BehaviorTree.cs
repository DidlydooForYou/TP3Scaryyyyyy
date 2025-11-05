using System.Threading;
using UnityEngine;

abstract public class BehaviorTree : MonoBehaviour
{
    protected Node root;
    public Node activeNode;

    abstract protected void InitializeTree();

    private void Start()
    {
        InitializeTree();
        EvaluateTree();
    }

    void Update()
    {
        if (activeNode != null)
            activeNode.Tick(Time.deltaTime);
    }

    public void EvaluateTree()
    {
        root.EvaluateActions();
    }

    public void Interrupt()
    {
        activeNode.Interrupt();
        EvaluateTree();
    }
}
