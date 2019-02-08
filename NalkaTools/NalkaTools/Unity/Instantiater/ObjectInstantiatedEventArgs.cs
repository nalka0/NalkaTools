using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for handlers added to <see cref="Destroyer.Destroying"/>
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class ObjectInstiatedEventArgs<DestroyedT> : InstiationEventArgsBase<DestroyedT> where DestroyedT : Object
    {
        internal ObjectInstiatedEventArgs(DestroyedT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }
        
        public static explicit operator ObjectInstiatedEventArgs<Object>(ObjectInstiatedEventArgs<DestroyedT> e) => new ObjectInstiatedEventArgs<Object>(e.InstantiatedObject, e.InstantiatingFileName);

        public static explicit operator ObjectInstiatedEventArgs<DestroyedT>(ObjectInstiatedEventArgs<Object> e) => new ObjectInstiatedEventArgs<DestroyedT>((DestroyedT)e.InstantiatedObject, e.InstantiatingFileName);
    }

    /// <summary>
    /// Happens after an object was instantiated by the <see cref="Instantiater"/>
    /// </summary>
    /// <param name="e"></param>
    internal delegate void ObjectInstantiatedEventHandler(ObjectInstiatedEventArgs<Object> e);
}