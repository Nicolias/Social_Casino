using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SlotColumn : SerializedMonoBehaviour
{
    [SerializeField] private List<SlotItem> _slotItems;

    [SerializeField] private readonly float _spinTime;
    private float _currentSpineTime;

    [SerializeField] private float _upperBorder, _bottemBorder;

    private SlotItem _winCell;

    private bool _isStoped;
    private bool _canStop;

    public bool IsStoped => _isStoped;
    public const float _speed = 10f;

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
            yield return MoveSpin();

        while (_winCell.transform.position.y >= 1.9 | _winCell.transform.position.y <= 1.4)
            yield return MoveSpin();

        IEnumerator MoveSpin()
        {
            transform.position -= new Vector3(
                0,
                0.79f
                , 0);

            yield return new WaitForEndOfFrame();

            if (transform.position.y <= _bottemBorder)
                transform.position = new Vector3(transform.position.x, _upperBorder, transform.position.z);
        }

        _isStoped = true;

        StopAllCoroutines();
    }
}
