using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material mat;
    [SerializeField]
    private Material mat_despel;
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

            var tween2 = DOTween.To(()=>  mat_despel.GetFloat("_RatioUp"), (x) => mat_despel.SetFloat("_RatioUp", x), 0, (float)2);
            tween2.onComplete = () =>
            {
                mat_despel.SetFloat("_RatioUp", 1);
            };
        }
    }
}
