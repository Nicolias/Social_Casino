using System.Collections.Generic;

public class CombinationInterpreter
{
    private WinWindow _winWindow;

    public CombinationInterpreter(WinWindow winWindow)
    {
        _winWindow = winWindow;
    }

    public void InterpritateCombination(List<CellsType> slotsCombination, int currentBet)
    {
        int winStrik = 0;

        foreach (CellsType cell in slotsCombination)
            if (cell == CellsType.SevenBar)
                winStrik++;

        if (winStrik == slotsCombination.Count)
            _winWindow.AccrueWinnings(currentBet * 2);
    }
}
