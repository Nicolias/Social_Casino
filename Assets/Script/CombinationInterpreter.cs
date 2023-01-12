using System.Collections.Generic;

public class CombinationInterpreter
{
    public void InterpritateCombination(List<CellsType> slotsCombination, WinWindow winWindow, int currentBet)
    {
        int winStrik = 0;

        foreach (CellsType cell in slotsCombination)
            if (cell == CellsType.SevenBar)
                winStrik++;

        if (winStrik == slotsCombination.Count)
            winWindow.AccrueWinnings(currentBet * 2);
    }
}
