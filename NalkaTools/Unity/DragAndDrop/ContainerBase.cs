using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NalkaTools.Unity.DragAndDrop
{
    /// <summary>
    /// Base class for any object that can recieve draggable objects
    /// </summary>
    /// <typeparam name="ContainedT">Type of the object this container can recieve</typeparam>
    public class ContainerBase<ContainedT> : MonoBehaviour, IDropHandler where ContainedT : DraggableBase<ContainedT>
    {
        #region Fields
        private ContainedT _containedObject;
        #endregion

        #region Properties
        /// <summary>
        /// The object being contained in this container
        /// </summary>
        public ContainedT ContainedObject
        {
            get => _containedObject;
            private set
            {
                if (_containedObject != null)
                    if (!_containedObject.PickedHandlers.Remove(CheckForItemRemoved))
                        Debug.LogError($"Something went wrong when removing {_containedObject.name} from {name}, please report the issue on discord or github and give us enough information so that we can reproduce your issue");
                _containedObject = value;
                if (_containedObject != null)
                    _containedObject.PickedHandlers.Add(CheckForItemRemoved);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Contains all the methods to be invoked when an item is dropped in the container
        /// </summary>
        public List<Action<ItemDroppedEventArgs<ContainedT>>> ItemDroppedHandlers { get; } = new List<Action<ItemDroppedEventArgs<ContainedT>>>();

        /// <summary>
        /// Contains all the methods to be invoked when an item is removed from the container
        /// </summary>
        public List<Action<ItemRemovedEventArgs<ContainedT>>> ItemRemovedHandlers { get; } = new List<Action<ItemRemovedEventArgs<ContainedT>>>();
        #endregion

        #region Unity methods
        /// <summary>
        /// Raised by Unity
        /// </summary>
        /// <param name="eventData">Provided by unity</param>
        [Obsolete("Only Unity should invoke this method", true)]
        public void OnDrop(PointerEventData eventData)
        {
            if (ContainedObject == null)
            {
                ContainedT dragged = eventData.pointerDrag.GetComponent<ContainedT>();
                if (dragged)
                {
                    dragged.Container = this;
                    ContainedObject = dragged;
                    ItemDroppedHandlers.ForEach(handler => handler(new ItemDroppedEventArgs<ContainedT>(ContainedObject)));
                }
            }
        }

        /// <summary>
        /// The awake method from unity, if you override it call base.Awake();
        /// </summary>
        public virtual void Awake()
        {
            Destroyer.Destroyed.AddHandler<ContainedT>(e =>
            {
                e.DestroyedObject.Container?.ItemRemovedHandlers.ForEach(handler => handler(new ItemRemovedEventArgs<ContainedT>(ContainedObject)));
            });
        }
        #endregion

        #region Methods
        private void CheckForItemRemoved(DraggablePickedEventArgs<ContainedT> e)
        {
            ItemRemovedHandlers.ForEach(handler => handler(new ItemRemovedEventArgs<ContainedT>(ContainedObject)));
            ContainedObject = null;
        }
        #endregion
    }
}