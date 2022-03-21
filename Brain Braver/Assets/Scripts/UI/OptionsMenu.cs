using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Options References")]
    [SerializeField] OptionsSO m_currentOptions = null;
    [SerializeField] OptionsSO m_defaultOptions = null;
    [SerializeField] Dropdown m_dragDropdown = null;
    [SerializeField] Slider m_offsetSlider = null;
    [SerializeField] Slider m_cameraSpeedSlider = null;
    [SerializeField] Slider m_cameraResizeSlider = null;
    //[SerializeField] Animator m_anim = null;

    public void Return()
    {

    }

    public void SetToDefault()
    {
        m_currentOptions.slideInput = m_defaultOptions.slideInput;
        m_currentOptions.cameraResizeSpeed = m_defaultOptions.cameraResizeSpeed;
        m_currentOptions.frontalCameraOffset = m_defaultOptions.frontalCameraOffset;
        m_currentOptions.cameraSpeed = m_defaultOptions.cameraSpeed;
        UpdateInterface();
    }

    private void Start()
    {
        UpdateInterface();
    }

    void UpdateInterface()
    {
        m_dragDropdown.value = (int)m_currentOptions.slideInput;
        m_offsetSlider.value = m_currentOptions.frontalCameraOffset;
        m_cameraSpeedSlider.value = m_currentOptions.cameraSpeed;
        m_cameraResizeSlider.value = m_currentOptions.cameraResizeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
