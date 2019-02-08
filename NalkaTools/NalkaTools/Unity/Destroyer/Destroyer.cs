using Nalka.Tools.Extensions;
using Nalka.Tools.Internal;
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
    /// Provides tools for <see cref="UnityObject"/>'s destruction
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
        public static async Task Destroy<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath, DontProvide] string destroyerPath = "") where DestroyedT : UnityObject
        {
            bool raiseDestroyed = true;
            if (DestroyedObject is Transform transform)
            {
                await Destroy(transform.gameObject, msDelay, destroyerPath);
            }
            else
            {
                string destroyerName = destroyerPath.Remove(destroyerPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (DestroyedObject is GameObject DestroyedGameObject)
                {
                    foreach (Component element in DestroyedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != DestroyedGameObject)
                                {
                                    if (!DestroyingMethod(item.gameObject, destroyerName))
                                    {
                                        raiseDestroyed = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!DestroyingMethod(element, destroyerName))
                                raiseDestroyed = false;
                        }
                    }
                    if (raiseDestroyed)
                    {
                        foreach (Component element in DestroyedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != DestroyedGameObject)
                                    {
                                        DestroyedMethod(item.gameObject, destroyerName);
                                    }
                                }
                            }
                            else
                            {
                                DestroyedMethod(element, destroyerName);
                            }
                        }
                    }
                }
            }
        }

        private static bool DestroyingMethod<DestroyedT>(DestroyedT DestroyedObject, string destroyerName) where DestroyedT : UnityObject
        {
            DestroyingObjectEventArgs<DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            DestroyingObjectEventArgs<UnityObject> SentDestroyingArgs = (DestroyingObjectEventArgs<UnityObject>)DestroyingArgs;
            _destroyingObject?.Invoke(SentDestroyingArgs);
            return !SentDestroyingArgs.Cancel;
        }

        private static void DestroyedMethod<DestroyedT>(DestroyedT DestroyedObject, string destroyerName) where DestroyedT : UnityObject
        {
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject, destroyerName);
            ObjectDestroyedEventArgs<UnityObject> SentDestroyedArgs = (ObjectDestroyedEventArgs<UnityObject>)DestroyedArgs;
            UnityObject.Destroy(DestroyedObject);
            _objectDestroyed?.Invoke(SentDestroyedArgs);
        }
      
        /// <summary>
        /// Destroys the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="UnityEngine.Object"/> to destroy</param>
        /// <param name="msDelay">The number of milliseconds to wait before destroying the given <see cref="UnityEngine.Object"/></param>
        /// <param name="destroyerPath">This argument is automatically provided, please do not provide it</param>
        [Obsolete("Not ready yet", true)]
        public static async Task DestroyImmediate<DestroyedT>(DestroyedT DestroyedObject, int msDelay = 0, [CallerFilePath, DontProvide] string destroyerPath = "") where DestroyedT : UnityObject
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

        #region Nested Classes (makes things clearer for users and may save memory)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="UnityObject"/>'s destruction
        /// </summary>
        public static class Destroying
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute before an object of type <typeparamref name="DestroyedType"/> or any derived class was destroyed by the <see cref="Destroyer"/>  
            /// </summary>
            /// <typeparam name="DestroyedType">Type of the object's destruction to listen</typeparam>
            /// <param name="method">Event listener to be added</param>
            public static void AddHandler<DestroyedType>(Action<DestroyingObjectEventArgs<DestroyedType>> method) where DestroyedType : UnityObject
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(method.Method))
                {
                    AddedMethods.Add(method.Method);
                    _destroyingObject += e =>
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
        /// Handlers added here will be invoked when after a <see cref="UnityObject"/>'s destruction
        /// </summary>
        public static class Destroyed
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute after an object of type <typeparamref name="DestroyedType"/> or any derived class was destroyed by the <see cref="Destroyer"/>  
            /// </summary>
            /// <typeparam name="DestroyedType">Type of the object's destruction to listen</typeparam>
            /// <param name="method">Event listener to be added</param>
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
