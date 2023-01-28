using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SlotColumn : SerializedMonoBehaviour
{
    [SerializeField] private List<SlotItem> _slotItems;

    [SerializeField] private SlotItem _upperItem, _bottemItem;

    [SerializeField] private readonly float _spinTime;
    private float _currentSpineTime;

    private Vector3 _startPosition;

    [SerializeField] private float _upperBorder, _bottemBorder;

    private SlotItem _winCell;

    private bool _isStoped;
    private bool _canStop;

    public bool IsStoped => _isStoped;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void SetStopCell(CellsType winCell)
    {
        foreach (var slotItem in _slotItems)
            if (slotItem.CellsType == winCell)
                _winCell = slotItem;
    }

    public void SpinSlot()
    {
        if (_winCell == null) throw new System.InvalidOperationException("Победная ячейка не указана");

        _canStop = false;
        _isStoped = false;
        _currentSpineTime = _spinTime;

        StartCoroutine(ScrollSlot());
        StartCoroutine(WaitSeconds(_spinTime));
        IEnumerator WaitSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            _currentSpineTime = 0;
        }
    }

    public void CanStop()
    {
        _canStop = true;
    }

    private IEnumerator ScrollSlot()
    {
        while (_currentSpineTime > 0 | _canStop == false)
            yield return MoveColumn();

        StopOnWinCell();

        _isStoped = true;

        StopAllCoroutines();
    }

    private IEnumerator MoveColumn()
        {
            transform.position -= new Vector3(
                0,
                0.79f
                , 0);

            yield return new WaitForEndOfFrame();

            if (transform.position.y <= _bottemBorder)
                transform.position = new Vector3(transform.position.x, _upperBorder, transform.position.z);
        }

    private void StopOnWinCell()
        {
            transform.position = new Vector3(_startPosition.x,
                _startPosition.y - (_upperItem.transform.position.y - _bottemItem.transform.position.y) / _slotItems.Count * _winCell.PositionInColomn,
                _startPosition.z);
        }
}
