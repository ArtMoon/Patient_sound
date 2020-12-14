using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDebug : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cameras;
    [SerializeField] private List<Transform> _playerPositions;
    private const float buttonWidth = 200;

    public static Camera CurrentCamera;

    private const float buttonHeight = 50;

    private void Awake()
    {
        foreach (var c in _cameras)
        {
            c.SetActive(false);
        }
        CurrentCamera = _cameras[0].GetComponent<Camera>();
        Player.Instance.HeroMovement.SetupHeroPosition(_playerPositions[0].position);
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
                CurrentCamera = _cameras[i].GetComponent<Camera>();
                Player.Instance.HeroMovement.SetupHeroPosition(_playerPositions[i].position);
                _cameras[i].SetActive(true);

            }
        }
        

    }

}
