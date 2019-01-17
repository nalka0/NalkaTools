using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nalka.Tools.Unity
{
    public class ItemDroppedEventArgs<T> : EventArgs where T: ContainerBase<DraggableBase>
    {
        public T ItemDropped { get; private set; }

        public ItemDroppedEventArgs(T itemDropped)
        {
            ItemDropped = itemDropped;
        }
    }

    public delegate void ItemDroppedEventHandler(ItemDroppedEventArgs<ContainerBase<DraggableBase>> e);
}