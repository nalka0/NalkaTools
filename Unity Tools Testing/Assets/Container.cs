using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : ContainerBase<Draggable>
{
    private void Start()
    {
        ItemRemovedHandlers.Add(MyItemRemovedHandler);
        ItemDroppedHandlers.Add(MyItemDroppedHandler);
    }
    private void MyItemDroppedHandler(ItemDroppedEventArgs<Draggable> e)
    {
        Debug.Log($"{e.ContainedItem.name} was dropped into {name}");
    }
    private void MyItemRemovedHandler(ItemRemovedEventArgs<Draggable> e)
    {
        Debug.Log($"{e.ContainedItem.name} was removed from {name}");
    }
}
