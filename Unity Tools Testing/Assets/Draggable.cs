using Nalka.Tools.Unity;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

public class Draggable : DraggableBase<Draggable>
{
    public int Degats { get; set; }
    private void Start()
    {
        PickedHandlers.Add(MyPickedHandler);
        HoldingHandlers.Add(MyHoldingHandler);
        ReleasedHandlers.Add(MyReleasedHandler);
    }
    
    private void MyReleasedHandler(DraggableReleasedEventArgs<Draggable> e)
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

    private void MyHoldingHandler(DraggableHoldingEventArgs<Draggable> e)
    {
        Debug.Log($"{e.MovedObject.name} is being held");
    }

    private void MyPickedHandler(DraggablePickedEventArgs<Draggable> e)
    {
        Debug.Log($"{e.MovedObject.name} has been picked");
    }
}