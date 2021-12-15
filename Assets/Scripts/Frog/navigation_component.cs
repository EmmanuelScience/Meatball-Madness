using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigation_component : MonoBehaviour
{
    private vertex curr_node;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setCurrNode(vertex n) {
        curr_node = n;
    }
    vertex playerNode(GameObject player) {
        vertex player_loc = curr_node;
        float mindist = 100000.0f;
        Vector3 minpos = curr_node.transform.position;
        vertex minvert = curr_node;
        List<vertex> op = new List<vertex>();
        List<vertex> clo = new List<vertex>();
        op.Add(curr_node);
        vertex node = op[0];

        while (op.Count > 0) {
            node = op[0];
            op.RemoveAt(0);
            clo.Add(node);
            float dist = Vector3.Distance(player.transform.position, node.transform.position);
            if (dist <= mindist) {
                mindist = dist;
                minvert = node;
            }

            foreach (vertex neigh in node.neighbors)
            {
                if (!op.Contains(neigh) && !clo.Contains(neigh)){
                    op.Add(neigh);
                } else {
                    // print("neighbor already seen");
                }
            }
        }
        player_loc = minvert;
        return player_loc;
    }

    public Vector3 jumpNext(GameObject player) {
        vertex goalNode = playerNode(player);
        List<vertex> op = new List<vertex>();
        List<vertex> clo = new List<vertex>();
        op.Add(curr_node);

        vertex node = op[0];
        node.prev = null;
        while (op.Count > 0 && !node.Equals(goalNode)) {
            node = op[0];
            op.RemoveAt(0);
            clo.Add(node);
            foreach (vertex neigh in node.neighbors)
            {
                if (!op.Contains(neigh) && !clo.Contains(neigh)){
                    neigh.prev = node;
                    op.Add(neigh);
                } else {
                    // print("neighbor already seen");
                }
            }

        }
        if (!node.Equals(goalNode)) {
            print("Didn't find goal.");
            return goalNode.transform.position;
        } 

        while (node.prev != null && !node.prev.Equals(curr_node)) {
            node = node.prev;
        }
        setCurrNode(node);
        return node.transform.position;



    }
}
