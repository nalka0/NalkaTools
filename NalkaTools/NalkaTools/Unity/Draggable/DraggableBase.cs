using Nalka.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

namespace Nalka.Tools.Unity
{
    /// <summary>
    /// Base class for all objects that can be dragged and dropped
    /// </summary>
    public abstract class DraggableBase<DraggableT> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where DraggableT : DraggableBase<DraggableT>
    {
        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// The container that contains this draggable
        /// </summary>
        public ContainerBase<DraggableT> Container { get; internal set; }

        /// <summary>
        /// The <see cref="InputButton"/> that can activate drag and drop
        /// </summary>
        public IEnumerable<InputButton> AllowedButtons { get; set; } = new InputButton[] { InputButton.Left, InputButton.Middle, InputButton.Right };
        #endregion

        #region Events
        /// <summary>
        /// Contains the methods that will be executed when this draggable is picked
        /// </summary>
        public List<Action<DraggablePickedEventArgs<DraggableT>>> PickedHandlers { get; } = new List<Action<DraggablePickedEventArgs<DraggableT>>>();
        
        /// <summary>
        /// Contains the methods that will be executed when this draggable is held
        /// </summary>
        public List<Action<DraggableHoldingEventArgs<DraggableT>>> HoldingHandlers { get; } = new List<Action<DraggableHoldingEventArgs<DraggableT>>>();
        
        /// <summary>
        /// Contains the methods that will be executed when this draggable is released
        /// </summary>
        public List<Action<DraggableReleasedEventArgs<DraggableT>>> ReleasedHandlers { get; } = new List<Action<DraggableReleasedEventArgs<DraggableT>>>();
        #endregion

        #region Unity methods
        /// <summary>
        /// Raised by unity
        /// </summary>
        /// <param name="eventData">Provided by unity</param>
        [Obsolete("Only Unity should invoke this method", true)]
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (AllowedButtons?.Contains(eventData.button) ?? false)
            {
                Container = null;
                PickedHandlers.ForEach(handler => handler(new DraggablePickedEventArgs<DraggableT>((DraggableT)this, eventData.button)));
            }
        }

        /// <summary>
        /// Raised by unity
        /// </summary>
        /// <param name="eventData">Provided by unity</param>
        [Obsolete("Only Unity should invoke this method", true)]
        public void OnDrag(PointerEventData eventData)
        {
            if (AllowedButtons?.Contains(eventData.button) ?? false)
            {
                if (Camera.main.orthographic)
                {
                    transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
                }
                else
                {
                    Vector3 screenPosition = Input.mousePosition;
                    // Track the current depth of the object in front of the camera.
                    // (You could also set this to some fixed constant or stored variable)
                    screenPosition.z = Vector3.Dot(transform.position - Camera.main.transform.position, Camera.main.transform.forward);
                    // Transform this screen position to a world position, 
                    // using the camera's transformation & projection/viewport settings.
                    // Snap the object to this position.
                    transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
                }
                HoldingHandlers.ForEach(handler => handler(new DraggableHoldingEventArgs<DraggableT>((DraggableT)this, eventData.button)));
            }
        }

        /// <summary>
        /// Raised by unity
        /// </summary>
        /// <param name="eventData">Provided by unity</param>
        [Obsolete("Only Unity should invoke this method", true)]
        public void OnEndDrag(PointerEventData eventData)
        {
            if (AllowedButtons?.Contains(eventData.button) ?? false)
            {
                ReleasedHandlers.ForEach(handler => handler(new DraggableReleasedEventArgs<DraggableT>((DraggableT)this, eventData.button, Container != null)));
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}