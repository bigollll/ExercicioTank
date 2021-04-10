using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    //Wps no mapa
    public GameObject[] waypoints;

    //ligação dos wps
    public Link[] links;

    //pega o graph
    public Graph graph = new Graph();


    void Start()
    {
        // ve se o array n ta vazio
        if (waypoints.Length > 0)
        {
            //quantidade de objetos 
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
                //adiciona node pro obj
            }

            //qntidade de obj no array
            foreach (Link l in links)
            {
                //graph pra adc um Edge
                graph.AddEdge(l.node1, l.node2);
                //vê se é bi ou não
                if (l.dir == Link.direction.BI)  
                    graph.AddEdge(l.node2, l.node1);
                //graph pra adc um Edge
            }
        }
    }
    void Update()
    {
        graph.debugDraw();
        //mostra as linhas
    }
}

