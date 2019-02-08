using System;
using static UnityEngine.EventSystems.PointerEventData;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data when a <typeparamref name="DraggableT"/> is released
    /// </summary>
    /// <typeparam name="DraggableT">Type of the object being dragged</typeparam>
    public sealed class DraggableReleasedEventArgs<DraggableT> : DraggableEventArgsBase<DraggableT> where DraggableT : DraggableBase<DraggableT>
    {
        /// <summary>
        /// Determines if the dragged object has reached a container
        /// </summary>
        public bool ReachedContainer { get; internal set; }

        internal DraggableReleasedEventArgs(DraggableT releasedObject, InputButton usedButton, bool reachedContainer) : base(releasedObject, usedButton)
        {
            ReachedContainer = reachedContainer;
        }
    }
}