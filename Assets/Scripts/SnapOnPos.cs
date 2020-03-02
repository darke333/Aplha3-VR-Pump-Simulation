using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SnapOnPos : MonoBehaviour
{
    public GameObject targetObj;
    [SerializeField] Controller Controller;
    [SerializeField] GameObject Rotator;

    private void Start()
    {
        OnOffProp(false);
    }

    private void OnEnable()
    {
        OnOffProp(false);
        targetObj.GetComponent<Interactable>().onAttachedToHand += SnapOnPos_onAttachedToHand;
        targetObj.GetComponent<Interactable>().onDetachedFromHand += SnapOnPos_onDetachedFromHand;
    }

    void OnOffProp(bool switcher)
    {
        GetComponent<Collider>().enabled = switcher;
        GetComponent<MeshRenderer>().enabled = switcher;
        Rotator.GetComponent<Collider>().enabled = switcher;
        Rotator.GetComponent<MeshRenderer>().enabled = switcher;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targetObj == other.gameObject)
        {
            Controller.SnapClapan();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SnapOnPos_onDetachedFromHand(Hand hand)
    {
        OnOffProp(false);
    }

    private void SnapOnPos_onAttachedToHand(Hand hand)
    {
        OnOffProp(true);
    }
}
