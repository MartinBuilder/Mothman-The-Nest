﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;
    
    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.2f;


    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    
    private void Update()
    {
        //pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);

        //Teleporter
        if (m_TeleportAction.GetStateUp(m_Pose.inputSource))
            TryTeleport();
    }

    private void TryTeleport()
    {
        //Check for valid position, and if already teleporting
        if (!m_HasPosition || m_IsTeleporting)
            return;

        // Get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;


        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;

        //Move
        StartCoroutine(MoveRig(cameraRig, translateVector));
    }
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        //Flag
        m_IsTeleporting = true;

        //Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        //Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        //Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        //De-flag
        m_IsTeleporting = false;

        yield return null;
    }

    private bool UpdatePointer()
    {
        //Ray from the controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //If it's a hit
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Floor")
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }

        // If not a hit
        return false;
    }
}
