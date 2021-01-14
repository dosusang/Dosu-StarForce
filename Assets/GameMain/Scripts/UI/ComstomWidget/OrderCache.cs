using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce {
    public class OrderCache : MonoBehaviour {
        public MoveableTar moveing_tar;
        public int distance = 200;
        [SerializeField]
        private List<MoveableTar> code_list;


        // Update is called once per frame
        void Update() {
            if (code_list.Count != 0 && moveing_tar != null) {
                
            }
        }

        private void ReadyInset() {

        }
    }
}

