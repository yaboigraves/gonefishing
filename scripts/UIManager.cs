using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class UIManager : MonoBehaviour
{
    public static UIManager current;
    public CanvasGroup canvasGroupFader;
    public CanvasGroup linkGroup;
    bool fadingOut, linksFadingIn, fadingIn;
    public float maxFadeTime;
    public float maxLinkFadeInTime;

    public float maxFadeInTime;
    float fadeTime;
    float fadeInTime;

    public GameObject tankCam;



    //random reference to the tooltip for clicking the mouse
    public UIAnimate toolTipAnimator;


    void Awake()
    {

        current = this;
        fadingIn = true;
        fadeInTime = maxFadeInTime;

    }
    void Start()
    {
        //canvasGroupFader = GetComponent<CanvasGroup>();
        toggleButtonEnabled(false);
        tankCam.SetActive(false);
    }
    void Update()
    {

        if (fadingIn)
        {
            fadeInTime -= Time.deltaTime;
            canvasGroupFader.alpha = fadeInTime / maxFadeInTime;
            if (fadeInTime <= 0)
            {
                fadingIn = false;
            }
        }
        if (fadingOut)
        {
            fadeTime += Time.deltaTime;
            canvasGroupFader.alpha = fadeTime / maxFadeTime;

            if (fadeTime > maxFadeTime)
            {
                fadingOut = false;
                ShowLinks();
            }
        }

        if (linksFadingIn)
        {
            fadeInTime += Time.deltaTime;
            linkGroup.alpha = fadeInTime / maxLinkFadeInTime;

        }
    }

    public void Fade()
    {
        fadingOut = true;
    }


    public void ShowLinks()
    {
        //show links here
        linksFadingIn = true;
        toggleButtonEnabled(true);
        Cursor.visible = true;

        //need to also show the tank at some point

    }
    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    public void link(string link)
    {

        //Application.OpenURL(link);
        //Application.ExternalEval("window.open(" + link + ")");
        openWindow(link);
    }


    //toggle the buttons and un enable it 

    public void toggleButtonEnabled(bool enable)
    {

        //turn on the tank cam too


        GameObject linkCanvas = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        for (int i = 0; i < linkCanvas.transform.childCount; i++)
        {
            if (linkCanvas.transform.GetChild(i).GetComponent<Button>() != null)
            {
                linkCanvas.transform.GetChild(i).GetComponent<Button>().interactable = enable;
                linkCanvas.transform.GetChild(i).GetComponent<PressHandler>().interactable = enable;

            }
        }

        tankCam.SetActive(true);

    }


}
