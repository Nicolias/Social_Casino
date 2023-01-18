using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SlotColumn : SerializedMonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;

    [SerializeField] private Dictionary<CellsType, double> _cellsWinNumbers;

    [SerializeField] private List<SlotItem> _slotItems;

    [SerializeField] private float _maxSpeed = 1f;
    private const float _minSpeed = 0;

    [SerializeField] private float _upperBorder, _bottemBorder;

    private SlotItem _winCell;

    private bool _isStoped;
    public bool IsStoped => _isStoped;
    public float CurrentSpeed { get; private set; }

    public void SpinSlot()
    {
        CurrentSpeed = _maxSpeed;

        _isStoped = false;
        StartCoroutine(ScrollSlot());
    }

    public void StopSlotOnCell(CellsType winCell)
    {
        foreach (var slotItem in _slotItems)
            if (slotItem.CellsType == winCell)
                _winCell = slotItem;

        //StartCoroutine(DecreaseSpeedAfterEverySecondsByValue(0.1f, 0.1f));
    }

    private IEnumerator ScrollSlot()
    {
        for (int i = 0; i < _slotItems.Count; i++)
            _slotItems[i].MoveDown();

        while (CurrentSpeed > _minSpeed)
        {
            MoveSpin();

            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitUntil(() => _winCell.transform.localPosition.y >= -144 & _winCell.transform.localPosition.y <= -140);

        foreach (var slotItem in _slotItems)
            slotItem.StopMovment();


        //while (CurrentSpeed > _minSpeed | Math.Round(_scrollbar.value, 2) != _winNumber)
        //{
        //    yield return new WaitForSeconds(0.01f);


        //}

        void MoveSpin()
        {
            transform.position -= new Vector3(
                0,
                CurrentSpeed * Time.deltaTime
                , 0);

            if (transform.position.y <= _bottemBorder)
                transform.position = new Vector3(transform.position.x, _upperBorder, transform.position.z);
        }

        _isStoped = true;

        StopAllCoroutines();

        yield break;
    }

    private IEnumerator DecreaseSpeedAfterEverySecondsByValue(float seconds, float value)
    {
        while (CurrentSpeed > _minSpeed)
        {
            yield return new WaitForSeconds(seconds);

            CurrentSpeed -= value;

            if (CurrentSpeed < 0)
                CurrentSpeed = 0;
        }
    }
}
