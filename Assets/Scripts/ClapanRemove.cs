using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapanRemove : MonoBehaviour
{
    HingeJoint hingeJoint;
    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hingeJoint.angle == hingeJoint.limits.min)
        {
            Destroy(GetComponent<HingeJoint>());           
        }
    }
}
