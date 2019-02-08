using UnityEngine;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Provides event data for all <see cref="Object"/> instantiations
    /// </summary>
    /// <typeparam name="InstantiatedT">Type of the <see cref="Object"/> that has been instantiated</typeparam>
    public abstract class InstiationEventArgsBase<InstantiatedT> : System.EventArgs where InstantiatedT : Object
    {
        /// <summary>
        /// The <see cref="Object"/> being instantiated
        /// </summary>
        public InstantiatedT InstantiatedObject { get; private set; }
        /// <summary>
        /// Name of the file that instantiated the <see cref="Object"/>
        /// </summary>
        public string InstantiatingFileName { get; private set; }
        
        internal InstiationEventArgsBase(InstantiatedT Obj, string instantiatingFileName)
        {
            InstantiatingFileName = instantiatingFileName;
            InstantiatedObject = Obj;
        }
    }
}