using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class Params
{
    public static bool IsInternetAvailable;
    public static Sprite[] buttons;
    public static string searchChoice;

    public static bool genresGot;
    public static bool picturesGot;

    public static Dictionary<int, List<string[]>> pictures = new Dictionary<int, List<string[]>>();

    public static bool isMenuClicked;

    public static List<string> res = new List<string>();
    public static List<Dropdown.OptionData> resForDropDown = new List<Dropdown.OptionData>();

    public static bool zPressed, gPressed, sPressed, dPressed;
}
