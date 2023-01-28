using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField] private CellsType _cellsType;
    [SerializeField] private int _positionInColomn;

    public int PositionInColomn => _positionInColomn;

    public CellsType CellsType => _cellsType;
}
