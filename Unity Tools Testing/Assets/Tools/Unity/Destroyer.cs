using Nalka.Tools.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using System;
namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides tools for <see cref="UnityEngine.Object"/>'s destruction
    /// </summary>
    public static class Destroyer
    {
        #region Fields
        private static event DestroyingObjectEventHandler _destroyingObject;
        private static event ObjectDestroyedEventHandler _objectDestroyed;
        #endregion

        #region Properties
        /// <summary>
        /// Determines if the handlers have to be added even if they have already been added
        /// </summary>
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Destroys the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="UnityEngine.Object"/> to destroy</param>
        /// <param name="msDelay">The number of milliseconds to wait before destroying the given <see cref="UnityEngine.Object"/></param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        public static async void Destroy<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "") where DestroyedT : UnityObject
        {
            string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            DestroyingObjectEventArgs<DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            DestroyingObjectEventArgs<UnityObject> SentDestroyingArgs = (DestroyingObjectEventArgs<UnityObject>)DestroyingArgs;
            ObjectDestroyedEventArgs<UnityObject> SentDestroyedArgs = (ObjectDestroyedEventArgs<UnityObject>)DestroyedArgs;
            _destroyingObject?.Invoke(SentDestroyingArgs);
            if (!SentDestroyingArgs.Cancel)
            {
                UnityObject.Destroy(DestroyedObject);
                _objectDestroyed?.Invoke(SentDestroyedArgs);
            }
        }

        [Obsolete("Not ready yet", true)]
        /// <summary>
        /// Destroys the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="UnityEngine.Object"/> to destroy</param>
        /// <param name="msDelay">The number of milliseconds to wait before destroying the given <see cref="UnityEngine.Object"/></param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        public static async void DestroyImmediate<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "") where DestroyedT : UnityObject
        {
            string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
            await Task.Delay(msDelay);
            DestroyingObjectEventArgs<DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject, destroyerName);//might be useless
            DestroyingObjectEventArgs<UnityObject> SentDestroyingArgs = (DestroyingObjectEventArgs<UnityObject>)DestroyingArgs;
            ObjectDestroyedEventArgs<UnityObject> SentDestroyedArgs = (ObjectDestroyedEventArgs<UnityObject>)DestroyedArgs;//might be useless
            _destroyingObject?.Invoke(SentDestroyingArgs);
            if (!SentDestroyingArgs.Cancel)
            {
                UnityObject.DestroyImmediate(DestroyedObject);
            }
        }
        #endregion

        #region Nested Classes (makes things clearer for users)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="UnityEngine.Object"/>'s destruction
        /// </summary>
        public static class Destroying<DestroyedType> where DestroyedType : UnityObject
        {
            private static List<Action<DestroyingObjectEventArgs<DestroyedType>>> AddedMethods = new List<Action<DestroyingObjectEventArgs<DestroyedType>>>();
            public static void AddHandler(Action<DestroyingObjectEventArgs<DestroyedType>> method)
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method))
                {
                    AddedMethods.Add(method);
                    _destroyingObject += (e) =>
                    {
                        if (typeof(DestroyedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(DestroyedType) == e.GetType().GenericTypeArguments[0])
                        {
                            method((DestroyingObjectEventArgs<DestroyedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void RemoveHandler(Action<DestroyingObjectEventArgs<DestroyedType>> method)
            {

            }
        }

        /// <summary>
        /// Handlers added here will be invoked when after the <see cref="UnityEngine.Object"/>'s destruction
        /// </summary>
        public static class Destroyed<DestroyedType> where DestroyedType : UnityObject
        {
            private static List<Action<ObjectDestroyedEventArgs<DestroyedType>>> AddedMethods = new List<Action<ObjectDestroyedEventArgs<DestroyedType>>>();
            public static void AddHandler(Action<ObjectDestroyedEventArgs<DestroyedType>> method)
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method))
                {
                    AddedMethods.Add(method);
                    _objectDestroyed += (e) =>
                    {
                        if (typeof(DestroyedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(DestroyedType) == e.GetType().GenericTypeArguments[0])
                        {
                            method((ObjectDestroyedEventArgs<DestroyedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void RemoveHandler(Action<ObjectDestroyedEventArgs<DestroyedType>> method)
            {

            }
        }
        #endregion
    }

    internal class RefNeeded
    {
        public bool RefCancel { get; set; } = false;
    }
}