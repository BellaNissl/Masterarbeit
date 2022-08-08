using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public void PvButtonClicked()
    {
        Tile selected = GridManager.Instance.GetSelectedTile();
        if (selected != null) {
            selected.BuildPV();
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }

    public void WindButtonClicked() {
        Tile selected = GridManager.Instance.GetSelectedTile();
        if (selected != null) {
            selected.BuildWind();
            GridManager.Instance.SelectTile(GridManager.INVALID);
        }
    }
}
