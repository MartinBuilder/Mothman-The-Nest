using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<Slider> _sliderList = new List<Slider>();
    [SerializeField]
    private List<GameObject> _masterList = new List<GameObject>();
    private List<GameObject> _effectList = new List<GameObject>();
    public List<GameObject> GetMList()
    {
        return _masterList;
    }
    public List<GameObject> GetEList()
    {
        return _effectList;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        _masterList = GameObject.FindGameObjectsWithTag("MasterAudio").ToList();
        _effectList = GameObject.FindGameObjectsWithTag("EffectsAudio").ToList();
    }

    // Update is called once per frame
    void Update()
    {   
        if (GameObject.FindWithTag("AudioPanel"))
        {
            SetVolume(0);
            SetVolume(1);
        }
    }

    public List<Slider> GetSliderList()
    {
        return _sliderList;
    }

    public void SetList(int index , Slider slider)
    {
         _sliderList[index] = slider;
        Debug.Log(_sliderList[index].value);
    }

    public void SetVolume(int index)
    {
       
        for (int i = 0; i < _masterList.Count; i++)
        {
            _masterList[i].GetComponent<AudioSource>().volume = _sliderList[0].value;
            
            Debug.Log("Test");
        }
        for (int i =0; i < _effectList.Count; i++)
        {
            _effectList[i].GetComponent<AudioSource>().volume = _sliderList[1].value;
        }
    }
}   