using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nalka.Tools.Unity
{
    public abstract class ContainerBase<T> : MonoBehaviour, IDropHandler where T : DraggableBase
    {
        private T _containedObject;

        public virtual T ContainedObject
        {
            get => _containedObject;
            set
            {
                _containedObject.Picked -= CheckForItemRemoved;
                _containedObject = value;
                _containedObject.Picked += CheckForItemRemoved;
            }
        }

        private void CheckForItemRemoved(DraggablePickedEventArgs<DraggableBase> e)
        {
            ItemRemoved?.Invoke();
        }

        public virtual event ItemDroppedEventHandler ItemDropped;
        public virtual event ItemRemovedEventHandler ItemRemoved;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            ItemDropped?.Invoke();
        }
    }
}