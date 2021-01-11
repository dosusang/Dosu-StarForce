using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    public class Dog : CollidableObj
    {
        private DogData m_AircraftData = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (Input.anyKey)
            {
                var temp = CachedTransform.localPosition;
                temp.Set(CachedTransform.localPosition.x, CachedTransform.localPosition.y + (float)0.1, CachedTransform.localPosition.z);
                CachedTransform.localPosition = temp;
            }
        }

    }
}

