using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SlotItem SlotItem;
    private Vector3 _startPosiition;
    private RectTransform _rectTransform;
    public int a;
    public float b;

    private void Start()
    {
        _startPosiition = transform.position;
        _rectTransform = (RectTransform)transform;
    }

    void Update()
    {
        Debug.Log($"{_rectTransform.rect.height / a}");
        Debug.Log($"{SlotItem.gameObject.name}, {SlotItem.transform.position.y}");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.position = new Vector3(_startPosiition.x, _startPosiition.y - b, _startPosiition.z);
            _startPosiition = transform.position;
        }
    }
}
