using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StarForce {
    public class BaseOrder : DragableObj, IBeginDragHandler, IEndDragHandler, IComparable<BaseOrder>{
        public OrderHolder tarRoot;
        public Vector3 holderPos = Vector3.zero; 
        public OrderStatus m_status = OrderStatus.IS_BASE;

        public void OnBeginDrag(PointerEventData eventData) {
            if (m_status != OrderStatus.IS_BASE) return;
            var obj_clone = Instantiate(gameObject, transform.position, transform.rotation);
            obj_clone.transform.SetParent(transform.parent);
            m_status = OrderStatus.IN_AIR;

            if (tarRoot != null) {
                tarRoot.PreInsert(this);
                m_status = OrderStatus.IN_HOLDER;
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (tarRoot != null) {
                tarRoot.TryInsert(gameObject);
                m_status = OrderStatus.IN_HOLDER;
            }
        }

        override
        public void OnDrag(PointerEventData eventData) {
            base.OnDrag(eventData);
            if (tarRoot != null) {
                tarRoot.FormatPos(this);
            }
        }
        public void OnExcute(System.Object userData) {

        }

        public int CompareTo(BaseOrder other) {
            return (int)-(this.transform.position.y - other.transform.position.y);
        }
    }

}
