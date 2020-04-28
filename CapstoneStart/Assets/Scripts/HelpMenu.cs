using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public HeadBob HeadBob;
    public HeadBob VoiceHeadBob;

    public GameObject KeyboardCamera; //Camera used for Manual controls
    public GameObject VoiceRecogCamera; //Camera used for Voice controls
    public GameObject CameraAnchorFirstPerson; //Anchor point for first person camera view
    public GameObject CameraAnchorThirdPerson; //Anchor point for third person camera view

    public GameObject ModelEyes;
    public GameObject ModelFace;
    public GameObject ModelHat;
    public GameObject ModelHair;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstPerson()
    {
        Debug.Log("First Person Button Clicked");
        KeyboardCamera.transform.position = CameraAnchorFirstPerson.transform.position;
        VoiceRecogCamera.transform.position = CameraAnchorFirstPerson.transform.position;
        ModelEyes.SetActive(false);
        ModelFace.SetActive(false);
        ModelHat.SetActive(false);
        ModelHair.SetActive(false);
        HeadBob.isFirstPerson = true;
        VoiceHeadBob.isFirstPerson = true;
    }

    public void ThirdPerson()
    {
        KeyboardCamera.transform.position = CameraAnchorThirdPerson.transform.position;
        VoiceRecogCamera.transform.position = CameraAnchorThirdPerson.transform.position;
        ModelEyes.SetActive(true);
        ModelFace.SetActive(true);
        ModelHat.SetActive(true);
        ModelHair.SetActive(true);
        HeadBob.isFirstPerson = false;
        VoiceHeadBob.isFirstPerson = false;
    }

    public void ManualControl()
    {
        KeyboardCamera.SetActive(true);
        VoiceRecogCamera.SetActive(false);
    }

    public void VoiceControl()
    {
        KeyboardCamera.SetActive(false);
        VoiceRecogCamera.SetActive(true);
    }
}
