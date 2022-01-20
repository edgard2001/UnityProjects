using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleColor : MonoBehaviour
{

    [SerializeField] private Button _button;
    private bool _selected = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_button == null) _button = gameObject.GetComponent<Button>();
        ColorBlock newColors = new ColorBlock();
        newColors.normalColor = Color.white;
        newColors.pressedColor = Color.white;
        newColors.selectedColor = Color.white;
        newColors.highlightedColor = Color.white;
        newColors.disabledColor = Color.white;
        newColors.colorMultiplier = 1f;
        _button.colors = newColors;
    }

    public void ToggleColor()
    {
        ColorBlock newColors = new ColorBlock();
        if (_selected)
        {
            newColors.normalColor = Color.white;
            newColors.pressedColor = Color.white;
            newColors.selectedColor = Color.white;
            newColors.highlightedColor = Color.white;
            newColors.disabledColor = Color.white;
            newColors.colorMultiplier = 1f;
        }
        else
        {
            newColors.normalColor = Color.grey;
            newColors.pressedColor = Color.grey;
            newColors.selectedColor = Color.grey;
            newColors.highlightedColor = Color.grey;
            newColors.disabledColor = Color.grey;
            newColors.colorMultiplier = 1f;
        }
        _button.colors = newColors;
        _selected = !_selected;
    }
}
