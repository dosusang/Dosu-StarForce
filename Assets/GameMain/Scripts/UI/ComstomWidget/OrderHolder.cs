using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce {
    public class OrderHolder : MonoBehaviour {

        [SerializeField]
        private int spacing = 150;
        [SerializeField]
        private int upspacing = -100;
        [SerializeField]
        private int axis = 200;

        private int startPos { get { return spacing + upspacing; } }

        private List<BaseOrder> m_order_list = new List<BaseOrder>(); 
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void TryInsert(GameObject obj) {
            var obj_base_order = obj.GetComponent<BaseOrder>();
            var rect = gameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rect, obj.transform.position)) {
                int pos = m_order_list.IndexOf(obj_base_order);
                   obj.transform.DOLocalMove(CaculPos(pos), (float)0.3);
            } else {
                Destroy(obj);
                m_order_list.Remove(obj_base_order);
            }
        }

        public void PreInsert(BaseOrder obj_base_order) {
            obj_base_order.gameObject.transform.SetParent(transform);
            m_order_list.Add(obj_base_order);
        }

        private Vector3 CaculPos(int idx) {
            int y_pos = -(startPos + idx * spacing);
            var temp_pos = Vector3.zero;
            temp_pos.Set(axis, y_pos, 0);
            return temp_pos;
        }

        public void OnTargetOrderMove(Transform taget_t, BaseOrder base_order) {
            var temp_t = taget_t;
            for (int i = 0; i < m_order_list.Count; i++) {
                temp_t = m_order_list[i].gameObject.transform;
                if (temp_t == taget_t) continue;
                if (Mathf.Abs(taget_t.position.y - temp_t.position.y) <= 20) {
                    FormatPosExcept(i, taget_t, base_order);
                    if (Vector3.Distance(taget_t.position, temp_t.position) <= 20) {
                        for (int idx = 0; idx < m_order_list.Count; idx++) {
                            var need_move_t = m_order_list[idx].gameObject.transform;
                            need_move_t.DOLocalMove(CaculPos(idx), (float)0.3);
                        }
                    }
                    return;
                }
            }
        }

        private void FormatPosExcept(int conlict_pos, Transform taget_t, BaseOrder base_order) {
            int ept_pos = m_order_list.IndexOf(base_order);
            OrderMoveDirect direction = OrderMoveDirect.DOWN;
            if (ept_pos < conlict_pos) {
                direction = OrderMoveDirect.UP;
            }
            if (direction == OrderMoveDirect.DOWN) {
                ListExtention.MoveRange(ref m_order_list, conlict_pos, ept_pos, direction);
            } else if (direction == OrderMoveDirect.UP) {
                ListExtention.MoveRange(ref m_order_list, ept_pos, conlict_pos, direction);
            }
        }

    }
}
