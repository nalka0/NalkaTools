using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDraggable : DraggableBase
{
    protected void Start()
    {
        Picked += e => Debug.Log($"{e.PickedObject.name} was picked");
        Holding += e => Debug.Log($"{e.HoldingObject.name} is being held");
        Released += e => Debug.Log($"{e.ReleasedObject.name} was released");
    }

    protected void Update()
    {

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}