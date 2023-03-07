using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WinRateGenerator
{
    private AdsServise _adsServise;

    private Dictionary<int, int> _rateBySpineDictionary;
    private int _spinStrik;

    public WinRateGenerator(AdsServise adsServise)
    {
        _adsServise = adsServise;

        _spinStrik = PlayerPrefs.HasKey("SpineStrik") ? PlayerPrefs.GetInt("SpineStrik") : 0;
        _rateBySpineDictionary = new()
        {
            { 1, 0 },
            { 2, 20 },
            { 3, 50 },
            { 4, 100 },
            { 5, 0},
            { 6, 0 },
            { 7, 0 },
            { 8, 50 },
            { 9, 80 },
            { 10, 0},
            { 11, 0 },
            { 12, 0 },
            { 13, 80 },
            { 14, 100 },
            { 15, 0 },
            { 16, 0 },
            { 17, 0 },
            { 18, 40 },
            { 19, 50 },
            { 20, 0 },
            { 21, 0 },
            { 22, 0 },
            { 23, 50 },
            { 24, 100 },
            { 25, 0 },
            { 26, 0 },
            { 27, 0 },
            { 28, 30 },
            { 29, 40 },
            { 30, 0 }
        };

    }

    public List<CellsType> GetSlotsCombinationList(int slotsCount)
    {
        _spinStrik++;

        PlayerPrefs.SetInt("SpineStrik", _spinStrik);

        if (_spinStrik > _rateBySpineDictionary.Count)
            _spinStrik = 1;

        if (_spinStrik % 5 == 0)
        {
            if (Advertisements.Instance.IsRewardVideoAvailable() && _adsServise.IsAdsBlocked == false)
            {
                _adsServise.ShowAdAndAccrue(0);
                return null;
            }

            _spinStrik++;

            if (_spinStrik > _rateBySpineDictionary.Count)
                _spinStrik = 1;
        }

        PlayerPrefs.SetInt("SpineStrik", _spinStrik);

        if (Random.Range(1, 100) <= _rateBySpineDictionary[_spinStrik])
        {
            int rowCounter = Random.Range(1, 100);

            Debug.Log(rowCounter);

            if (rowCounter <= 50)
            {
                if (rowCounter <= 20)
                    return GetGeneratedCombinationWithCellsInRow(GetRandomCells(), 5);
                else
                    return GetGeneratedCombinationWithCellsInRow(GetRandomCells(), 4);
            }
            else
            {
                return GetGeneratedCombinationWithCellsInRow(GetRandomCells(), 3);
            }
        }
        else
        {
            return GetGeneratedRandomCombinationCells(slotsCount);
        }
    }

    private List<CellsType> GetGeneratedRandomCombinationCells(int slotsCount)
    {
        List<CellsType> currenCellsList = new();

        for (int i = 0; i < slotsCount; i++)
        {
            CellsType currentCell = (CellsType)Random.Range(0, 7);

            if (currentCell != CellsType.SevenBar)
                currenCellsList.Add(currentCell);
            else
                slotsCount++;
        }

        return currenCellsList;
    }    

    private List<CellsType> GetGeneratedCombinationWithCellsInRow(CellsType rowTypeOfCell, int rowCount)
    {
        CellsType[] result = new CellsType[5];

        int firstCellIndex;

        while (true)
        {
            firstCellIndex = Random.Range(1, 5);

            if (firstCellIndex + (rowCount - 1) <= 5)
                break;
        }

        for (int i = 1; i < 6; i++)
        {
            if (i >= firstCellIndex && i <= firstCellIndex + (rowCount - 1))
                result[i - 1] = rowTypeOfCell;
            else
                result[i - 1] = GetRandomCellsWithout(rowTypeOfCell);
        }

        return result.ToList() ;
    }

    private CellsType GetRandomCellsWithout(CellsType cellsType)
    {
        CellsType result = cellsType;

        while (result == cellsType)
            result = GetRandomCells();

        return result;
    }

    private CellsType GetRandomCells()
    {
        return (CellsType)Random.Range(0, System.Enum.GetNames(typeof(CellsType)).Length);
    }
}
