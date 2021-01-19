using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarForce {
    public class Robot : Entity {
        public float step = (float)0.02;
        private Dirct m_nowdirct = Dirct.None;
        private float distance = 0;
        private enum Dirct {
            None = 0,
            W = 1,
            S = 2,
            A = 3,
            D = 4,
        }
        // Update is called once per frame
        void Update() {
            GetInput();
        }

        private void FixedUpdate() {
            if (m_nowdirct != Dirct.None) {
                distance -= step;
                if (distance > 0) {
                    MoveAGrid(m_nowdirct);
                } else {
                    m_nowdirct = Dirct.None;
                    var now_pos = transform.localPosition;
                    now_pos.Set(Mathf.Round(now_pos.x), Mathf.Round(now_pos.y), Mathf.Round(now_pos.z));
                    transform.localPosition = now_pos;
                }
            }
        }

        void MoveAGrid(Dirct d) {
            var now_pos = transform.localPosition;
            if (d == Dirct.W) {
                now_pos.Set(now_pos.x, now_pos.y + step, now_pos.z);
            }
            if (d == Dirct.S) {
                now_pos.Set(now_pos.x, now_pos.y - step, now_pos.z);
            }
            if (d == Dirct.A) {
                now_pos.Set(now_pos.x - step, now_pos.y, now_pos.z);
            }
            if (d == Dirct.D) {
                now_pos.Set(now_pos.x + step, now_pos.y, now_pos.z);
            }
            transform.localPosition = now_pos;
        }

        void GetInput() {
            if (m_nowdirct != Dirct.None) return;
            if (Input.GetKey(KeyCode.W)) {
                m_nowdirct = Dirct.W;
                distance = 1;
            } else if (Input.GetKey(KeyCode.S)) {
                m_nowdirct = Dirct.S;
                distance = 1;
            } else if (Input.GetKey(KeyCode.A)) {
                m_nowdirct = Dirct.A;
                distance = 1;
            } else if (Input.GetKey(KeyCode.D)) {
                m_nowdirct = Dirct.D;
                distance = 1;
            }
        }

    }

}
