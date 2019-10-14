using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class ButtonChecker : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_GrabAction = null;
    
    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.8f;


    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    
    private void Update()
    {
        //pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);
    }

    private bool UpdatePointer()
    {
        //Ray from the controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //If it's a hit
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Button")
        {
            Debug.DrawRay(transform.position, transform.forward, Color.cyan);
            m_Pointer.transform.position = hit.point;

            if (m_GrabAction.GetStateDown(m_Pose.inputSource))
            {
                print(m_Pose.inputSource + " Trigger Down");
                StartCoroutine(StartGame());
            }
            return true;
        }

        // If not a hit
        return false;
    }

    IEnumerator StartGame()
    {
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(1);
    }
}
