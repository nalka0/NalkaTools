﻿using System;
using static UnityEngine.EventSystems.PointerEventData;

namespace NalkaTools.Unity.DragAndDrop
{
    /// <summary>
    /// Base class for all <see cref="DraggableBase{DraggableT}"/> events's event data
    /// </summary>
    /// <typeparam name="DraggableT">Type of the draggable object</typeparam>
    public abstract class DraggableEventArgsBase<DraggableT> : EventArgs where DraggableT : DraggableBase<DraggableT>
    {
        /// <summary>
        /// The draggable object being moved
        /// </summary>
        public DraggableT MovedObject { get; protected set; }

        /// <summary>
        /// The button used to drag
        /// </summary>
        public InputButton UsedButton { get; protected set; }

        internal DraggableEventArgsBase(DraggableT movedObject, InputButton button)
        {
            MovedObject = movedObject;
            UsedButton = button;
        }
    }
}
