using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nalka.Tools.Unity
{
    public abstract class DraggableBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event ObjectPickedEventHandler Picked;
        public event HoldingObjectEventHandler Holding;
        public event ObjectReleasedEventHandler Released;

        private bool selected;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (selected)
            {
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            Picked?.Invoke();
            selected = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Released?.Invoke(new DraggableReleasedEventArgs<DraggableBase>(this));
            selected = false;
        }
    }
}