using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveScript : MonoBehaviour
{

    [SerializeField] Material DisolveMaterial;
    Material StartMaterial;
    public bool Disolving;
    bool StartedDisolving;

    private void OnEnable()
    {
        StartMaterial = GetComponent<Renderer>().material;
        //this.enabled = false;
        StartedDisolving = false;
    }

    public void DisolveProcess(bool OnDisovle)
    {
        
        Disolving = OnDisovle;
        Material material = new Material(DisolveMaterial);
        material.SetTexture("_Albedo", StartMaterial.GetTexture("_BaseColorMap"));
        material.SetTexture("_Normal", StartMaterial.GetTexture("_NormalMap"));
        GetComponent<MeshRenderer>().material = material;
        if (OnDisovle)
        {
            GetComponent<MeshRenderer>().material.SetFloat("_DisolveControll", 0);
        }
        else
        {
            GetComponent<MeshRenderer>().material.SetFloat("_DisolveControll", 1);
        }
        StartedDisolving = true;
    }

    void Update()
    {
        if (StartedDisolving)
        {
            if (Disolving)
            {
                float i = GetComponent<MeshRenderer>().material.GetFloat("_DisolveControll");
                if (i < 1)
                {
                    i += Time.deltaTime;
                    GetComponent<MeshRenderer>().material.SetFloat("_DisolveControll", i);
                }
                if (i >= 1)
                {
                    /*if (GetComponent<Renderer>().material != StartMaterial)
                    {
                        GetComponent<Renderer>().material = StartMaterial;
                    }*/
                    StartedDisolving = false;
                    if (GetComponent<BackOnPosWhenFall>())
                    {
                        GetComponent<BackOnPosWhenFall>().ReturnOnPos();
                    }
                }
            }
            else
            {
                float i = GetComponent<MeshRenderer>().material.GetFloat("_DisolveControll");
                if (i > 0)
                {
                    i -= Time.deltaTime;
                    GetComponent<MeshRenderer>().material.SetFloat("_DisolveControll", i);
                }
                if (i <= 0)
                {
                    if (GetComponent<Renderer>().material != StartMaterial)
                    {
                        GetComponent<Renderer>().material = StartMaterial;
                        if (transform.Find("Quad"))
                        {
                            transform.Find("Quad").gameObject.SetActive(true);
                        }
                        StartedDisolving = false;
                        //this.enabled = false;
                    }
                }
            }

        }
    }        
}
