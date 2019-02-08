using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : ContainerBase<Test>
{
    private void Start()
    {
        this.ItemRemovedHandlers.Add(MyItemRemovedHandler);
        this.ItemDroppedHandlers.Add(MyItemDroppedHandler);
    }
    private void MyItemDroppedHandler(ItemDroppedEventArgs<Test> e)
    {
        Debug.Log($"{e.ContainedItem.name} was dropped into {this.name}");
    }
    private void MyItemRemovedHandler(ItemRemovedEventArgs<Test> e)
    {
        Debug.Log($"{e.ContainedItem.name} was removed from {this.name}");
    }
}
