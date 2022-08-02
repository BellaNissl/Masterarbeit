using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    [SerializeField] public static Button _selectedButton;
    [SerializeField] private Color _blueColour = Color.blue;
    [SerializeField] private Color _whiteColour = Color.white;
 
    public void ButtonClicked(Button button)
    {
        if (_selectedButton == button)
        {
            button.GetComponent<Image>().color = _whiteColour;
            _selectedButton = null;
        }
        else
        {
            if(_selectedButton != null)
            _selectedButton.GetComponent<Image>().color = _whiteColour;
            button.GetComponent<Image>().color = _blueColour;
            _selectedButton = button;
        }
    }
}
