﻿namespace NalkaTools.Unity.DragAndDrop
{
    /// <summary>
    /// Provides event data when an item is removed from a <see cref="ContainerBase{ContainedT}"/>
    /// </summary>
    /// <typeparam name="ContainedT">Type of the contained object</typeparam>
    public sealed class ItemRemovedEventArgs<ContainedT> : ContainerEventArgsBase<ContainedT> where ContainedT : DraggableBase<ContainedT>
    {
        internal ItemRemovedEventArgs(ContainedT removedItem):base(removedItem)
        {
        }
    }
}