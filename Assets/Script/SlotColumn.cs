using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SlotColumn : SerializedMonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;

    [SerializeField] private Dictionary<CellsType, double> _cellsWinNumbers;

    private const float _maxSpeed = 3.5f;
    private const float _minSpeed = 0.8f;

    private double _winNumber;
    private float _currentSpeed;

    private bool _isStoped;
    public bool IsStoped => _isStoped;

    public void SpinSlot()
    {
        _currentSpeed = _maxSpeed;

        _isStoped = false;
        StartCoroutine(ScrollSlot());
    }

    public void StopSlotOnCell(CellsType winCell)
    {
        _winNumber = _cellsWinNumbers[winCell];

        StartCoroutine(DecreaseSpeedAfterEverySecondsByValue(0.1f, 0.1f));
    }

    private IEnumerator ScrollSlot()
    {
        while (_currentSpeed > _minSpeed)
        {
            yield return new WaitForSeconds(0.01f);
            _scrollbar.value += _currentSpeed / 100f;

            if (_scrollbar.value >= 1)
                _scrollbar.value = 0;
        }

        while (Math.Round(_scrollbar.value, 2) != _winNumber)
        {
            yield return new WaitForSeconds(0.01f);
            _scrollbar.value += _minSpeed / 100f;

            if (_scrollbar.value >= 1)
                _scrollbar.value = 0;
        }

        _isStoped = true;

        StopAllCoroutines();
    }

    private IEnumerator DecreaseSpeedAfterEverySecondsByValue(float seconds, float value)
    {
        while (_currentSpeed > 0)
        {
            yield return new WaitForSeconds(seconds);

            _currentSpeed -= value;

            if (_currentSpeed < 0)
                _currentSpeed = 0;
        }
    }
}
