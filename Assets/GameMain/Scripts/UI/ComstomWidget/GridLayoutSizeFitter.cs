using UnityEngine;
using UnityEngine.UI;


namespace StarForce {
	/// <summary>
	/// This will automatically set a grid layout's size so it fits its content. This can be useful if you need a grid 
	/// layout to fit properly inside a Scroll Rect.
	/// </summary>
	[ExecuteInEditMode]
	public class GridLayoutSizeFitter : MonoBehaviour {
		public bool executeInEditMode;
		public GridLayoutGroup layoutGroup;
		public bool height;
		public bool width;


		RectTransform _rTransform;
		int _ChildrenCount {
			get { return transform.childCount; }
		}


		// Use this for initialization
		void Start() {
			if (!layoutGroup) {
				layoutGroup = GetComponentInChildren<GridLayoutGroup>();
				if (!layoutGroup) {
					Debug.LogWarning("No GridLayout was found", gameObject);
					enabled = false;
					return;
				}
			}
			_Init();
		}
		void Update() {
			if (!executeInEditMode || !enabled)
				return;
			_Init();
		}


		protected void _Init() {
			_rTransform = (RectTransform)layoutGroup.transform;
			Fit();
		}


		public void Fit() {
			if (!enabled) {
				Debug.LogWarning("This has been disabled", gameObject);
				return;
			}
			float x = _rTransform.sizeDelta.x;
			float y = _rTransform.sizeDelta.y;
			if (height) {
				Vector2 temp = _rTransform.anchorMin;
				temp.y = 1f;
				_rTransform.anchorMin = temp;
				temp = _rTransform.anchorMax;
				temp.y = 1f;
				_rTransform.anchorMax = temp;
				temp = _rTransform.pivot;
				temp.y = 1f;
				_rTransform.pivot = temp;
				_rTransform.sizeDelta = new Vector2(x, GetProperHeight());
			}
			if (width) {
				Vector2 temp = _rTransform.anchorMin;
				temp.x = 0f;
				_rTransform.anchorMin = temp;
				temp = _rTransform.anchorMax;
				temp.x = 0f;
				_rTransform.anchorMax = temp;
				temp = _rTransform.pivot;
				temp.x = 0f;
				_rTransform.pivot = temp;
				_rTransform.sizeDelta = new Vector2(GetProperWidth(), y);
			}
		}
		public float GetProperHeight() {
			int rows = 1;
			if (layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount) {
				rows = Mathf.CeilToInt((float)(_ChildrenCount) / (float)(layoutGroup.constraintCount));
			} else if (layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount) {
				rows = layoutGroup.constraintCount;
			}
			float totalSpacing = (rows - 1) * layoutGroup.spacing.y;
			return layoutGroup.padding.top + layoutGroup.padding.bottom + (layoutGroup.cellSize.y * rows) + totalSpacing;
		}
		public float GetProperWidth() {
			int columns = 1;
			if (layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount) {
				columns = Mathf.CeilToInt((float)(_ChildrenCount) / (float)(layoutGroup.constraintCount));
			} else if (layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount) {
				columns = layoutGroup.constraintCount;
			}
			float totalSpacing = (columns - 1) * layoutGroup.spacing.x;
			return layoutGroup.padding.left + layoutGroup.padding.right + (layoutGroup.cellSize.x * columns) + totalSpacing;
		}
	}
}