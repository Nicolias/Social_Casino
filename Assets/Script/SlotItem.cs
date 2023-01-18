using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField] private SlotColumn _slotColumn;
    [SerializeField] private CellsType _cellsType;

    [SerializeField] private float _upperBorder, _bottemBorder;

    private Transform _transform;
    private bool _canMove;

    public CellsType CellsType => _cellsType;

    private void Start()
    {
        _transform = transform;

    }

    //private void Update()
    //{
    //    if (_canMove == false)
    //        return;

    //    _transform.position -= new Vector3(0, _slotColumn.CurrentSpeed * Time.deltaTime, 0);

    //    if (transform.position.y <= _bottemBorder)
    //        transform.position = new Vector3(_transform.position.x, _upperBorder, _transform.position.z);
    //}

    public void MoveDown()
    {
        _canMove = true;
    }

    public void StopMovment()
    {
        _canMove = false;
    }
}
