using UnityEngine;
using System.Linq;

namespace Nalka.Tools.Unity
{
    public static class EncapsulerDestruction<DestroyedT> where DestroyedT : Object
    {
        #region Fields
        private static event ObjectDestroyingEventHandler<DestroyedT> _destroyingObject;
        private static event ObjectDestroyedEventHandler<DestroyedT> _objectDestroyed;
        #endregion

        #region Properties
        /// <summary>
        /// Happens before an object is being destroyed, if <see cref="AllowsMultipleEqualHandler"/> is false a same method will be invoked once only
        /// </summary>
        public static event ObjectDestroyingEventHandler<DestroyedT> DestroyingObject
        {
            add
            {
                if (AllowsMultipleEqualHandler || _destroyingObject == null || !_destroyingObject.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _destroyingObject += value;
                }
            }
            remove
            {
                _destroyingObject -= value;
            }
        }

        /// <summary>
        /// Happens when an object is destroyed, if <see cref="AllowsMultipleEqualHandler"/> is false a same method will be invoked once only
        /// </summary>
        public static event ObjectDestroyedEventHandler<DestroyedT> ObjectDestroyed
        {
            add
            {
                if (AllowsMultipleEqualHandler || _objectDestroyed == null || !_objectDestroyed.GetInvocationList().Any(element => element.Method == value.Method))
                {
                    _objectDestroyed += value;
                }
            }
            remove
            {
                _objectDestroyed -= value;
            }
        }

        /// <summary>
        /// Determines if <see cref="ObjectDestroyed"/> and <see cref="DestroyingObject"/>
        /// <para/>can contain the same method in their invoation list
        /// </summary>
        public static bool AllowsMultipleEqualHandler { private get; set; } = false;
        #endregion

        #region Methods
        /// <summary>
        /// Destroys the given <see cref="Object"/> and fires related events
        /// </summary>
        /// <param name="DestroyedObject"><see cref="Object"/> to destroy</param>
        public static void DestroyObject(DestroyedT DestroyedObject)
        {
            DestroyingObjectEventArgs<DestroyedT> DestroyingArgs = new DestroyingObjectEventArgs<DestroyedT>(DestroyedObject);
            ObjectDestroyedEventArgs<DestroyedT> DestroyedArgs = new ObjectDestroyedEventArgs<DestroyedT>(DestroyedObject);
            _destroyingObject?.Invoke(DestroyingArgs);
            if (!DestroyingArgs.Cancel)
            {
                Object.Destroy(DestroyedObject);
                _objectDestroyed?.Invoke(DestroyedArgs);
            }
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
        public DestroyedT DestroyedObject;
        
        protected internal ObjectDestrcutionEventArgsBase(DestroyedT Obj)
        {
            DestroyedObject = Obj;
        }
    }

    /// <summary>
    /// Provides event data for <see cref="EncapsulerDestruction{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="T">Type of the destroyed <see cref="Object"/></typeparam>
    public class ObjectDestroyedEventArgs<T> : ObjectDestrcutionEventArgsBase<T> where T : Object
    {
        //Tout ce qui est spécifique à Destroyed
        internal ObjectDestroyedEventArgs(T destroyedObject) : base(destroyedObject)
        {

        }
    }

    /// <summary>
    /// Provides event data for <see cref="EncapsulerDestruction{DestroyedT}.DestroyingObject"/>
    /// </summary>
    /// <typeparam name="DestroyingT">Type of the destroyed <see cref="Object"/></typeparam>
    public class DestroyingObjectEventArgs<DestroyingT> : ObjectDestrcutionEventArgsBase<DestroyingT> where DestroyingT : Object
    {
        //Tout ce qui est spécifique à Destroying se trouve ici

        /// <summary>
        /// Allows to cancel the <see cref="Object"/>'s destruction
        /// </summary>
        public bool Cancel = false;

        internal DestroyingObjectEventArgs(DestroyingT destroyedObject) : base(destroyedObject)
        {

        }
    }

    public delegate void ObjectDestroyedEventHandler<T>(ObjectDestroyedEventArgs<T> e) where T : Object;
    public delegate void ObjectDestroyingEventHandler<T>(DestroyingObjectEventArgs<T> e) where T : Object;
}