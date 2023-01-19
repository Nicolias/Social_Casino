using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField] private CellsType _cellsType;

    public CellsType CellsType => _cellsType;
}
