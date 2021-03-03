using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
namespace StarForce {
    public class Box : Entity {
        // Start is called before the first frame update
        private TextMeshPro tmp;
        public string boxPram  { get { return tmp.text; } set { tmp.text = value; } }
        void Start() {
            tmp = transform.GetComponentInChildren<TextMeshPro>();
        }


    }
}
