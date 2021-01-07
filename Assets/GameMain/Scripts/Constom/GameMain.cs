using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material mat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            float getFloat()
            {
                return mat.GetFloat("_ColorRatio");
            }
            void setFloat(float x)
            {
                mat.SetFloat("_ColorRatio", x);
            }
            var tween = DOTween.To(getFloat, setFloat, 2, (float)0.5);
            tween.onComplete = () =>
            {
                mat.SetFloat("_ColorRatio", 1);
            };
            tween.SetEase(Ease.OutBounce);
        }
    }
}
