using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnClick : MonoBehaviour
{
    public int column;
    public GameManager gm;

    private void OnMouseDown()
    {
        gm.ColumnSelect(column);
    }

    private void OnMouseOver()
    {
        gm.HoverColumn(column);
    }
}
