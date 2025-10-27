using System;
using System.Collections.Generic;
using Node = Unity.GraphToolkit.Editor.Node;
using CustomType;

[Serializable]
public class StartNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort("out").Build();
    }
}

[Serializable]
public class EndNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
    }
}
[Serializable]
public class DialogueNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
        context.AddOutputPort("out").Build();
        
        context.AddInputPort<DialogueInfo>("Details").Build();
    }
}

[Serializable]
public class ChoiceNode : Node
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort("in").Build();
        context.AddOutputPort("choice 1").Build();
        context.AddOutputPort("choice 2").Build();
        
        context.AddInputPort<ChoiceInfo>("Details").Build();
    }
}