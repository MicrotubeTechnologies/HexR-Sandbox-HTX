using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlanetDescription : MonoBehaviour
{
    public TextMeshProUGUI PrimaryText, SecondaryText, Diameter, Composition;
    public LanguageControl languagecontrol;

    public void Sun()
    {
        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "The Sun";
            SecondaryText.text = "The Sun is a star located at the center of our solar system. It is a massive, glowing sphere of hot gases, primarily hydrogen and helium, undergoing nuclear fusion. ";
            Diameter.text = "1.39 million km";
            Composition.text = "91% Hydrogen, 9% Helium";
        }
        else
        {
            PrimaryText.text = "太阳";
            SecondaryText.text = "太阳是一颗位于我们太阳系中心的恒星。它是一个巨大的、发光的热气体球体，主要由氢和氦组成，正在进行核聚变。";
            Diameter.text = "139万公里 ";
            Composition.text = "91% 氢，9% 氦";
        }
    }
    public void Mercury()
    {

        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "Mercury";
            SecondaryText.text = "Mercury is the smallest planet in our solar system and the closest one to the Sun. It has a rocky surface, similar to Earth's Moon.";
            Diameter.text = "4,880 km";
            Composition.text = "Oxygen, Sodium, Hydrogen, Helium, Potassium";
        }
        else
        {
            PrimaryText.text = "水星 ";
            SecondaryText.text = "水星是我们太阳系中最小的行星，也是离太阳最近的行星。它的表面是岩石状的，类似于地球的月球。";
            Diameter.text = "4,880 公里";
            Composition.text = "氧 , 钠, 氢, 氦, 钾";
        }
    }
    public void Venus()
    {
        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "Venus";
            SecondaryText.text = "Venus is the second planet from the Sun and is often referred to as Earth's sister planet due to its similar size and composition.";
            Diameter.text = "12,104 km";
            Composition.text = "96% Carbon Dioxide, 3% Nitrogen";
        }
        else
        {
            PrimaryText.text = "金星";
            SecondaryText.text = "金星是离太阳第二远的行星，由于其大小和成分与地球相似，常被称为地球的姐妹行星。";
            Diameter.text = "12,104 公里";
            Composition.text = "96% 二氧化碳，3% 氮气";
        }

    }
    public void Earth()
    {
        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "Earth";
            SecondaryText.text = "Earth is the third planet from the Sun and the only known celestial body to support life.";
            Diameter.text = "12,742 km";
            Composition.text = "78% Nitrogen, 21% Oxygen";
        }
        else
        {
            PrimaryText.text = "地球";
            SecondaryText.text = "地球是离太阳第三远的行星，也是已知唯一支持生命的天体。";
            Diameter.text = "12,742 公里";
            Composition.text = "78% 氮气，21% 氧气";
        }
    }
    public void Mars()
    {
        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "Mars";
            SecondaryText.text = "Mars is the fourth planet from the Sun and is often referred to as the Red Planet due to its reddish appearance, which is a result of iron oxide (rust) on its surface.";
            Diameter.text = "6,779 km";
            Composition.text = "95% Carbon Dioxide, 3% Nitrogen";
        }
        else
        {
            PrimaryText.text = "火星";
            SecondaryText.text = "火星是离太阳第四远的行星，因其红色外观而常被称为红色星球，这种外观是由于表面的氧化铁（锈）所致。";
            Diameter.text = "6,779 公里";
            Composition.text = "95% 二氧化碳，3% 氮气";
        }
    }
    public void Jupiter()
    {
        if (languagecontrol.IsEnglish == true)
        {
            PrimaryText.text = "Jupiter";
            SecondaryText.text = "Jupiter is the fifth planet from the Sun and is called the “King of the Planets” due to its size and dominant presence in the solar system.";
            Diameter.text = "139,820 km";
            Composition.text = "90% Hydrogen, 10% Helium";
        }
        else
        {
            PrimaryText.text = "木星";
            SecondaryText.text = "木星是离太阳第五远的行星，由于其体积巨大和在太阳系中的主导地位，常被称为“行星之王”。";
            Diameter.text = "139,820 公里";
            Composition.text = "90% 氢，10% 氦";
        }

    }
}
