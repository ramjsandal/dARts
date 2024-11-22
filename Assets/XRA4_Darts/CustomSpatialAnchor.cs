using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpatialAnchor : MonoBehaviour
{
    public string anchorName = "frame";
    List<OVRSpatialAnchor.UnboundAnchor> _unboundAnchors = new();

    private void Start()
    {
        LoadAnchor();
    }
    public void AddAnchor()
    {
        OVRSpatialAnchor anchor;
        anchor = transform.gameObject.AddComponent<OVRSpatialAnchor>();
        StartCoroutine(AnchorAdded(anchor));
    }

    private IEnumerator AnchorAdded(OVRSpatialAnchor anchor)
    {
        yield return new WaitUntil(() => anchor.Created && anchor.Localized);

        SaveAnchor(anchor);
    }

    public async void SaveAnchor(OVRSpatialAnchor anchor)
    {
        var result = await anchor.SaveAnchorAsync();

        SaveAnchorToPlayerPrefs(anchor.Uuid.ToString());
    }

    public void SaveAnchorToPlayerPrefs(string uuidString)
    {
        PlayerPrefs.SetString(anchorName, uuidString);
    }

    public void LoadAnchor()
    {
        if (PlayerPrefs.HasKey(anchorName))
        {
            string uuidString = PlayerPrefs.GetString(anchorName);
            var uuids = new Guid[1];
            uuids[0] = new Guid(uuidString);

            LoadAnchorsByUuid(uuids);
        }
    }

    async void LoadAnchorsByUuid(IEnumerable<Guid> uuids)
    {
        // Step 1: Load
        var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, _unboundAnchors);

        if (result.Success)
        {
            Debug.Log($"Anchors loaded successfully.");

            // Note result.Value is the same as _unboundAnchors
            foreach (var unboundAnchor in result.Value)
            {
                // Step 2: Localize
                unboundAnchor.LocalizeAsync().ContinueWith((success, anchor) =>
                {
                    if (success)
                    {
                        // Create a new game object with an OVRSpatialAnchor component
                        //var spatialAnchor = new GameObject($"Anchor {unboundAnchor.Uuid}")
                        //.AddComponent<OVRSpatialAnchor>();
                        var spatialAnchor = transform.gameObject.AddComponent<OVRSpatialAnchor>();

                        // Step 3: Bind
                        // Because the anchor has already been localized, BindTo will set the
                        // transform component immediately.
                        unboundAnchor.BindTo(spatialAnchor);
                    }
                    else
                    {
                        Debug.LogError($"Localization failed for anchor {unboundAnchor.Uuid}");
                    }
                }, unboundAnchor);
            }
        }
        else
        {
            Debug.LogError($"Load failed with error {result.Status}.");
        }

    }

    public void EraseAnchor()
    {
        if (GetComponent<OVRSpatialAnchor>())
        {
            var anchor = GetComponent<OVRSpatialAnchor>();
            EraseAnchorAsync(anchor);
            Destroy(GetComponent<OVRSpatialAnchor>());
        }

    }
    async void EraseAnchorAsync(OVRSpatialAnchor _spatialAnchor)
    {
        var result = await _spatialAnchor.EraseAnchorAsync();
        if (result.Success)
        {
            Debug.Log($"Successfully erased anchor.");
            PlayerPrefs.DeleteKey(anchorName);
        }
        else
        {
            Debug.LogError($"Failed to erase anchor {_spatialAnchor.Uuid} with result {result.Status}");
        }
    }
}
