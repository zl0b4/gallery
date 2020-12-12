using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageParse : MonoBehaviour
{
    public Image im;
    private const float HEIGHT = 292.1003f;
    private const float WIDTH = 526.393f;
    public string url;
    private RectTransform rectTransform;

    public IEnumerator Parse()
    {
        rectTransform = im.GetComponent<RectTransform>();
        using (WWW www = new WWW(url))
        {
            yield return www;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;
            im.material = renderer.material;
            if ((www.texture.width * HEIGHT) / www.texture.height < WIDTH)
                rectTransform.sizeDelta = new Vector2((www.texture.width * HEIGHT) / www.texture.height, HEIGHT);
            else rectTransform.sizeDelta = new Vector2(((www.texture.width * HEIGHT) / www.texture.height)*0.7f, HEIGHT*0.7f);
        }
    }
}
