using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    [SerializeField] private HeroMovement _heroMovement;
    [SerializeField] private PointAndClick _pointAndClick;
    [SerializeField] private List<GameObject> _bodyParts;

    public HeroMovement HeroMovement { get { return _heroMovement; }}
    public PointAndClick PointAndClick { get { return _pointAndClick; }}
    public bool IsVisible { get; private set; } = true;


    private void Awake()
    {
        InitInstance(this);
        Init();
       
    }

    private void Init()
    {
        PointAndClick.Init(HeroMovement);

    }


    [ContextMenu("Hide Player")]
    public void HidePlayer()
    {
        foreach(var rend in _bodyParts)
        {
            rend.SetActive(false);
        }
        _pointAndClick.IsEnabled = false;
        IsVisible = false;
    }

    [ContextMenu("Show Player")]
    public void ShowPlayer()
    {
        foreach (var rend in _bodyParts)
        {
            rend.SetActive(true);
        }
        _pointAndClick.IsEnabled = true; 
        IsVisible = true;
    }





}
