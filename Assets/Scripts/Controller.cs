using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public class ClapanController : MonoBehaviour
    {
        public GameObject RotatingPart;
        public HingeJoint hingeJoint;

        public ClapanController(GameObject targetObj)
        {
            RotatingPart = targetObj;
            hingeJoint = RotatingPart.GetComponent<HingeJoint>();
        }
    }

    public class ClapanBase : ClapanController
    {
        public ClapanBase(GameObject targetObj)
        : base(targetObj)
        {

        }

        public void DisconnectClapan()
        {
            if (hingeJoint != null)
            {
                if (Mathf.Abs(hingeJoint.angle - hingeJoint.limits.min) < 5)
                {
                    Destroy(hingeJoint);
                }
            }            
        }
    }

    public class ControllerRotate : ClapanController
    {
        public ControllerRotate(GameObject targetObj)
        : base(targetObj)
        {

        }

        public float RotatePercent;

        public void CalculateFromAngle()
        {
            RotatePercent = Mathf.Abs(hingeJoint.angle - hingeJoint.limits.min) / Mathf.Abs(hingeJoint.limits.max - hingeJoint.limits.min);
        }
        private void Update()
        {
            CalculateFromAngle();
        }
    }

    public class ClapanRotate : ControllerRotate
    {
        public ClapanRotate(GameObject targetObj)
        : base(targetObj)
        {

        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
