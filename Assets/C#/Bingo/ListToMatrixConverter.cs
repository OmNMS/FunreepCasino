using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Takes a UI button list and converts it into a 5x5 matrix
/// </summary>
public static class ListToMatrixConverter 
{
    public static List<Button> buttonList; // The list of buttons

    public static Button[,] ConvertButtonListToMatrix(List<Button> buttonList)
    {
        var matrix = new Button[5, 5];

        int index = 0;
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                matrix[row, col] = buttonList[index++];
            }
        }

        return matrix;
    }

    public static void DisplayMatrix(Button[,] matrix)
    {
        Debug.Log("Matrix:");

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                Debug.Log("Row: " + row + ", Col: " + col + ", Button: " + matrix[row, col].GetComponentInChildren<Text>().text);
            }
        }
    }
}