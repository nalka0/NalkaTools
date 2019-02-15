using Nalka.Tools.Extensions;
using Nalka.Tools.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace NalkaTools.Unity.EventManagers
{
    /// <summary>
    /// Provides tools for <see cref="UnityEngine.Object"/>'s instantiation
    /// </summary>
    public static class Instantiater
    {
        #region Fields
        private static event InstantiatingObjectEventHandler _instantiatingObject;
        private static event ObjectInstantiatedEventHandler _objectInstantiated;
        #endregion

        #region Properties
        /// <summary>
        /// Determines if the handlers have to be added even if they have already been added.
        /// </summary>
        /// <value><see langword="false"/></value>
        public static bool AllowsMultipleEqualHandler { get; set; } = false;
        #endregion

        #region Methods

        #region Instantiate(UnityEngine.Object original)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject instantiatedGameObject)
                {
                    foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != instantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != instantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
                else
                {
                    if (InstantiatingMethod(instantiatedObject, instantiaterName))
                    {
                        InstantiatedMethod(instantiatedObject, instantiaterName);
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
            }
        }

        private static bool InstantiatingMethod<InstantiatedT>(InstantiatedT instantiatedObject, string instantiaterName) where InstantiatedT : UnityObject
        {
            InstantiatingObjectEventArgs<InstantiatedT> InstantiatingArgs = new InstantiatingObjectEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            InstantiatingObjectEventArgs<UnityObject> SentInstantiatingArgs = (InstantiatingObjectEventArgs<UnityObject>)InstantiatingArgs;
            _instantiatingObject?.Invoke(SentInstantiatingArgs);
            return !SentInstantiatingArgs.Cancel;
        }

        private static void InstantiatedMethod<InstantiatedT>(InstantiatedT instantiatedObject, string instantiaterName) where InstantiatedT : UnityObject
        {
            ObjectInstiatedEventArgs<InstantiatedT> InstantiatedArgs = new ObjectInstiatedEventArgs<InstantiatedT>(instantiatedObject, instantiaterName);
            ObjectInstiatedEventArgs<UnityObject> SentInstantiatedArgs = (ObjectInstiatedEventArgs<UnityObject>)InstantiatedArgs;
            _objectInstantiated?.Invoke(SentInstantiatedArgs);
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given world position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, position, rotation, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject instantiatedGameObject)
                {
                    foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != instantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != instantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                        UnityObject.Instantiate(instantiatedObject, position, rotation);
                    }
                }
                else
                {
                    if (InstantiatingMethod(instantiatedObject, instantiaterName))
                    {
                        InstantiatedMethod(instantiatedObject, instantiaterName);
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
            }
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation, Transform parent)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/> with given local position and rotation and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="position">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="rotation">The <see cref="Space.World"/> position of the <paramref name="instantiatedObject"/></param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Vector3 position, Quaternion rotation, Transform parent, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, position, rotation, parent, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject instantiatedGameObject)
                {
                    foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != instantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != instantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                        UnityObject.Instantiate(instantiatedObject, position, rotation, parent);
                    }
                }
                else
                {
                    if (InstantiatingMethod(instantiatedObject, instantiaterName))
                    {
                        InstantiatedMethod(instantiatedObject, instantiaterName);
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
            }
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Transform parent)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, transform, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject instantiatedGameObject)
                {
                    foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != instantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != instantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                        UnityObject.Instantiate(instantiatedObject, parent);
                    }
                }
                else
                {
                    if (InstantiatingMethod(instantiatedObject, instantiaterName))
                    {
                        InstantiatedMethod(instantiatedObject, instantiaterName);
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
            }
        }
        #endregion

        #region Instantiate(UnityEngine.Object original, Transform parent, bool worldPositionStays)
        /// <summary>
        /// Instantiates the given <see cref="UnityEngine.Object"/>, sets <paramref name="parent"/> as parent and fires related events
        /// </summary>
        /// <param name="instantiatedObject"><see cref="UnityEngine.Object"/> to instantiate</param>
        /// <param name="parent"><see cref="Transform"/> to set as parent of the <paramref name="instantiatedObject"/></param>
        /// <param name="msDelay">The number of milliseconds to wait before instantiating the given <see cref="UnityEngine.Object"/></param>
        /// <param name="worldPositionStays">Determines if the world position has to be kept</param>
        /// <param name="instantiaterPath">This argument is automatically provided, please do not provide it</param>
        public static async Task Instantiate<InstantiatedT>(InstantiatedT instantiatedObject, Transform parent, bool worldPositionStays, int msDelay = 0, [CallerFilePath, DontProvide] string instantiaterPath = "") where InstantiatedT : UnityObject
        {
            bool raiseInstantiated = true;
            if (instantiatedObject is Transform transform)
            {
                await Instantiate(transform.gameObject, transform, worldPositionStays, msDelay, instantiaterPath);
            }
            else
            {
                string instantiaterName = instantiaterPath.Remove(instantiaterPath.Length - 3).Split('\\').Last();
                await Task.Delay(msDelay);
                if (instantiatedObject is GameObject instantiatedGameObject)
                {
                    foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                    {
                        if (element is Transform transfo)
                        {
                            foreach (Transform item in transfo)
                            {
                                if (item.gameObject != instantiatedGameObject)
                                {
                                    if (!InstantiatingMethod(item.gameObject, instantiaterName))
                                    {
                                        raiseInstantiated = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!InstantiatingMethod(element, instantiaterName))
                                raiseInstantiated = false;
                        }
                    }
                    if (raiseInstantiated)
                    {
                        foreach (Component element in instantiatedGameObject.GetComponents<Component>())
                        {
                            if (element is Transform transfo)
                            {
                                foreach (Transform item in transfo)
                                {
                                    if (item.gameObject != instantiatedGameObject)
                                    {
                                        InstantiatedMethod(item.gameObject, instantiaterName);
                                    }
                                }
                            }
                            else
                            {
                                InstantiatedMethod(element, instantiaterName);
                            }
                        }
                        UnityObject.Instantiate(instantiatedObject, parent, worldPositionStays);
                    }
                }
                else
                {
                    if (InstantiatingMethod(instantiatedObject, instantiaterName))
                    {
                        InstantiatedMethod(instantiatedObject, instantiaterName);
                        UnityObject.Instantiate(instantiatedObject);
                    }
                }
            }
        }
        #endregion
        
        #endregion

        #region Nested Classes (makes things clearer for users and may save memory)
        /// <summary>
        /// Handlers added here will be invoked when before the <see cref="UnityEngine.Object"/>'s instantiation
        /// </summary>
        public static class Instantiating
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute before an object of type <typeparamref name="InstantiatedType"/> was instantiated by the <see cref="Instantiater"/>
            /// </summary>
            /// <typeparam name="InstantiatedType">Type of the object's instantiation to listen</typeparam>
            /// <param name="handler">Event listener to be added</param>
            public static void AddHandler<InstantiatedType>(Action<InstantiatingObjectEventArgs<InstantiatedType>> handler) where InstantiatedType : UnityObject
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(handler.Method))
                {
                    AddedMethods.Add(handler.Method);
                    _instantiatingObject += (e) =>
                    {
                        if (e.InstantiatedObject is InstantiatedType && (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            handler((InstantiatingObjectEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            //[Obsolete("Not implemented", true)]
            public static void Clear()
            {
                _instantiatingObject = null;
                AddedMethods.Clear();
            }
        }

        /// <summary>
        /// Handlers added here will be invoked when after the <see cref="UnityEngine.Object"/>'s instantiation
        /// </summary>
        public static class Instantiated
        {
            private static List<MethodInfo> AddedMethods = new List<MethodInfo>();
            /// <summary>
            /// Adds a method to execute after an object of type <typeparamref name="InstantiatedType"/> was instantiated by the <see cref="Instantiater"/>
            /// </summary>
            /// <typeparam name="InstantiatedType">Type of the object's instantiation to listen</typeparam>
            /// <param name="handler">Event handler to be added</param>
            public static void AddHandler<InstantiatedType>(Action<ObjectInstiatedEventArgs<InstantiatedType>> handler) where InstantiatedType : UnityObject
            {
                if (AllowsMultipleEqualHandler || !AddedMethods.Contains(handler.Method))
                {
                    AddedMethods.Add(handler.Method);
                    _objectInstantiated += (e) =>
                    {
                        if (e.InstantiatedObject is InstantiatedType && (typeof(InstantiatedType).Inherits(e.GetType().GenericTypeArguments[0]) || typeof(InstantiatedType) == e.GetType().GenericTypeArguments[0]))
                        {
                            handler((ObjectInstiatedEventArgs<InstantiatedType>)e);
                        }
                    };
                }
            }

            [Obsolete("Not implemented", true)]
            public static void Clear()
            {
                _objectInstantiated = null;
                AddedMethods.Clear();
            }
        }
        #endregion
    }
}