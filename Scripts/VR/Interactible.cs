using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactible : MonoBehaviour
{
    [HideInInspector]
    public Hand m_activeHand = null;
}
