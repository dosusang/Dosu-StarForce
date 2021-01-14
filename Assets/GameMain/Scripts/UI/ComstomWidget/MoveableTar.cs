using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StarForce {
    public class MoveableTar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
        private GameObject m_self_copy;
        [SerializeField]
        private GameObject m_tar_root;
        [SerializeField]
        private int distance = 50;
        public MoveStatus status = MoveStatus.IS_BASE;

        public enum MoveStatus {
            IS_BASE = 0,
            MOVEING = 1,
            IN_TAR = 2,
        }
        public void OnPointerDown(PointerEventData eventData) {
            if (status != 0) return;
            m_self_copy = Instantiate(gameObject, transform);
            m_self_copy.GetComponent<MoveableTar>().status = MoveStatus.MOVEING;
        }

        // Start is called before the first frame update
        public void OnPointerUp(PointerEventData eventData) {
            if (status != 0) return;
            if (Vector3.Distance(m_self_copy.transform.position, m_tar_root.transform.position) <= distance) {
                m_self_copy.transform.SetParent(m_tar_root.transform);
                m_self_copy.GetComponent<MoveableTar>().status = MoveStatus.IN_TAR;
            } else {
                m_self_copy.transform.DOMove(transform.position, (float)0.3).onComplete = ()=>{ GameObject.Destroy(m_self_copy);};
            }
        }

        private void Update() {
            if (m_self_copy != null) {
                if (m_self_copy.GetComponent<MoveableTar>().status != MoveStatus.MOVEING) return;
                m_self_copy.transform.position = Input.mousePosition;
            }
        }
    }
}
