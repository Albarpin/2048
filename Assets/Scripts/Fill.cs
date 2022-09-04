using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill : MonoBehaviour
{

    public int value; 
    [SerializeField] Text valueDisplay; 
    [SerializeField] float moovingSpeed; 

    bool hasMerged;
    Image fillColor;

    public void UpdateFillValue(int cellValue)
    {
        value = cellValue;
        valueDisplay.text = value.ToString();

        SetFillColor(value);
    }

    private void Update()
    {
      
        if (transform.localPosition != Vector3.zero)
        {
            hasMerged = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moovingSpeed * Time.deltaTime);
        }
        else if (hasMerged == false)
        {
            if (transform.parent.GetChild(0) != this.transform) 
            {
                Destroy(transform.parent.GetChild(0).gameObject); 
            }
            hasMerged = true;
        }
    }

    public void DoubleCellValue()
    {
        value *= 2;
        GameManagement.instance.UpdateGameScore(value);
        valueDisplay.text = value.ToString();
        SetFillColor(value);
        GameManagement.instance.IsGameWon(value);
    }

    private int GetIndexOfColor(int cellValue)
    {
        int index = 0;
        while (cellValue != 1)
        {
            index++;
            cellValue /= 2;
        }
        index--;
        return index;
    }

    private void SetFillColor(int value)
    {
        int colorIndex = GetIndexOfColor(value);
        fillColor = GetComponent<Image>();
        fillColor.color = GameManagement.instance.colorsOfFill[colorIndex];
    }
}
