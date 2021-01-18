using UnityEngine;
using UnityEngine.EventSystems;

namespace StarForce {
    /// <summary>
    /// 可拖动UI
    /// </summary>
    public class DragableObj : MonoBehaviour, IDragHandler {
        private RectTransform rectTransform;


        // Start is called before the first frame update
        void Start() {
            rectTransform = transform as RectTransform;
        }
        public void OnDrag(PointerEventData eventData) {
            var globalMousePos = Vector3.zero;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos)) {
                rectTransform.position = globalMousePos;
                rectTransform.rotation = rectTransform.rotation;
            }
        }
    }

}
