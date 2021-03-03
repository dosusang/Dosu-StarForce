using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce {
	public class OutPutBox : Entity {

        public List<string> outPuts = new List<string>();
        public MainForm form;
        public int formId = 0;
		
        private void OnTriggerEnter2D(Collider2D collision) {
            var obj = collision.gameObject;
            var box = obj.GetComponent<Box>();
            if (box != null) {
                outPuts.Add(box.boxPram);
                RefreshOutPut();
            }
        }

        private void RefreshOutPut() {
            if (form == null) form = (MainForm)GameEntry.UI.GetUIForm(formId).Logic;
            form.UpdateOutPut(GetPrams());
        }
        private string GetPrams() {
            string s = "";
            foreach (var pram in outPuts) {
                s += pram;
            }
            return s;
        }
    }
}
