using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DummyInEditor : MonoBehaviour
{
    public BoxCollider[] GO;
    public MeshRenderer[] MR;
#if UNITY_EDITOR
    void Update()
    {
        GO = FindObjectsOfType<BoxCollider>();
        MR = FindObjectsOfType<MeshRenderer>();
    }
    public void AddMeshColliders() {
       
        for (int i = 0; i < GO.Length; i++)
        {
            GO[i].enabled = false;

        }

        for (int i = 0; i < MR.Length; i++)
        {
            if (!MR[i].GetComponent<MeshCollider>())
                MR[i].gameObject.AddComponent<MeshCollider>();

        }
    }
#endif
}
