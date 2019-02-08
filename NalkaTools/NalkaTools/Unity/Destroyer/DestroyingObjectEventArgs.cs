using Nalka.Tools.Extensions;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data when a <see cref="DestroyingObjectEventHandler"/> is raised
    /// </summary>
    /// <typeparam name="DestroyingT">Type of the destroyed <see cref="Object"/></typeparam>
    public sealed class DestroyingObjectEventArgs<DestroyingT> : DestrcutionEventArgsBase<DestroyingT> where DestroyingT : Object
    {
        private RefNeeded trick;

        /// <summary>
        /// Determines if the <see cref="Object"/>'s destruction has to be cancelled
        /// </summary>
        public bool Cancel
        {
            get => trick.RefCancel;
            set => trick.RefCancel = value;
        }

        private DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName, RefNeeded refNeeded) : this(destroyedObject, destroyingFileName)
        {
            trick = refNeeded;
        }

        internal DestroyingObjectEventArgs(DestroyingT destroyedObject, string destroyingFileName) : base(destroyedObject, destroyingFileName)
        {
            trick = new RefNeeded();
        }

        public static explicit operator DestroyingObjectEventArgs<DestroyingT>(DestroyingObjectEventArgs<Object> e)
        {
            return new DestroyingObjectEventArgs<DestroyingT>((DestroyingT)e.DestroyedObject, e.DestroyingFileName, e.trick);
        }

        public static explicit operator DestroyingObjectEventArgs<Object>(DestroyingObjectEventArgs<DestroyingT> e)
        {
            return new DestroyingObjectEventArgs<Object>(e.DestroyedObject, e.DestroyingFileName, e.trick);
        }
    }

    internal delegate void DestroyingObjectEventHandler(DestroyingObjectEventArgs<Object> e);
}