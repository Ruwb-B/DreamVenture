using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioMixer audioMixer;

    public Toggle fullscreenTog;

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    private void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;
            UpdateResLabel();
        }
    }

    public void Quit()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResLeft()
    {
        --selectedResolution;
        if (selectedResolution < 0)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        ++selectedResolution;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + "x" + resolutions[selectedResolution].vertical.ToString();
    }

    public void ChangeSoundVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void ChangeVFXVolume(float volume)
    {
        audioMixer.SetFloat("VFX", volume);
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
