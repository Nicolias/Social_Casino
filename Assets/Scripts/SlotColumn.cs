using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotColumn : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
        
    private decimal _speed = 1.5m;
    [SerializeField] private int _winNumber;

    private void Start()
    {
        StartCoroutine(ScrollSlot());
        StartCoroutine(DecreaseSpeedAfterEverySecondsByValue(1f, 0.1m));
    }
    private IEnumerator ScrollSlot()
    {
        while (_speed > 0)
        {
            yield return new WaitForSeconds(0.01f);
            _scrollbar.value += (float)_speed / 100f;

            if (_scrollbar.value >= 1)
                _scrollbar.value = 0;
        }

        _scrollbar.value = FindClosestNumberBetweenNumbersByStep(Math.Round(_scrollbar.value, 2), 15);
    }

    private IEnumerator DecreaseSpeedAfterEverySecondsByValue(float seconds, decimal value)
    {
        while (_speed > 0)
        {
            yield return new WaitForSeconds(seconds);

            _speed -= value;

            if (_speed < 0)
                _speed = 0;
        }
    }

    private float FindClosestNumberBetweenNumbersByStep(double value, int step)
    {
        value *= 100;
        double temp = value / step;

        if (temp % 1 * 10 >= 5)
            temp += 1;

        temp -= temp % 1;
        return (float)(temp * step / 100);
    }
}
