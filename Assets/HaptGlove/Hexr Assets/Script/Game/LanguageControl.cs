using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LanguageControl : MonoBehaviour
{
    public TextMeshPro KeypadGrabDemo, KeypadGrabDrum, KeypadSlingShot, KeypadMedical, KeypadEducation, KeypadPuzzle, KeypadWateringPlant;
    public TextMeshProUGUI PanelHexRLeft, PanelHexRRight, PanelWearingGuide, PanelLanguageChange, PanelTitle;
    public TextMeshProUGUI BasicTennisPrimary, BasicTennisSecondary, BasicKeyPrimary, BasicKeySecondary, BasicTorchPrimary, BasicTorchSecondary, BasicCupPrimary, BasicCupSecondary;
    public TextMeshProUGUI MedicalPulsePri, MedicalPulseSec, MedicalPalpationPri, MedicalPalpationSec, MedicalPulsePulseTypePri, MedicalPulseBodyRegion;
    public TextMeshPro MedicalTogglePulseType, LongTableResetToggle;
    public TextMeshProUGUI LightBulbPuzzleText;
    public TextMeshProUGUI SolarSysDescriptionPri, SolarSysDescriptionSec, SolarSysDiameterPri, SolarSysDiameterSec, SolarSysAtmospherePri, SolarSysAtmosphereSec;
    public bool IsEnglish = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Call the function when "P" is pressed
            LanguageToggle();
        }
    }
    public void LanguageToggle()
    {
        if (IsEnglish == true)
        {
            IsEnglish = false;
        }
        else
        {
            IsEnglish = true;
        }
        KeypadTextLanguageToggle();
        //HexRControlPanelTextLanguageToggle();
        BasicDemoSceneTextLangugaeToggle();
        MedicalDemoSceneTextLanguageToggle();
        PlanetSceneTextLanguageToggle();
    }
    private void KeypadTextLanguageToggle()
    {
        if(IsEnglish == true)
        {
            KeypadGrabDemo.text = "Grab Demo";
            KeypadGrabDrum.text = "Drum";
            KeypadSlingShot.text = "Slingshot and Pong";
            KeypadMedical.text = "Medical Training";
            KeypadEducation.text = "Education";
            KeypadPuzzle.text = "Puzzle";
            KeypadWateringPlant.text = "Watering Plant";
        }
        else if (IsEnglish == false)
        {
            KeypadGrabDemo.text = "基本演示";
            KeypadGrabDrum.text = "鼓";
            KeypadSlingShot.text = "弹弓和乒乓";
            KeypadMedical.text = "医学培训";
            KeypadEducation.text = "星球教育";
            KeypadPuzzle.text = "智力游戏";
            KeypadWateringPlant.text = "浇水模拟";
        }
    }
    private void HexRControlPanelTextLanguageToggle()
    {
        if (IsEnglish == true)
        {
            PanelTitle.text = "HexR Control Panel";
            PanelHexRLeft.text = "Connect To HexR Left";
            PanelHexRRight.text = "Connect To HexR Right";
            PanelWearingGuide.text = "HexR Wearing Guide";
            PanelLanguageChange.text = "Change Language";
        }
        else if (IsEnglish == false)
        {
            PanelTitle.text = "HexR 控制面板";
            PanelHexRLeft.text = "连接到 HexR 左侧";
            PanelHexRRight.text = "连接到 HexR 右侧";
            PanelWearingGuide.text = "HexR 穿戴指南";
            PanelLanguageChange.text = "更改语言";
        }
    }
    private void BasicDemoSceneTextLangugaeToggle()
    {
        if (IsEnglish == true)
        {
            BasicTennisPrimary.text = "Squishy";
            BasicTennisSecondary.text = "Squish from pressure.";
            BasicKeyPrimary.text = "Pinch Grab";
            BasicKeySecondary.text = "Dynamic constant pressure.";
            BasicTorchPrimary.text = "Hand Grab";
            BasicTorchSecondary.text = "Stable constant pressure.";
            BasicCupPrimary.text = "Breakable";
            BasicCupSecondary.text = "Break from pressure.";
            LongTableResetToggle.text = "Reset";
            LightBulbPuzzleText.text = "Can you light up all the bulb?";
        }
        else if (IsEnglish == false)
        {
            BasicTennisPrimary.text = "挤压的";
            BasicTennisSecondary.text = "因压力而被挤压";
            BasicKeyPrimary.text = "捏抓";
            BasicKeySecondary.text = " 动态恒压";
            BasicTorchPrimary.text = "手抓";
            BasicTorchSecondary.text = "稳定恒压";
            BasicCupPrimary.text = "易碎的";
            BasicCupSecondary.text = "因压力而断裂";
            LongTableResetToggle.text = "重置";
            LightBulbPuzzleText.text = "你能点亮所有的灯泡吗？";
        }
    }
    private void MedicalDemoSceneTextLanguageToggle()
    {
        if (IsEnglish == true)
        {
            MedicalPulsePri.text = "Palpation of the pulse";
            MedicalPulseSec.text = "You may place your index and middle finger on the patient wrist and neck to feel his pulse.";
            MedicalPalpationPri.text = "Palpation of abdomen";
            MedicalPalpationSec.text = "You may press into the patient body and perform an abdomen check up.";
            MedicalTogglePulseType.text = "Toggle Pulse Type";
            MedicalPulsePulseTypePri.text = "Current Pulse";
            MedicalPulseBodyRegion.text = "Body Region";
        }
        else if (IsEnglish == false)
        {
            MedicalPulsePri.text = "脉搏触诊";
            MedicalPulseSec.text = "您可以将食指和中指放在病人的手腕和脖子上，以感受他的脉搏。";
            MedicalPalpationPri.text = "腹部触诊";
            MedicalPalpationSec.text = "您可以按压病人的身体，进行腹部检查。";
            MedicalTogglePulseType.text = "切换脉搏类型";
            MedicalPulsePulseTypePri.text = "脉搏类型";
            MedicalPulseBodyRegion.text = "腹部区域";
        }
    }
    private void PlanetSceneTextLanguageToggle()
    {

        if (IsEnglish == true)
        {
            SolarSysDescriptionPri.text = "Solar System";
            SolarSysDescriptionSec.text = "The solar system is a vast and complex system centered around the Sun, a medium-sized star.";
            SolarSysDiameterPri.text = "Diameter";
            SolarSysDiameterSec.text = "-";
            SolarSysAtmospherePri.text = "Atmosphere";
            SolarSysAtmosphereSec.text = "-";
        }
        else if (IsEnglish == false)
        {
            SolarSysDescriptionPri.text = "太阳系";
            SolarSysDescriptionSec.text = "太阳系是一个庞大而复杂的系统，以太阳为中心，太阳是一颗中等大小的恒星。";
            SolarSysDiameterPri.text = "直径 ";
            SolarSysDiameterSec.text = "-";
            SolarSysAtmospherePri.text = "大气";
            SolarSysAtmosphereSec.text = "-";
        }
    }
}
