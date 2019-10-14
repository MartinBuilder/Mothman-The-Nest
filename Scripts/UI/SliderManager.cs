using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SliderManager : MonoBehaviour
{
    private AudioManager auManager;

   [SerializeField]
    private List<Slider> _sliderlist = new List<Slider>();

    // Start is called before the first frame update
    void Awake()
    {
        _sliderlist = gameObject.GetComponentsInChildren<Slider>().ToList();
        auManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        for (int i = 0; i < _sliderlist.Count; i++)
        {
            auManager.SetList(i, _sliderlist[i]);
        }

        for (int i = 0; i < auManager.GetMList().Count; i++)
        {
            _sliderlist[0].value = auManager.GetMList()[0].GetComponent<AudioSource>().volume;
            Debug.Log(_sliderlist[i]);
            Debug.Log(auManager.GetMList()[i]);
        }
        for (int i = 0; i < auManager.GetEList().Count; i++)
        {
            _sliderlist[1].value = auManager.GetEList()[0].GetComponent<AudioSource>().volume;
        }
    }
}
