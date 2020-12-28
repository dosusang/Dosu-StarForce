using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LockRatation : MonoBehaviour
{
    public bool lockX, lockY, lockZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var euler_angle = transform.rotation.eulerAngles;
        if (lockX) euler_angle.x = 0;
        if (lockY) euler_angle.y = 0;
        if (lockZ) euler_angle.z = 0;
        transform.localRotation= Quaternion.Euler(euler_angle);
    }
}
