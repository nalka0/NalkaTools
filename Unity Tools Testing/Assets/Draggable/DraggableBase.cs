using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nalka.Tools.Unity
{
    public abstract class DraggableBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Fields
        private bool selected;
        #endregion

        #region Events
        public virtual event ObjectPickedEventHandler Picked;
        public virtual event HoldingObjectEventHandler Holding;
        public virtual event ObjectReleasedEventHandler Released;
        #endregion
        
        #region Unity methods
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
            Picked?.Invoke(new DraggablePickedEventArgs<DraggableBase>(this));
            selected = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Released?.Invoke(new DraggableReleasedEventArgs<DraggableBase>(this));
            selected = false;
        }
        #endregion
    }
}