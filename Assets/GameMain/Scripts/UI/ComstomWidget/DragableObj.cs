using UnityEngine;
using UnityEngine.EventSystems;

namespace StarForce {
    /// <summary>
    /// 可拖动UI
    /// </summary>
    public class DragableObj : MonoBehaviour, IDragHandler {
        private RectTransform rectTransform;
        void Start() {
            rectTransform = transform as RectTransform;
        }
        public void OnDrag(PointerEventData eventData) {
            var delta = eventData.delta;
            var temp_pos = rectTransform.position;
            temp_pos.Set(temp_pos.x + delta.x, temp_pos.y + delta.y, temp_pos.z);
            rectTransform.position = temp_pos;
        }
    }

}
