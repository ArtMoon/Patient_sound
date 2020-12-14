using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClick : MonoBehaviour
{

    [SerializeField] private GameObject _point;
    [SerializeField] private LayerMask _layerMask;  
    [SerializeField] private Texture2D _handCursor;
    [SerializeField] private List<Texture2D> _dirrectionArrows;
    private RaycastHit _raycastHit;
    private HeroMovement _heroMovement;
    private Ray _ray;
    private GameObject _tmpPoint;
    private Vector3 _possibleMovePoint;
    public bool IsEnabled { get; set; } = false;
    public bool CursorOnUI { get; set; } = false;

    //Инициализация 
    public void Init(HeroMovement param)
    {
        _heroMovement = param;
        IsEnabled = true;
    }
    private void Update()
    {
        if (!IsEnabled || CursorOnUI) return;
        RayCasting();
        GetInput();
    }


    private void GetInput()
    {        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouseClick();
        }
    }

    //Вызывается каждый кадр для выделения и кэширования объекта
    private void RayCasting()
    {
        if (CamDebug.CurrentCamera == null) return;
        _ray = CamDebug.CurrentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _raycastHit, _layerMask))
        {
            
                _possibleMovePoint = _raycastHit.point;

            
        }
    }


    private void OnMouseClick()
    {               
        if(_possibleMovePoint != null)
        {            
            _heroMovement.GoToPoint(_possibleMovePoint);
            
        }
    }


}

