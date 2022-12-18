using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsWindow : MonoBehaviour
{
    [SerializeField] private List<SlotColumn> _slots;
    [SerializeField] private Button _spinButton;

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(() => StartCoroutine(Spin()));
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveAllListeners();
    }

    public IEnumerator Spin()
    {
        _spinButton.interactable = false;

        int index = 0;
        foreach (var slot in _slots)
            slot.SpinSlot();

        var currentSlot = _slots[index];

        while (currentSlot.IsStoped == false)
        {
            currentSlot.StopSlotOnCell(0);
            index++;

            yield return new WaitUntil(() => currentSlot.IsStoped != false);

            if (index < _slots.Count)
                currentSlot = _slots[index];
        }

        _spinButton.interactable = true;
    }
}
