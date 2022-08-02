using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _selection;

    [SerializeField] private Color _pvColor;
    [SerializeField] private Color _windColor;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter(){
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    void OnMouseDown(){
        Button selected = ButtonSelection._selectedButton;
        SpriteRenderer selectionRenderer = _selection.GetComponent<SpriteRenderer>();

        if(selected != null) {
            if(selected == GameLogic.Instance._pvButton) {
                selectionRenderer.color = _pvColor;
            } else if(selected == GameLogic.Instance._windButton) {
                selectionRenderer.color = _windColor;
            } else {
                selectionRenderer.color = Color.white;
            }

            _selection.SetActive(true);
        }
    }
}
