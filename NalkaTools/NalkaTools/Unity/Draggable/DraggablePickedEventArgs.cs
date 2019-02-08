using System;
using static UnityEngine.EventSystems.PointerEventData;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data when a <typeparamref name="DraggableT"/> is picked
    /// </summary>
    /// <typeparam name="DraggableT">Type of the object being dragged</typeparam>
    public sealed class DraggablePickedEventArgs<DraggableT> : DraggableEventArgsBase<DraggableT> where DraggableT : DraggableBase<DraggableT>
    {
        internal DraggablePickedEventArgs(DraggableT pickedObject, InputButton usedButton) : base(pickedObject, usedButton)
        {
        }
    }
}