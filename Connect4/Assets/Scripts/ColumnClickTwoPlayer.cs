using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnClickTwoPlayer : MonoBehaviour
{
    public int column;
    public GameManagerTwoPlayer gm;

    private void OnMouseDown()
    {
        gm.ColumnSelect(column);
    }

    private void OnMouseOver()
    {
        gm.HoverColumn(column);
    }
}
