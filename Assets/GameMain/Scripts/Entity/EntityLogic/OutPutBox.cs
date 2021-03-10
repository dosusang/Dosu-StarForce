using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce {
	public class OutPutBox : Entity {

        public List<string> outPuts = new List<string>();
        private string answer = "";
        private MainForm form;
		
        private void OnTriggerEnter2D(Collider2D collision) {
            var obj = collision.gameObject;
            var box = obj.GetComponent<Box>();
            if (box != null) {
                outPuts.Add(box.boxPram);
            }
        }


        public void CheckOutPut() {
            bool isRight = true;
            if (outPuts.Count != answer.Length) isRight = false;
            else {
                for (int i = 0; i < answer.Length; i++) {
                    if (outPuts[i] != answer[i].ToString()) {
                        isRight = false;
                        break;
                    }
                }
            }

            if (isRight) {
                form.UpdateOutPut("Accpet!!");
            } else {
                form.UpdateOutPut("错误输出\n你应该输出" + answer);
            }
        }
        private string GetPrams() {
            string s = "";
            foreach (var pram in outPuts) {
                s += pram;
            }
            return s;
        }

        internal void SetInfo(string outPut, int formId) {
            answer = outPut;
            if (form == null) form = (MainForm)GameEntry.UI.GetUIForm(formId).Logic;
            var outputpos = GameObject.Find("PointOutput").transform.position;
            gameObject.transform.position = outputpos + 1 * Vector3.down;
            OnHide(true, null);
        }
    }
}
