using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    public KeyCode uiToggleKey = KeyCode.P;
    public GameObject ui;

    public Text waveSpeedText;

    public FourierDrawer drawer;

    public Image showVectorsImage;
    public Sprite checkedSprite, uncheckedSprite;

    public Button noTrailButton, normalTrailButton, waveTrailButton;
    public Slider waveSpeedSlider;

    [ContextMenu("Toggle")]
    void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf) SetupUI();
    }

    void Start()
    {
        SetupUI();
    }

    void SetupUI()
    {
        waveSpeedSlider.value = drawer.waveLineSpeedMultiplier;
        var trailButton = drawer.trailMode switch
        {
            FourierDrawer.TrailMode.Trail => normalTrailButton,
            FourierDrawer.TrailMode.Wave => waveTrailButton,
            _ => noTrailButton
        };
        trailButton.onClick.Invoke();
        if ((showVectorsImage.sprite == uncheckedSprite) == drawer.displayVectors) ToggleVectors();
    }

    void Update()
    {
        if (Input.GetKeyDown(uiToggleKey)) Toggle();
    }

    public void OnWaveSpeedChanged(float value)
    {
        value = value.Round1();
        drawer.waveLineSpeedMultiplier = value;
        waveSpeedText.text = $"Wave Speed: {value}x";
    }

    public void OnTrailDisabled()
    {
        drawer.SetTrailMode(FourierDrawer.TrailMode.None);
    }

    public void OnNormalTrailEnabled()
    {
        drawer.SetTrailMode(FourierDrawer.TrailMode.Trail);
    }

    public void OnWaveTrailEnabled()
    {
        drawer.SetTrailMode(FourierDrawer.TrailMode.Wave);
    }

    public void ToggleVectors()
    {
        bool show = showVectorsImage.sprite == uncheckedSprite;

        showVectorsImage.sprite = show ? checkedSprite : uncheckedSprite;
        if (show) drawer.EnableVectors();
        else drawer.DisableVectors();
    }
}
