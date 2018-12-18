using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for <see cref="Destroyer{DestroyedT}.ObjectDestroyed"/>
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the destroyed <see cref="Object"/></typeparam>
    public class ObjectInstiatedEventArgs<DestroyedT> : InstiationEventArgsBase<DestroyedT> where DestroyedT : Object
    {
        internal ObjectInstiatedEventArgs(DestroyedT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }

        public static explicit operator ObjectInstiatedEventArgs<Object>(ObjectInstiatedEventArgs<DestroyedT> e) => new ObjectInstiatedEventArgs<Object>(e.InstantiatedObject, e.InstantiatingFileName);
        public static explicit operator ObjectInstiatedEventArgs<DestroyedT>(ObjectInstiatedEventArgs<Object> e) => new ObjectInstiatedEventArgs<DestroyedT>((DestroyedT)e.InstantiatedObject, e.InstantiatingFileName);
    }

    internal delegate void ObjectInstantiatedEventHandler(ObjectInstiatedEventArgs<Object> e);
}