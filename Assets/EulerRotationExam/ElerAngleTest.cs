using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElerAngleTest : MonoBehaviour
{
    public Vector3 roataion = Vector3.zero;
    public float time = 0;
    public Vector3 startRotation = Vector3.zero;
    public Vector3 endRotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        roataion.x = Mathf.Lerp(startRotation.x, endRotation.x, time / 5);
        roataion.y = Mathf.Lerp(startRotation.y, endRotation.y, time / 5);
        roataion.z = Mathf.Lerp(startRotation.z, endRotation.z, time / 5);
        if (time >= 7) time = 0;

        transform.rotation = Quaternion.Euler(roataion);

    }
}
