using Nalka.Tools.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides tools for <see cref="Object"/>'s destruction
    /// </summary>
    /// <typeparam name="DestroyedT"></typeparam>
    public static class Destroyer
    {
        #region Fields
        private static event DestroyingObjectEventHandler _destroyingObject;
        private static event ObjectDestroyedEventHandler _objectDestroyed;
        #endregion

        private static event DestroyingObjectEventHandler DestroyingObject
        {
            add
            {
                if (AllowsMultipleEqualHandler || _destroyingObject == null || !_destroyingObject.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _destroyingObject += value;
                }
            }
            remove { _destroyingObject -= value; }
        }

        private static event ObjectDestroyedEventHandler ObjectDestroyed
        {
            add
            {
                if (AllowsMultipleEqualHandler || _objectDestroyed == null || !_objectDestroyed.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _objectDestroyed += value;
                }
            }
            remove { _objectDestroyed -= value; }
        }

        #region Propriétés
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Destroys the given <see cref="Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="Object"/> to destroy</param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        public static void Destroy<DestroyedT>(DestroyedT DestroyedObject, [CallerFilePath] string destroyerPath = "") where DestroyedT : Object
        {
            string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
            DestroyingObjectEventArgs<DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            DestroyingObjectEventArgs<Object> SentDestroyingArgs = (DestroyingObjectEventArgs<Object>)DestroyingArgs;
            ObjectDestroyedEventArgs<Object> SentDestroyedArgs = (ObjectDestroyedEventArgs<Object>)DestroyedArgs;
            _destroyingObject?.Invoke(SentDestroyingArgs);
            if (!SentDestroyingArgs.Cancel)
            {
                Object.Destroy(DestroyedObject);
                _objectDestroyed?.Invoke(SentDestroyedArgs);
            }
        }

        public static void AddHandler<U>(System.Action<DestroyingObjectEventArgs<U>> handler) where U : Object
        {
            DestroyingObject += (e) =>
            {
                if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                {
                    handler(e as DestroyingObjectEventArgs<U>);
                }
            };
        }

        //private static void TryRaise<U>(DestroyingObjectEventArgs<Object> e, System.Action<DestroyingObjectEventArgs<U>> handler) where U : Object
        //{
        //    if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
        //    {
        //        handler(e as DestroyingObjectEventArgs<U>);
        //    }
        //}

        public static void AddHandler<U>(System.Action<ObjectDestroyedEventArgs<U>> handler) where U : Object
        {
            ObjectDestroyed += (e) =>
            {
                if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                {
                    handler(e as ObjectDestroyedEventArgs<U>);
                }
            };
        }
        #endregion
    }
    /// <summary>
    /// Provides event data for all <see cref="Object"/> destructions
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the <see cref="Object"/> that has been destroyed</typeparam>
    public abstract class ObjectDestrcutionEventArgsBase<DestroyedT> : System.EventArgs where DestroyedT : Object
    {
        //Tout ce qui est commun à Destroyed et Destroying
        /// <summary>
        /// The <see cref="Object"/> being destroyed
        /// </summary>
        public DestroyedT DestroyedObject { get; private set; }
        /// <summary>
        /// Name of the file that destroyed the <see cref="Object"/>
        /// </summary>
        public string DestroyingFileName { get; private set; }

        protected internal ObjectDestrcutionEventArgsBase(DestroyedT Obj, string destroyingFileName)
        {
            DestroyingFileName = destroyingFileName;
            DestroyedObject = Obj;
        }
    }

    /// <summary>
    /// Provides event data for <see cref="Destroyer{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="DestroyedT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class ObjectDestroyedEventArgs<DestroyedT> : ObjectDestrcutionEventArgsBase<DestroyedT> where DestroyedT : Object
    {
        //Tout ce qui est spécifique à Destroyed
        internal ObjectDestroyedEventArgs(DestroyedT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }

        public static explicit operator ObjectDestroyedEventArgs<Object>(ObjectDestroyedEventArgs<DestroyedT> e)
        {
            return new ObjectDestroyedEventArgs<Object>(e.DestroyedObject, e.DestroyingFileName);
        }
    }

    /// <summary>
    /// Provides event data for <see cref="Destroyer{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="DestroyingT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class DestroyingObjectEventArgs<DestroyingT> : ObjectDestrcutionEventArgsBase<DestroyingT> where DestroyingT : Object
    {
        //Tout ce qui est spécifique à Destroying se trouve ici

        /// <summary>
        /// Determines if the <see cref="Object"/>'s destruction has to be cancelled
        /// </summary>
        public bool Cancel { get; set; } = false;

        internal DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {

        }

        public static explicit operator DestroyingObjectEventArgs<Object>(DestroyingObjectEventArgs<DestroyingT> e)
        {
            return new DestroyingObjectEventArgs<Object>(e.DestroyedObject, e.DestroyingFileName);
        }
    }

    public delegate void ObjectDestroyedEventHandler(ObjectDestroyedEventArgs<Object> e);
    public delegate void DestroyingObjectEventHandler(DestroyingObjectEventArgs<Object> e);
}