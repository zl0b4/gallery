using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resultImage : MonoBehaviour
{
    private Vector3 zoom;
    public Material material;

    private bool twoTouch = false;
    private bool oneTouch = true;

    public GameObject canvas;
    public GameObject panel;
    private InternetChecker checker;
    private ImageParse parseScript;

    public Image im;
    public Text info;

    public void Start()
    {
        checker = canvas.GetComponent<InternetChecker>();
        checker.Start();
        if (!Params.IsInternetAvailable)
        {
            panel.SetActive(true);
            return;
        }
        panel.SetActive(false);
        parseScript = canvas.GetComponent<ImageParse>();
        parseScript.im.material = material;
        for (int i = 1; i <= Params.pictures.Keys.Count; i++)
        {
            for (int j = 0; j < Params.pictures[i].Count; j++)
            {
                if (Params.pictures[i][j][0] == Params.searchChoice)
                {
                    parseScript.url = Params.pictures[i][j][3];
                    info.text = $"{Params.pictures[i][j][1]}\n{Params.pictures[i][j][0]}\n{Params.pictures[i][j][2]}";
                    break;
                }
            }
        }

        parseScript.StartCoroutine(parseScript.Parse());
    }

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
    public void onBackClick()
    {
        SceneManager.LoadScene(1);
    }
    public void onExit()
    {
        Application.Quit();
    }

}
