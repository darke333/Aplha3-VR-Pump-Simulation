using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOnPosWhenFall : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] Transform position;
    [SerializeField] DisolveScript DisolveScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor" || collision.gameObject.name == "Floor_Unwrapped")
        {
            OnFall();
        }
    }

    void OnFall()
    {
        if(controller != null)
        {
            controller.DisovleController(true);
        }
        else
        {
            DisolveScript.enabled = true;
            DisolveScript.DisolveProcess(true);
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (transform.Find("Quad"))
        {
            transform.Find("Quad").gameObject.SetActive(false);
        }
    }

    public void ReturnOnPos()
    {
        if (controller == null)
        {
            DisolveScript.enabled = true;
            DisolveScript.DisolveProcess(false);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.position = position.position;
            gameObject.transform.rotation = position.rotation;
        }
        else
        {
            controller.SnapClapan();
            controller.DisovleController(false);
        }
    }

}
