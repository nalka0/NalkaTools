using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    public class DraggableHoldingEventArgs<T> : EventArgs where T : DraggableBase
    {
        public T HoldingObject { get; private set; }

        public DraggableHoldingEventArgs(T holdingObject)
        {
            HoldingObject = holdingObject;
        }
    }

    public delegate void HoldingObjectEventHandler(DraggableHoldingEventArgs<DraggableBase> e);
}
