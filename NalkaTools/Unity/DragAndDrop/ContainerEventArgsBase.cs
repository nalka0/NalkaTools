using System;

namespace NalkaTools.Unity.DragAndDrop
{
    /// <summary>
    /// Base class for all <see cref="ContainerBase{ContainedT}"/> events's event data
    /// </summary>
    /// <typeparam name="ContainedT"></typeparam>
    public abstract class ContainerEventArgsBase<ContainedT> : EventArgs where ContainedT : DraggableBase<ContainedT>
    {
        /// <summary>
        /// The draggable object the container interacted with
        /// </summary>
        public ContainedT ContainedItem { get; protected set; }

        internal ContainerEventArgsBase(ContainedT containedItem)
        {
            ContainedItem = containedItem;
        }
    }
}
