using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nalka.Tools.Unity
{
    public class DraggableReleasedEventArgs<T> : EventArgs where T : DraggableBase
    {
        public T ReleasedObject { get; private set; }

        public DraggableReleasedEventArgs(T releasedObject)
        {
            ReleasedObject = releasedObject;
        }
    }

    public delegate void ObjectReleasedEventHandler(DraggableReleasedEventArgs<DraggableBase> e);
}