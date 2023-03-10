using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public Agent[] agent;

    int index = 0;

    public LayerMask layerMask;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) index = (++index % agent.Length);


        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                if (index == 0)
                {
                    Instantiate(agent[0], hitInfo.point, Quaternion.identity);
                }
                if (index == 1)
                {
                    Instantiate(agent[1], hitInfo.point, Quaternion.AngleAxis(Random.Range(0,360), Vector3.up));
                }
            }
        }
    }
}
