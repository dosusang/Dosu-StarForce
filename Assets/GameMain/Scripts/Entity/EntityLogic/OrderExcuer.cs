using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace StarForce {
    public class OrderExcuer : Entity {
        private enum nowStatus {
            GETORDER = 0,
            EXCUTING = 1,
            READY = 2,
            COMPELETED,
        };
        private OrderType order = OrderType.NONE;
        private nowStatus status = nowStatus.COMPELETED;
        private OrderHolder holder = null;
        private int nowLine = 0;
        private int tweenId = 0;
        private Vector3 pointInput;
        private Vector3 pointOutput;

        private MainForm form;
        public int mainFormId = 0;

        private void Start() {
            holder = GameObject.Find("OrderHolder").GetComponent<OrderHolder>();
            pointInput = GameObject.Find("PointInput").transform.position;
            pointOutput = GameObject.Find("PointOutput").transform.position;
        }

        public void StartExcute() {
            if (form == null) {
                form = (MainForm)GameEntry.UI.GetUIForm(mainFormId).Logic;
            }
            status = nowStatus.READY;
            transform.position = new  Vector3(0, 0, 0);
            DOTween.Kill(tweenId);
            nowLine = 0;
            SafeUpdateLog("开始执行", InfoTypes.Info);
        }

        public void SafeUpdateLog(string s, InfoTypes type) {
            if (form != null) form.UpdateLogger(s, type);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) { StartExcute(); }
            if (status == nowStatus.READY) {
                order = GetOrder();
                if (order != OrderType.NONE) {
                    status = nowStatus.EXCUTING;
                    ExcuteOrder(order);
                }
            }
        }
        private void ExcuteOrder(OrderType order) {
            if (order == OrderType.GETINPUT) {
                SafeUpdateLog("获取输入中", InfoTypes.Info);
                var tween = transform.DOMove(pointInput, 1);
                tween.onComplete = () => {
                    var gameobj = GetTopBox();
                    if (gameobj != null) {
                        SafeUpdateLog("检测到输入", InfoTypes.Info);
                        gameobj.transform.DOMove(transform.position, 0.5f).onComplete = () => {
                            OnAttached(gameobj.GetComponent<Entity>(), transform, null) ;
                            onOrderComplete();
                        };
                        Debug.Log("SetPos");
                    } else {
                        SafeUpdateLog("警告：本次无输入\n请重置指令", InfoTypes.Wram);
                        Debug.Log("NOhit");
                    }

                };
                tweenId = tween.intId;
            } else if (order == OrderType.OUTPUT) {
                SafeUpdateLog("输出中", InfoTypes.Info);
                var tween = transform.DOMove(pointOutput, 1);
                tween.onComplete  = ()=> {
                    OnDetached(child[0].GetComponent<Entity>(), null);
                    onOrderComplete();
                };
                tweenId = tween.intId;
            }
        }
        private GameObject GetTopBox() {
            var info = Physics2D.Raycast(transform.position, Vector3.down);
            Debug.Log(info.transform.gameObject);
            if (info.transform.gameObject.GetComponent<Box>() == null) return null;
            return info.transform.gameObject;
        }
        private void onOrderComplete() {
            status = nowStatus.READY;
            
        }
        private OrderType GetOrder() {

            if (nowLine == holder.transform.childCount) {
                nowLine = 0;
                status = nowStatus.COMPELETED;
                SafeUpdateLog("运行完成", InfoTypes.Info);
                return OrderType.NONE;
            }
            var o = holder.GetOrderByPos(nowLine).order;
            nowLine += 1;
            return o;
        }
    }
}
