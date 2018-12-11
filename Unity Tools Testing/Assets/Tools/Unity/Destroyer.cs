using Nalka.Tools.Extensions;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides tools for <see cref="Object"/>'s destruction
    /// </summary>
    public static class Destroyer
    {
        #region Fields
        private static event DestroyingObjectEventHandler _destroyingObject;
        private static event ObjectDestroyedEventHandler _objectDestroyed;
        #endregion

        #region Events
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
        #endregion

        #region Properties
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
        #endregion

        #region Nested Classes (makes things clearer for users)
        public static class Destroying
        {
            public static void AddHandler<U>(System.Action<DestroyingObjectEventArgs<U>> handler) where U : Object
            {
                DestroyingObject += (e) =>
                {
                    if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                    {
                        handler((DestroyingObjectEventArgs<U>)e);
                    }
                };
            }
        }
        public static class Destroyed
        {
            public static void AddHandler<U>(System.Action<ObjectDestroyedEventArgs<U>> handler) where U : Object
            {
                ObjectDestroyed += (e) =>
                {
                    if (typeof(U).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(U) == e.GetType().GenericTypeArguments[0])
                    {
                        handler((ObjectDestroyedEventArgs<U>)e);
                    }
                };
            }
        }
        #endregion
    }

    internal class RefNeeded
    {
        public bool RefCancel { get; set; } = false;
    }
}