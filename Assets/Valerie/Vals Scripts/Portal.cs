using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
