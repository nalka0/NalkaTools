using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for <see cref="Destroyer{DestroyedT}.ObjectDestroyed"/>
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class ObjectDestroyedEventArgs<DestroyedT> : DestrcutionEventArgsBase<DestroyedT> where DestroyedT : Object
    {
        internal ObjectDestroyedEventArgs(DestroyedT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }

        public static explicit operator ObjectDestroyedEventArgs<Object>(ObjectDestroyedEventArgs<DestroyedT> e) => new ObjectDestroyedEventArgs<Object>(e.DestroyedObject, e.DestroyingFileName);
        public static explicit operator ObjectDestroyedEventArgs<DestroyedT>(ObjectDestroyedEventArgs<Object> e) => new ObjectDestroyedEventArgs<DestroyedT>((DestroyedT)e.DestroyedObject, e.DestroyingFileName);
    }

    public delegate void ObjectDestroyedEventHandler(ObjectDestroyedEventArgs<Object> e);
}