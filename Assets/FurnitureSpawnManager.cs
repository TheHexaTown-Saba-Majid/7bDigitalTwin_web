using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;
    public GameObject GameobjectToCreate;
    public GameObject ScrollViewHorizontal;
    public Text textShow;
    [SerializeField]
    private GameObject featherObject;
    private string debugging;


    public GameObject[] scrollBars;

    /*
  [SerializeField]
     private GameObject moveCameraUI;

     [SerializeField]
     private GameObject tapToPlaceUI;

       */
 
    public bool MoveMode = false;
     public bool RotateMode = false;
     public bool PinchMode = false;
     public bool LockMode = true;



     //public GameObject tap_to_place;
     //public GameObject Move_Ipad;

     public Toggle T_MoveMode;
     public Toggle T_RotateMode;
     public Toggle T_PinchMode;
     public Toggle T_LockMode;

     public GameObject ToggleBtn;
    
     public GameObject BackBtn;
     public GameObject logo;
     public void Setmovemode(bool mode)
     {
         MoveMode = mode;
     }

     public void SetRotatemode(bool mode)
     {
         RotateMode = mode;
     }


     public void SetPinchmode(bool mode)
     {
         PinchMode = mode;

     }

     public void SetLockMode(bool mode)
     {
         LockMode = mode;
     }
    
    public GameObject Placedprefab
    {
        get
        {
            return GameobjectToCreate;
        }
        set
        {
            GameobjectToCreate = value;
        }
    }
   
    ARRaycastManager arRaycastManager;
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = gameObject.GetComponent<ARRaycastManager>();



        /*

        BackBtn.SetActive(true);
        logo.SetActive(false);
      
         name = PlayerPrefs.GetString("TargetObjectName");
        {
            ChangePrefabSelection(name);
        }
         // PlaceObject();
        debugging = "Aaa";
        textShow.text = debugging;
        */
    }
    IEnumerator DelayedExecution()
    {
        string targetObjectName = PlayerPrefs.GetString("TargetObjectName");

        foreach (GameObject scrollBar in scrollBars)
        {
            if (scrollBar.name == targetObjectName)
            {
                scrollBar.SetActive(true);
                ScrollViewHorizontal = GameObject.Find(targetObjectName); // Assigning the found GameObject\

                name = PlayerPrefs.GetString("TargetObjectName");
                {
                    ChangePrefabSelection(name);
                }
              PlaceObject();
            }
            else
            {
             scrollBar.SetActive(false);
            }
        }

        yield return new WaitForSeconds(2f);
    }
    public void PlaceObject()
    {
        GameObject parentObj = GameobjectToCreate;

        for (int i = 0; i < parentObj.transform.childCount; i++)
        {
            GameObject childObj = parentObj.transform.GetChild(i).gameObject;

            if (childObj.name == name)
            {
                childObj.SetActive(true);
            }
            else
            {
                childObj.SetActive(false);
            }
        }
    }


    public void SelectModel(GameObject currentModel)
    {
        for (int i = 0; i < Placedprefab.transform.childCount; i++)
        {
            GameObject childObj = Placedprefab.transform.GetChild(i).gameObject;

            if (childObj.name == currentModel.name)
            {
                childObj.SetActive(true);
            }
            else
            {
                childObj.SetActive(false);
            }
        }
    }
    bool TryGetTouchPosition(out Vector2 touchposition)
    {
        if (Input.touchCount > 0)
        {
            touchposition = Input.GetTouch(0).position;
            return true;
        }
        {
            touchposition = default;
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        debugging = "Update called";
        textShow.text = debugging;

#if UNITY_EDITOR
        if (!instantiated)
        {
            debugging = "Instantiating prefab";
            textShow.text = debugging;

            StartCoroutine(DelayedExecution());
            Placedprefab = Instantiate(Placedprefab);
            instantiated = true;
            ToggleBtn.SetActive(true);
        }
#endif

        if (Placedprefab != null)
        {
            debugging = "Placedprefab is not null";
            textShow.text = debugging;

            if (Placedprefab.GetComponent<LeanTwistRotateAxis>() != null)
            {
                Placedprefab.GetComponent<LeanTwistRotateAxis>().enabled = RotateMode;
            }
            else
            {
                debugging = "LeanTwistRotateAxis component is missing";
                textShow.text = debugging;
            }

            if (Placedprefab.GetComponent<LeanPinchScale>() != null)
            {
                Placedprefab.GetComponent<LeanPinchScale>().enabled = PinchMode;
            }
            else
            {
                debugging = "LeanPinchScale component is missing";
                textShow.text = debugging;
            }

            if (Placedprefab.GetComponent<PlacementObject>() != null)
            {
                Placedprefab.GetComponent<PlacementObject>().Locked = LockMode;
            }
            else
            {
                debugging = "PlacementObject component is missing";
                textShow.text = debugging;
            }
        }
        else
        {
            debugging = "Placedprefab is null";
            textShow.text = debugging;
        }


        UpdatePlacementIndicator();
        if (!TryGetTouchPosition(out Vector2 touchposition))
            return;

        debugging = "b";
        textShow.text = debugging;


        debugging = "C";
        textShow.text = debugging;
        if (!instantiated)
        {
            StartCoroutine(DelayedExecution());
        }
        debugging = "C1";
        textShow.text = debugging;

       if (arRaycastManager.Raycast(touchposition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            debugging = "C2";
            textShow.text = debugging;

            var hitpose = hits[0].pose;
            debugging = "C3";
            textShow.text = debugging;
            if (!instantiated && !IsPointerOverUIObject())
            {

               
                debugging = "D";
                textShow.text = debugging;

              
                Placedprefab = Instantiate(Placedprefab, hitpose.position, hitpose.rotation);
                instantiated = true;
                ToggleBtn.SetActive(true);
                gameObject.GetComponent<ARPlaneManager>().enabled = false;
                gameObject.GetComponent<ARPointCloudManager>().enabled = false;

                debugging = "E";
                textShow.text = debugging;

                foreach (ARPlane plane in gameObject.GetComponent<ARPlaneManager>().trackables)
                {
                    plane.gameObject.SetActive(false);

                }
                foreach (ARPointCloud point in gameObject.GetComponent<ARPointCloudManager>().trackables)
                {
                    point.gameObject.SetActive(false);
                }
            }
            else if (instantiated && MoveMode && !RotateMode && !IsPointerOverUIObject())
            {
                Placedprefab.transform.position = hitpose.position;
                UnityEngine.Debug.Log("hit to replace");
            }

        }
    }

    void UpdatePlacementIndicator()
    {
        if (!instantiated && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            debugging = "F";
            textShow.text = debugging;
            var hits = new List<ARRaycastHit>();
            //moveCameraUI.SetActive(true);
            //  tapToPlaceUI.SetActive(false);
            UnityEngine.Debug.Log("move ipad  check");
            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                if (featherObject.activeInHierarchy)
                {
                    UnityEngine.Debug.Log("tap to place");
                }
                else
                {
                    // moveCameraUI.SetActive(false);
                    // tapToPlaceUI.SetActive(true);

                    UnityEngine.Debug.Log("move ipad");
                }
            }
        }
    }
    bool instantiated = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // UI Ignoring Code Snipet

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    public void ChangePrefabSelection(string name)
    {
        if (ScrollViewHorizontal != null && ScrollViewHorizontal.name == "Scroll-Snap-Plants")
        {
            GameObject loadedGameObject = Resources.Load<GameObject>($"Prefabscheck/{"Plants"}");
            if (loadedGameObject != null)
            {
                GameobjectToCreate = loadedGameObject;
                UnityEngine.Debug.Log($"Game object with name {name} was loaded");
            }
            else
            {
                UnityEngine.Debug.Log($"Unable to find a game object with name {name}");
            }
        }
        else
        {
            UnityEngine.Debug.Log("The target object either doesn't exist or doesn't have the desired name.");
        }
        if (ScrollViewHorizontal != null && ScrollViewHorizontal.name == "Scroll-Snap-DiningSet")
        {
            GameObject loadedGameObject = Resources.Load<GameObject>($"Prefabscheck/{"Dining_table"}");
            if (loadedGameObject != null)
            {
                GameobjectToCreate = loadedGameObject;
                UnityEngine.Debug.Log($"Game object with name {name} was loaded");
            }
            else
            {
                UnityEngine.Debug.Log($"Unable to find a game object with name {name}");
            }
        }
        else
        {
            UnityEngine.Debug.Log("The target object either doesn't exist or doesn't have the desired name.");
        }
        if (ScrollViewHorizontal != null && ScrollViewHorizontal.name == "Scroll-Snap-Sofa")
        {
            GameObject loadedGameObject = Resources.Load<GameObject>($"Prefabscheck/{"Sofa"}");
            if (loadedGameObject != null)
            {
                GameobjectToCreate = loadedGameObject;
                UnityEngine.Debug.Log($"Game object with name {name} was loaded");
            }
            else
            {
                UnityEngine.Debug.Log($"Unable to find a game object with name {name}");
            }
        }
        else
        {
            UnityEngine.Debug.Log("The target object either doesn't exist or doesn't have the desired name.");
        }

        if (ScrollViewHorizontal != null && ScrollViewHorizontal.name == "Scroll-Snap-Lamps")
        {
            GameObject loadedGameObject = Resources.Load<GameObject>($"Prefabscheck/{"Lamps"}");
            if (loadedGameObject != null)
            {
                GameobjectToCreate = loadedGameObject;
                UnityEngine.Debug.Log($"Game object with name {name} was loaded");
            }
            else
            {
                UnityEngine.Debug.Log($"Unable to find a game object with name {name}");
            }
        }
        else
        {
            UnityEngine.Debug.Log("The target object either doesn't exist or doesn't have the desired name.");
        }
    }
}
