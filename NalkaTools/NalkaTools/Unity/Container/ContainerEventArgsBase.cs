using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Base class for all <see cref="ContainerBase{ContainedT}"/> events's event data
    /// </summary>
    /// <typeparam name="ContainedT"></typeparam>
    public abstract class ContainerEventArgsBase<ContainedT> : EventArgs where ContainedT : DraggableBase<ContainedT>
    {
        /// <summary>
        /// The draggable object the container interacted with
        /// </summary>
        public ContainedT ContainedItem { get; protected set; }

        internal ContainerEventArgsBase(ContainedT containedItem)
        {
            ContainedItem = containedItem;
        }
    }
}
