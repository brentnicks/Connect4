using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnClickVsAi : MonoBehaviour
{
    public int column;
    public GameManagerVsAi gm;

    private void OnMouseDown()
    {
        gm.ColumnSelect(column);
    }

    private void OnMouseOver()
    {
        gm.HoverColumn(column);
    }
}
