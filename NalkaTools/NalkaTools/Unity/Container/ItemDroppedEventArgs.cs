using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data when an item is dropped into a <see cref="ContainerBase{ContainedT}"/>
    /// </summary>
    /// <typeparam name="ContainedT">Type of the contained draggable</typeparam>
    public sealed class ItemDroppedEventArgs<ContainedT> : ContainerEventArgsBase<ContainedT> where ContainedT : DraggableBase<ContainedT>
    {
        internal ItemDroppedEventArgs(ContainedT itemDropped) : base(itemDropped)
        {

        }
    }
}