using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
    public TMPro.TMP_Text StatusText;
    public bool Powered = false;

    [Space(10)]
    public NavMeshAgent Agent;
    public bool Moving = false;

    [Header("Material Logic")]
    public MeshRenderer Renderer;
    public Material PoweredMat;
    public Material NotPoweredMat;

    private void Start()
    {
        // Default to where the turret starts is now
        Agent.SetDestination(transform.position);
    }

    public void OnPlacement()
    {

    }

    public void OnPickup()
    {

    }

    void Update()
    {
        string statusText = Powered ? "POWERED" : "NOT POWRED";
        string movingText = Moving ? "Moving" : "Stationary";

        StatusText.text = $"{statusText}\n{movingText}";
    }

    public void SetPowerStatus(bool isPowered)
    {
        Renderer.materials = new Material[]{isPowered ? PoweredMat : NotPoweredMat};
        Powered = isPowered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BasePowerRange")
        {
            Base _base = other.gameObject.GetComponentInParent<Base>();
            _base.AddTowerInRange(this);
            SetPowerStatus(true);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BasePowerRange")
        {
            Base _base = other.gameObject.GetComponentInParent<Base>();
            _base.RemoveTowerInRange(this);

            SetPowerStatus(false);
        } 
    }
}
