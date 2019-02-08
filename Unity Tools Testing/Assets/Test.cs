using Nalka.Tools.Unity;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

public class Test : DraggableBase<Test>
{
    private void Start()
    {
        this.PickedHandlers.Add(MyPickedHandler);
        this.HoldingHandlers.Add(MyHoldingHandler);
        this.ReleasedHandlers.Add(MyReleasedHandler);
    }

    private void MyReleasedHandler(DraggableReleasedEventArgs<Test> e)
    {
        if (e.ReachedContainer)
        {
            Debug.Log($"{e.MovedObject.name} now contained into {e.MovedObject.Container.name }");
        }
        else
        {
            Debug.Log($"Didn't reach any container");
        }
    }

    private void MyHoldingHandler(DraggableHoldingEventArgs<Test> e)
    {
        Debug.Log($"{e.MovedObject.name} is being held");
    }

    private void MyPickedHandler(DraggablePickedEventArgs<Test> e)
    {
        Debug.Log($"{e.MovedObject.name} has been picked");
    }
}
