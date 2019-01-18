using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nalka.Tools.Unity
{
    public abstract class DraggableBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        #region Fields
        private Vector3 startPoint;
        private float startTime;
        #endregion

        #region Events
        public virtual event ObjectPickedEventHandler Picked;
        public virtual event HoldingObjectEventHandler Holding;
        public virtual event ObjectReleasedEventHandler Released;
        #endregion

        #region Unity methods
        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            startTime = Time.time;
            startPoint = eventData.pointerPressRaycast.worldPosition;
            Picked?.Invoke(new DraggablePickedEventArgs<DraggableBase>(this));
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            Debug.Log($"{transform.position}");
            //Debug.Log($"Camera.main.pixelWidth = {Camera.main.pixelWidth}");
            ////obtenir la différence entre le nombre de pixels sur l'écran (x et y)
            //transform.position += new Vector3(eventData.delta.x, eventData.delta.y, transform.position.z);
            Holding?.Invoke(new DraggableHoldingEventArgs<DraggableBase>(this));
            
            #region osef
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            //if (Physics.Raycast(ray, out RaycastHit hit, 3000))
            //{
            //    //if (hit.collider.gameObject.GetComponent<ContainerBase<DraggableBase>>() != null)
            //    {
            //        Vector3 clickedPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            //        transform.position = Vector3.Lerp(startPoint, clickedPosition, (Time.time - startTime) / 1.0f);
            //    }
            //}
            #endregion
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Released?.Invoke(new DraggableReleasedEventArgs<DraggableBase>(this));
        }
        #endregion
    }
}