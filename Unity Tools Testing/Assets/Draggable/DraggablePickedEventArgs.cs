using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    public class DraggablePickedEventArgs<T> : EventArgs where T : DraggableBase
    {
        public T PickedObject { get; private set; }

        public DraggablePickedEventArgs(T pickedObject)
        {
            PickedObject = pickedObject;
        }
    }

    public delegate void ObjectPickedEventHandler(DraggablePickedEventArgs<DraggableBase> e);
}