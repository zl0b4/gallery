using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageParse : MonoBehaviour
{
    public Image im;
    private const float HEIGHT = 292.1003f;
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
            rectTransform.sizeDelta = new Vector2((www.texture.width * HEIGHT) / www.texture.height, HEIGHT);
        }
    }



}
