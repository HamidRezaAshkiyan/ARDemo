using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    //private ARSessionOrigin arOrigin;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, PlacementPose.position, Quaternion.identity);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;

            // var cameraForward = Camera.current.transform.forward;
            // var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            // PlacementPose.rotation = Quanternion.LookRotation(cameraBearing);
        }
    }
}























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.Experimental.XR;

// public class ARTapToPlaceObject : MonoBehaviour
// {
//     public GameObject placementIndicator;
//     private ARSessionOrigin arOrigin;
//     private ARRaycastManager aRRaycastManager;
//     private Pose placementPose;
//     private bool placementPoseIsValid = false;

//     void Start()
//     {
//         arOrigin = FindObjectOfType<ARSessionOrigin>();
//     }

//     void Update()
//     {
//         UpdatePlacementPose();
//         UpdatePlacementIndicator();
//     }

//     private void UpdatePlacementIndicator()
//     {
//         if (placementPoseIsValid)
//         {
//             placementIndicator.SetActive(true);
//             placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
//         }
//         else
//         {
//             placementIndicator.SetActive(false);
//         }
//     }

//     private void UpdatePlacementPose()
//     {
//         var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
//         var hits = new List<ARRaycastHit>();

//         aRRaycastManager.Raycast(screenCenter, hits);

//         placementPoseIsValid = hits.Count > 0;
//         if (placementPoseIsValid)
//         {
//             placementPose = hits[0].pose;
//         }
//     }
// }
