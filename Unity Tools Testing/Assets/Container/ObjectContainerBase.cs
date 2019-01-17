using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nalka.Tools.Unity
{
    public class ContainerBase<T> : MonoBehaviour, IDropHandler where T : DraggableBase
    {
        #region Fields
        private T _containedObject;
        #endregion

        #region Properties
        public virtual T ContainedObject
        {
            get => _containedObject;
            set
            {
                if (_containedObject != null)
                    _containedObject.Picked -= CheckForItemRemoved;
                _containedObject = value;
                if (_containedObject != null)
                    _containedObject.Picked += CheckForItemRemoved;
            }
        }
        #endregion

        #region Events
        public virtual event ItemDroppedEventHandler ItemDropped;
        public virtual event ItemRemovedEventHandler ItemRemoved;
        #endregion
    
        #region Ctors
        protected ContainerBase() { }

        protected ContainerBase(T containedObject)
        {
            ContainedObject = containedObject;
        }
        #endregion

        #region Unity methods
        public virtual void OnDrop(PointerEventData eventData)
        {
            ItemDropped?.Invoke(new ItemDroppedEventArgs<ContainerBase<DraggableBase>>((ContainerBase<DraggableBase>)this));
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }
        #endregion

        #region Methods
        private void CheckForItemRemoved(DraggablePickedEventArgs<DraggableBase> e)
        {
            ItemRemoved?.Invoke(new ItemRemovedEventArgs<ContainerBase<DraggableBase>>((ContainerBase<DraggableBase>)this));
        }
        #endregion

        #region Operators
        public static explicit operator ContainerBase<T>(ContainerBase<DraggableBase> other) => new ContainerBase<T>((T)other.ContainedObject);

        public static explicit operator ContainerBase<DraggableBase>(ContainerBase<T> other) => new ContainerBase<DraggableBase>(other.ContainedObject);
        #endregion
    }
}