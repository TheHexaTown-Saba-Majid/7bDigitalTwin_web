using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingFeature : MonoBehaviour
{

    private AudioSource microphoneSource;

    public void Start()
    {
        NativeGallery.RequestPermission(NativeGallery.PermissionType.Write);
    }

    public void shareApp()
    {
        var payload = new NatSuite.Sharing.SharePayload();
        string toshare;


        //TODO ADD ID HERE IN IOS LINK 
#if UNITY_IPHONE
        toshare = "https://apps.apple.com/us/app/id1535379117";
#endif
#if UNITY_ANDROID
            toshare = "https://play.google.com/store/apps/details?id=com.Kaminlicht.AR";
#endif

        if (PlayerPrefs.GetString("Lang", "") == "En")
        {
            payload.AddText("Check out this AR App from KAMINLICHT.de: ");
            payload.Commit();
        }
        else 
        {
            payload.AddText("Schau dir diese AR App von KAMINLICHT.de an: ");
            payload.Commit();
        }


        

    }




    private IEnumerator HelloStart()
    {

#if UNITY_WEBGL
        Debug.Log("This is a WebGL build.");
#else
            Debug.Log("This is not a WebGL build.");


        // Start microphone
        microphoneSource = gameObject.AddComponent<AudioSource>();
        microphoneSource.mute =
        microphoneSource.loop = true;
        microphoneSource.bypassEffects =
        microphoneSource.bypassListenerEffects = false;
        microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
        microphoneSource.Play();

        MyOnDestroy();
#endif
        yield return 0;
    }

    private void MyOnDestroy()
    {

        #if UNITY_WEBGL
        Debug.Log("This is a WebGL build.");
#else
            Debug.Log("This is not a WebGL build.");

        // Stop microphone
        microphoneSource.Stop();
        Microphone.End(null);
#endif
    }
}
