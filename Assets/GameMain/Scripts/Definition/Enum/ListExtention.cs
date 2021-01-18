using System;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce {
	public static class ListExtention {
		public static void Swap(ref List<BaseOrder> list, int i, int j) {
            try {
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            } catch (Exception) {
                Debug.Log(i + " " + j);
                throw;
            }
        }

        public static void MoveRange(ref List<BaseOrder> list, int start, int end, OrderMoveDirect status) {
            if (status == OrderMoveDirect.DOWN) {
                for (int i = end; i < start-1; i--) {
                    Swap(ref list, i, i - 1);
                }
            } else {
                for (int i = start; i < end-1; i++) {
                    Swap(ref list, i, i + 1);
                }
            }
        }

    }
}

