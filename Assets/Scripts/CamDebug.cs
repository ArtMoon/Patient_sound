using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDebug : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cameras;
    private const float buttonWidth = 200;

    private const float buttonHeight = 50;

    private void Awake()
    {
        foreach (var c in _cameras)
        {
            c.SetActive(false);
        }
        _cameras[0].SetActive(true);
    }

    private void OnGUI()
    {
        for(int i = 0; i < _cameras.Count; i++)
        {
           if(GUI.Button(new Rect(0,i*buttonHeight + 5,buttonWidth,buttonHeight),"Activate " + _cameras[i].name))
           {

                foreach (var c in _cameras)
                {
                    c.SetActive(false);
                }
                _cameras[i].SetActive(true);

            }
        }
        

    }

}
