using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// initial design based on GameDevGuide video: https://www.youtube.com/watch?v=CGsEJToeXmA

public class FlexibleGridLayout : LayoutGroup
{

    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public int rows;
    public int cols;
    public Vector2 cellSize;
    public Vector2 spacing;
    public FitType fitType;

    public bool fitX, fitY;


    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = cols = Mathf.CeilToInt(sqrRt);
        }       
        
        if(fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)cols);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            cols = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)cols - (spacing.x / (float)cols) * 2 - (padding.left / (float)cols) - (padding.right / (float)cols);
        float cellHeight = parentHeight / (float)rows - (spacing.y / (float)rows)*2 - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int colCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / cols;
            colCount = i % cols;

            var childItem = rectChildren[i];

            float xPos = (cellSize.x * colCount) + (spacing.x * colCount) + padding.left; 
            float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(childItem, 0, xPos, cellSize.x);
            SetChildAlongAxis(childItem, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
