using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    [Range(1, 40)]public float distance = 1f;
    [Range(1, 180)]public float maxAngle = 45f;
    public string tagName = "";

    public GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject)continue;
            
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float cos = Vector3.Dot(transform.forward, direction);
            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

            if(angle > maxAngle)
            {
                result.Add(collider.gameObject);
            }
        }
        return result.ToArray();
    }
}
