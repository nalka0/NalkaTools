using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nalka.Tools.Unity;
using UnityEngine.EventSystems;

public class TestContainer : ContainerBase<DraggableBase>
{
    public override DraggableBase ContainedObject
    {
        get => base.ContainedObject;
        set => base.ContainedObject = value;
    }

    protected override void Start()
    {
        base.ItemDropped += TestContainer_ItemDropped;
        base.ItemRemoved += TestContainer_ItemRemoved;
        base.Start();
    }

    private void TestContainer_ItemRemoved(ItemRemovedEventArgs<ContainerBase<DraggableBase>> e)
    {
        Debug.Log($"aurevoir");
    }

    private void TestContainer_ItemDropped(ItemDroppedEventArgs<ContainerBase<DraggableBase>> e)
    {
        Debug.Log($"Salut");
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
