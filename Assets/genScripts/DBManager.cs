using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    public void Start()
    {
        if (!Params.picturesGot && !Params.genresGot)
        {
            StartCoroutine(GetGenres());
            StartCoroutine(GetPictures());
        }
    }

    private IEnumerator GetGenres()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://z76383.hostru11.fornex.host/zlobaplay.com/getGenres.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ошибка: " + www.error);
            yield break;
                
        }
        string genresString = www.text;
        for (int i = 0; i < genresString.Split('\n').Length-1; i++)
        {
            Params.pictures.Add(int.Parse(genresString.Split('\n')[i].Split('/')[0].ToString()), new List<string[]>());
        }
        Params.genresGot = true;
    }
    private IEnumerator GetPictures()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://z76383.hostru11.fornex.host/zlobaplay.com/getPictures.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ошибка: " + www.error);
            yield break;

        }
        string picturesString = www.text;
        var genreSep = picturesString.Split('↨');
        for (int i = 1; i <= Params.pictures.Count; i++)
        {
            string[] genrePictures = genreSep[i - 1].Split('↹');
            for (int j = 0; j < genrePictures.Length - 1; j++)
                Params.pictures[i].Add(new string[] { genrePictures[j].Split('↜')[0], genrePictures[j].Split('↜')[3], genrePictures[j].Split('↜')[1], genrePictures[j].Split('↜')[2] });
        }
        Params.picturesGot = true;
    }

}
