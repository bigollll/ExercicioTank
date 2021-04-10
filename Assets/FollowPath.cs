using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    public float speed = 5.0f;    
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    
    int currentWP = 0;
    Graph g;
    void Start()
    {
        //setando variaveis 
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        
        //nó que começa
        currentNode = wps[0];
    }

    // 3 metodos que dão direcionamento
    public void GoToHeli()
    {
        //pega o grafh
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
        //define como 0 qnd termina tudo
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[3]);
        currentWP = 0;
    }

    public void GoToFabrica()
    {
       
        g.AStar(currentNode, wps[10]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        //zera qnd finaliza
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
          return;

        currentNode = g.getPathPoint(currentWP);
        //nó = wp

        
        //soma wp atual dependendo da distancia entre pontos
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,transform.position) < accuracy)
        {
            currentWP++;
        }

        if (currentWP < g.getPathLength())
        {
            //proximo wp
            goal = g.getPathPoint(currentWP).transform;
            //ve o proximo wp
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y,goal.position.z);
            //rotaciona 
            Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
        }
        //linha que faltava: movimentação 
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
