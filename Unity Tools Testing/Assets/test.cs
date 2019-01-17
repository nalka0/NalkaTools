using Nalka.Tools.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : DraggableBase
{
    protected override void Start()
    {
        base.Picked += Test_Picked;
        base.Released += Test_Released;
        base.Start();
    }

    private void Test_Released(DraggableReleasedEventArgs<DraggableBase> e)
    {
        Debug.Log($"{name} released");
    }

    private void Test_Picked(DraggablePickedEventArgs<DraggableBase> e)
    {
        Debug.Log($"{name} picked");
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}