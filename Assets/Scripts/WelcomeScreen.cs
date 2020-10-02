using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField]
    CanvasGroupFader _CanvasGroupFader;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);

        yield return _CanvasGroupFader.FadeSceneOut();

        yield return new WaitForSeconds(2.0f);

        yield return _CanvasGroupFader.FadeSceneIn();
    }

}
