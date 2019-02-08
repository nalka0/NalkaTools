using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for handlers added to <see cref="Destroyer.Destroyed"/>
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

    internal delegate void ObjectDestroyedEventHandler(ObjectDestroyedEventArgs<Object> e);
}