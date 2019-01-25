using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nalka.Tools.Unity;
using UnityEngine.EventSystems;

public class TestContainer : ContainerBase<DraggableBase>
{
    protected void Start()
    {
        ItemDropped += e => Debug.Log($"{e.ItemDropped.name} was dropped into {name}");
        ItemRemoved += e => Debug.Log($"{e.RemovedItem.name} was removed from {name}");
    }

    protected void Update()
    {
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
