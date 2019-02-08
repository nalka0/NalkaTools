using Nalka.Tools.Extensions;
using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data when an <see cref="InstantiatingObjectEventHandler"/> is raised
    /// </summary>
    /// <typeparam name="InstantiatingT">Type of the instantiated <see cref="Object"/></typeparam>
    public sealed class InstantiatingObjectEventArgs<InstantiatingT> : InstiationEventArgsBase<InstantiatingT> where InstantiatingT : Object
    {
        private RefNeeded trick;

        /// <summary>
        /// Determines if the <see cref="Object"/>'s instantiation has to be cancelled
        /// </summary>
        public bool Cancel
        {
            get => trick.RefCancel;
            set => trick.RefCancel = value;
        }

        private InstantiatingObjectEventArgs(InstantiatingT instantiatingObject, string instantiatingFileName, RefNeeded refNeeded) : this(instantiatingObject, instantiatingFileName)
        {
            trick = refNeeded;
        }

        internal InstantiatingObjectEventArgs(InstantiatingT instantiatedObject, string instantiatingFileName) : base(instantiatedObject, instantiatingFileName)
        {
            trick = new RefNeeded();
        }

        public static explicit operator InstantiatingObjectEventArgs<InstantiatingT>(InstantiatingObjectEventArgs<Object> e) => new InstantiatingObjectEventArgs<InstantiatingT>((InstantiatingT)e.InstantiatedObject, e.InstantiatingFileName, e.trick);

        public static explicit operator InstantiatingObjectEventArgs<Object>(InstantiatingObjectEventArgs<InstantiatingT> e) => new InstantiatingObjectEventArgs<Object>(e.InstantiatedObject, e.InstantiatingFileName, e.trick);
    }

    internal delegate void InstantiatingObjectEventHandler(InstantiatingObjectEventArgs<Object> e);
}
