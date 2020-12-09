using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginScript : MonoBehaviour
{
    public Sprite[] b = new Sprite[4];
    private InternetChecker checker;
    private DBManager db;
    public GameObject canvas;
    public GameObject panel;
    public Text loadText;
    public Image bgImage;
    public Image im;

    private void Start()
    {
        Params.buttons = b;
    }

    public void onStart()
    {
        checker = canvas.GetComponent<InternetChecker>();
        checker.Start();
        db = canvas.GetComponent<DBManager>();
        db.Start();
        if (!Params.IsInternetAvailable)
        {
            panel.SetActive(true);
            return;
        }
        bgImage.rectTransform.position = im.rectTransform.position;
       

        SceneManager.LoadScene(1);
    }

    public void onExit()
    {
        Application.Quit();
    }
}
