using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public InputField searchInput;
    public Dropdown searchResults;
    private InternetChecker checker;
    public GameObject panel;
    public GameObject canvas;
    public ScrollRect resultsScroll;
    private List<string> results = new List<string>();

    private void Update()
    {
        if (searchResults.value != 0 && searchResults.options[1].text != "Нет результатов")
        {
            Params.searchChoice = results[searchResults.value - 1];
            SceneManager.LoadScene(3);   
        }
    }
    private void Start()
    {
        searchResults.options.Clear();
        if (Params.res != null && !Params.isMenuClicked)
        {
            searchResults.options = Params.resForDropDown;
            results = Params.res;
            searchResults.Show();
        }
        Params.isMenuClicked = false;
    }

    public void onSearchClick()
    {
        checker = canvas.GetComponent<InternetChecker>();
        checker.Start();
        if (!Params.IsInternetAvailable)
        {
            panel.SetActive(true);
            return;
        }
        panel.SetActive(false);
        searchResults.options.Clear();
        Params.res?.Clear();
        Params.resForDropDown?.Clear();

        searchResults.options.Add(new Dropdown.OptionData(string.Empty));

        if (searchInput.text != string.Empty)
        {
            for (int i = 1; i <= Params.pictures.Keys.Count; i++)
            {
                for (int j = 0; j < Params.pictures[i].Count; j++)
                {
                    if (Params.pictures[i][j].Any(str => str.ToLower().Contains(searchInput.text.ToLower())))
                    {
                        searchResults.options.Add(new Dropdown.OptionData($"{Params.pictures[i][j][0]}.\n{Params.pictures[i][j][1]}"));
                        results.Add(Params.pictures[i][j][0]);
                    }                    
                }
            }
        }
        searchResults.value = -1;

        if (searchResults.options.Count == 1)
        {
            searchResults.options.Add(new Dropdown.OptionData("Нет результатов"));
        }

        //resultsScroll.verticalNormalizedPosition = -200;
        searchResults.Show();
    }

    public void onButtonZClick()
    {
        SceneManager.LoadScene(2);
        Params.zPressed = true;
    }
    public void onButtonGClick()
    {
        SceneManager.LoadScene(2);
        Params.gPressed = true;
    }
    public void onButtonSClick()
    {
        SceneManager.LoadScene(2);
        Params.sPressed = true;
    }
    public void onButtonDClick()
    {
        SceneManager.LoadScene(2);
        Params.dPressed = true;
    }
    public void onExit()
    {
        Application.Quit();
    }
}