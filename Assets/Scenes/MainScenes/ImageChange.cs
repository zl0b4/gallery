using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ImageChange : MonoBehaviour
{
    public Button button;
    public Text info;
    public Image im;
    private InternetChecker checker;
    public GameObject panel;
    public ScrollRect scr;
    public GameObject canvas;
    private ImageParse parseScript;
    public Material material;

    List<string[]> currentGenre;

    private int counter = 0;

    private Vector3 zoom;

    private bool twoTouch = false;
    private bool oneTouch = true;

    //private readonly float width = 526.393f;
    //private readonly float height = 292.1003f;

    //private readonly float posX = 360.9525f;
    //private readonly float posY = 250.3576f;


    private void Update()
    {
        if (Input.touchCount == 2 && oneTouch)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            Vector3 prevMiddle = new Vector3((touchZeroPrevPos.x + touchOnePrevPos.x) / 2, (touchZeroPrevPos.y + touchOnePrevPos.y) / 2);
            Vector3 Middle = new Vector3((touchZero.position.x + touchOne.position.x) / 2, (touchZero.position.y + touchOne.position.y) / 2);
            
            Middle -= prevMiddle;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) / 100;

            if (zoom.x <= 1 && deltaMagnitudeDiff > 0) deltaMagnitudeDiff = 0;

            if (zoom.x >= 3 && deltaMagnitudeDiff < 0) deltaMagnitudeDiff = 0;

            zoom = new Vector3(zoom.x -= deltaMagnitudeDiff, zoom.y -= deltaMagnitudeDiff, 1);

            if (zoom.x >= 1)
            {
                im.transform.localScale = zoom;
                twoTouch = true;
            }
            im.transform.position = im.transform.position + Middle;
        }
        else if ((Input.touchCount == 1 || Input.touchCount == 2) && twoTouch)
        {
            Vector3 touchPrevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
            im.transform.position = im.transform.position +
                new Vector3(Input.GetTouch(0).position.x - touchPrevPos.x, Input.GetTouch(0).position.y - touchPrevPos.y);
            oneTouch = false;
        }
        else if (Input.touchCount == 0)
        {
            im.transform.localScale = new Vector3(1, 1);
            im.transform.localPosition = new Vector3(-107f, 6.899983f);
            zoom = new Vector3(1, 1, 1);
            twoTouch = false;
            oneTouch = true;
        }
    }

    private void Change()
    {
        checker = canvas.GetComponent<InternetChecker>();
        checker.Start();
        if (!Params.IsInternetAvailable)
        {
            panel.SetActive(true);
            return;
        }

        panel.SetActive(false);
        info.text = $"{currentGenre[counter][1]}\n{currentGenre[counter][0]}\n{currentGenre[counter][2]}";
        parseScript.im.material = material;
        parseScript.url = currentGenre[counter][3];
        parseScript.StartCoroutine(parseScript.Parse());
    }

    private void Start()
    {
        parseScript = canvas.GetComponent<ImageParse>();
        counter = 0;

        if (Params.zPressed)
        {
            button.image.sprite = Params.buttons[1];
            currentGenre = Params.pictures[1];
        }

        else if (Params.gPressed)
        {
            button.image.sprite = Params.buttons[2];
            currentGenre = Params.pictures[2];
        }

        else if (Params.sPressed)
        {
            button.image.sprite = Params.buttons[3];
            currentGenre = Params.pictures[3];
        }

        else if (Params.dPressed)
        {
            button.image.sprite = Params.buttons[0];
            currentGenre = Params.pictures[4];
        }

        Change();
    }
    public void onForwardClick()
    {
        scr.verticalNormalizedPosition = 1;
        counter++;

        if (counter == currentGenre.Count) counter = 0;

        Change();

    }
    public void onBackwardClick()
    {
        scr.verticalNormalizedPosition = 1;
        counter--;

        if (counter == -1) counter = currentGenre.Count - 1;

        Change();
    }
    public void onMenuClick()
    {
        Params.dPressed = false;
        Params.gPressed = false;
        Params.sPressed = false;
        Params.zPressed = false;
        Params.isMenuClicked = true;
        SceneManager.LoadScene(1);
    }
    public void onNextClick()
    {
        scr.verticalNormalizedPosition = 1;
        if (Params.zPressed)
        {
            Params.zPressed = false;
            Params.gPressed = true;
        }

        else if (Params.gPressed)
        {
            Params.gPressed = false;
            Params.sPressed = true;
        }

        else if (Params.sPressed)
        {
            Params.sPressed = false;
            Params.dPressed = true;
        }

        else if (Params.dPressed)
        {
            Params.dPressed = false;
            Params.zPressed = true;
        }

        Start();
    }
    public void onExit()
    {
        Application.Quit();
    }
}