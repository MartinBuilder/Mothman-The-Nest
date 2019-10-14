using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public void InitiatePanel(string path)
    {
        Transform parent = transform.parent;
        
        Object panel = Instantiate(Resources.Load(path), parent.transform.position, Quaternion.Euler(0,0,0) , GameObject.Find("Canvas").transform);

        Destroy(parent.gameObject);
    }
}
