using Nalka.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;

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
        public static async Task Destroy<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "") where DestroyedT : UnityObject
        {
            if (DestroyedObject is Transform)
                return;
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

        //ici on gere les components
        public static async Task Destroy(GameObject DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "")
        {
            foreach (Component element in DestroyedObject.GetComponents<Component>())
            {
                await Destroy(element, 0, destroyerPath);
            }
            string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
            await Destroy<GameObject>(DestroyedObject, msDelay, destroyerPath);
        }

        //ici on gere les enfants
        public static async Task Destroy(Transform DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "")
        {
            await Task.Delay(msDelay);
            foreach (Transform item in DestroyedObject)
            {
                await Destroy(item, 0, destroyerPath);
            }
            await Destroy(DestroyedObject.gameObject, 0, destroyerPath);
        }

        [Obsolete("Not ready yet", true)]
        /// <summary>
        /// Destroys the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="UnityEngine.Object"/> to destroy</param>
        /// <param name="msDelay">The number of milliseconds to wait before destroying the given <see cref="UnityEngine.Object"/></param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        public static async Task DestroyImmediate<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath] string destroyerPath = "") where DestroyedT : UnityObject
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
        public static class Destroying
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            public static void AddHandler<DestroyedType>(Action<DestroyingObjectEventArgs<DestroyedType>> method) where DestroyedType : UnityObject
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method.Method))
                {
                    AddedMethods.Add(method.Method);
                    _destroyingObject += (e) =>
                    {
                        if (e.DestroyedObject is DestroyedType && (typeof(DestroyedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(DestroyedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            method((DestroyingObjectEventArgs<DestroyedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void Clear()
            {
                _destroyingObject = null;
                AddedMethods.Clear();
            }
        }

        /// <summary>
        /// Handlers added here will be invoked when after the <see cref="UnityEngine.Object"/>'s destruction
        /// </summary>
        public static class Destroyed
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            public static void AddHandler<DestroyedType>(Action<ObjectDestroyedEventArgs<DestroyedType>> method) where DestroyedType : UnityObject
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method.Method))
                {
                    AddedMethods.Add(method.Method);
                    _objectDestroyed += (e) =>
                    {
                        if (e.DestroyedObject is DestroyedType && (typeof(DestroyedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(DestroyedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            method((ObjectDestroyedEventArgs<DestroyedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void Clear()
            {
                _objectDestroyed = null;
                AddedMethods.Clear();
            }
        }
        #endregion
    }
}