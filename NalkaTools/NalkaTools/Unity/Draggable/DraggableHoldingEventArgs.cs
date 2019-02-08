using System;
using static UnityEngine.EventSystems.PointerEventData;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data every time a <typeparamref name="DraggableT"/> is dragged
    /// </summary>
    /// <typeparam name="DraggableT">Type of the object being dragged</typeparam>
    public sealed class DraggableHoldingEventArgs<DraggableT> : DraggableEventArgsBase<DraggableT> where DraggableT : DraggableBase<DraggableT>
    {
        internal DraggableHoldingEventArgs(DraggableT holdingObject, InputButton usedButton) : base(holdingObject, usedButton)
        {
        }
    }
}
