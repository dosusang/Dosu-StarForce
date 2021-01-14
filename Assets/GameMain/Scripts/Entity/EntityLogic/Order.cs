using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;

namespace StarForce {
    public class Order : Entity, IPointerDownHandler, IPointerUpHandler {
        public void OnPointerDown(PointerEventData eventData) {
            Log.Info("ODWN");
        }

        public void OnPointerUp(PointerEventData eventData) {
            Log.Info("UP");
        }
    }

}
