using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_RenderModel controllerModel;
    
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_joint = null;

    private Interactible m_curentInteractible = null;
    public List<Interactible> m_contactInteracitbles = new List<Interactible>();

    public JumpScaresManager jumpScaresManager;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_joint = GetComponent<FixedJoint>();
        controllerModel = GetComponentInChildren<SteamVR_RenderModel>();
    }


    private void Update()
    {
        // get trigger down input
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }
        //get trigger up input
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //tag check
        if (!other.gameObject.CompareTag("cube"))
            return;
        
        m_contactInteracitbles.Add(other.gameObject.GetComponent<Interactible>());

        if (other.gameObject.name != "GlowStick")
        {
            jumpScaresManager.isPickedUp = true;
            Debug.Log(jumpScaresManager.isPickedUp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("cube"))
            return;

        m_contactInteracitbles.Remove(other.gameObject.GetComponent<Interactible>());
    }

    public void Pickup()
    {
        // get nearest
        m_curentInteractible = GetNearestInteractible();

        // null check
        if (!m_curentInteractible)
            return;

        //already held check
        if (m_curentInteractible.m_activeHand)
            m_curentInteractible.m_activeHand.Drop();

        // attach
        Rigidbody targetBody = m_curentInteractible.GetComponent<Rigidbody>();
        m_joint.connectedBody = targetBody;

        //set active hand
        m_curentInteractible.m_activeHand = this;
        controllerModel.gameObject.SetActive(false);
    }

    public void Drop()
    {
        //null check
        if (!m_curentInteractible)
            return;

        //apply velocity
        Rigidbody targetBody = m_curentInteractible.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        //detatch
        m_joint.connectedBody = null;

        //clear
        m_curentInteractible.m_activeHand = null;
        m_curentInteractible = null;
        controllerModel.gameObject.SetActive(true);
    }

    private Interactible GetNearestInteractible()
    {
        Interactible nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;
        foreach (Interactible interactible in m_contactInteracitbles)
        {
            distance = (interactible.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactible;
            }
        }
        return nearest;
    }
}