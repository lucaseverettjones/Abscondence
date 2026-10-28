using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblades : MonoBehaviour
{
    Transform[] children;
    void Start()
    {
        StartCoroutine(ActivateSawblades());
    }
    IEnumerator ActivateSawblades()
    {
        children = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
        }
    }
}