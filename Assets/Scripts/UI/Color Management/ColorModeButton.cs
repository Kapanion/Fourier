using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorModeButton : MonoBehaviour
{
    public int darkModeIndex;
    public int lightModeIndex;

    [Space] public Image image;
    public Sprite darkModeIcon;
    public Sprite lightModeIcon;
    
    private bool darkMode = true;

    public void ChangeMode()
    {
        ColorManagerSingleton.Instance.SetProfile(darkMode ? lightModeIndex : darkModeIndex);
        image.sprite = darkMode ? darkModeIcon : lightModeIcon;
        darkMode = !darkMode;
    }
}
