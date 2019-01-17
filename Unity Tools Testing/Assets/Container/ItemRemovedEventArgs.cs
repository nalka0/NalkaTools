using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nalka.Tools.Unity
{
    public class ItemRemovedEventArgs<T> : EventArgs where T : ContainerBase<DraggableBase>
    {
        public T RemovedItem { get; private set; }

        public ItemRemovedEventArgs(T removedItem)
        {
            RemovedItem = removedItem;
        }
    }

    public delegate void ItemRemovedEventHandler(ItemRemovedEventArgs<ContainerBase<DraggableBase>> e);
}