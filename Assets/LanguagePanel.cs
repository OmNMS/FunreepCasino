using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePanel : MonoBehaviour
{
    [SerializeField] Button settingsButton;
    [SerializeField] Button okButton;

    void Start()
    {

    }

    public void SetLanguagePanel(bool value)
    {
        gameObject.SetActive(value);
    }
}
