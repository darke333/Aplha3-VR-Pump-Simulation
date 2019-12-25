using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    JointLimits jointLimits;
    [SerializeField] FixedJoint fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        jointLimits = GetComponent<HingeJoint>().limits;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.eulerAngles.y == jointLimits.min || transform.eulerAngles.y == jointLimits.max)
        {
            Destroy(fixedJoint);
        }
    }
}
