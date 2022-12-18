using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotColumn : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;

    private const float _maxSpeed = 2.5f;
    private const float _minSpeed = 0.35f;

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

    public void StopSlotOnCell(float cellNumber)
    {
        _winNumber = cellNumber;

        StartCoroutine(DecreaseSpeedAfterEverySecondsByValue(0.7f, 0.1f));
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

        _scrollbar.value = GetClosestNumberBetweenNumbersByStep(Math.Round(_scrollbar.value, 2), 15);
        _isStoped = true;

        StopAllCoroutines();
    }

    private IEnumerator DecreaseSpeedAfterEverySecondsByValue(float seconds, float value)
    {
        while (_maxSpeed > 0)
        {
            yield return new WaitForSeconds(seconds);

            _currentSpeed -= value;

            if (_maxSpeed < 0)
                _currentSpeed = 0;
        }
    }

    private float GetClosestNumberBetweenNumbersByStep(double value, int step)
    {
        value *= 100;
        double temp = value / step;

        if (temp % 1 * 10 >= 5)
            temp += 1;

        temp -= temp % 1;
        return (float)(temp * step / 100);
    }
}
