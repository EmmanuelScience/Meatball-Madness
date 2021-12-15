using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using AI;

public class navigator : MonoBehaviour
{
    public vertex start;
    public GameObject player;
    public List<vertex> nodes = new List<vertex>();
    
    public float sleep_dur;
    private float timestamp;

    private Vector3 player_loc;
    private vertex frog_loc;
    private bool player_moved;
    private List<vertex> explored = new List<vertex>();
    private List<vertex> open = new List<vertex>();
    private List<vertex> jobs = new List<vertex>();
    
    void Start()
    {
        timestamp = Time.time;
        open.Add(start);
        jobs.Add(start);
        UpdatePlayerLoc();
        this.transform.position = start.transform.position;
        frog_loc = start;
    }

    
    void Update()
    {
        if (Time.time > timestamp + sleep_dur)
        {
            UpdatePlayerLoc();
            // print("jobslen"+jobs.Count);
            // foreach (vertex v in jobs)
            // {
            //     print("jobs: " + v.transform.position);
            // }

            timestamp = Time.time;
            this.transform.position = jobs[0].transform.position + Vector3.up;
            frog_loc = jobs[0];
            // print("frog_loc" + frog_loc.transform.position);
            // print(explored.Contains(frog_loc));

            bool loc_in = false;
            foreach (vertex v in explored)
            {
                // v.prnt();
                if(v.Equals(frog_loc)) {
                    loc_in = true;
                }
            }
            if (!loc_in) {
                print("Adding to exp: "+frog_loc.Name);
                explored.Add(frog_loc);
            }
           
            foreach (vertex v in frog_loc.neighbors) {
                if (!explored.Contains(v) && !open.Contains(v)) {
                    print("Addingto open: "+ v.Name);

                    open.Add(v);
                }
            }
            
            // print(jobs.Count);
            open.Remove(jobs[0]);
            jobs.Remove(jobs[0]);
            // print(jobs.Count);
            // foreach (vertex v in explored)
            // {
            //     print(explored.Count+"explored" + v.transform.position);
            // }
            // foreach (vertex v in open)
            // {
            //     print(open.Count+"open" + v.transform.position);
            // }
            if (jobs.Count == 0 || player_moved || true) {
                RebuildJobs();
                // print("jobs");
                // foreach (vertex v in jobs) {
                //     print(v.transform.position);
                // }
                // print(jobs.Count);
            }
            // if (Vector3.Distance(this.transform.position, goal.transform.position) < epsilon)
            // {
            //     if (Vector3.Distance(goal.transform.position, player.transform.position) < 3 * epsilon)
            //     {
            //         print("Distance to player: " + Vector3.Distance(this.transform.position, player.transform.position));
                    
            //     } else
            //     {
            //         float mindist = 100000;
            //         int minind = -1;

            //         for (int i = 0; i < goal.neighbors.Length; i++)
            //         {
            //             vertex next = goal.neighbors[i];
            //             float weight = Vector3.Distance(this.transform.position, next.transform.position);
            //             float h = Vector3.Distance(player.transform.position, next.transform.position);

            //             if (weight + h <= mindist)
            //             {
            //                 minind = i;
            //                 mindist = weight + h;
            //             }
            //         }
            //         goal = goal.neighbors[minind];
            //     }
            //     timestamp = Time.time;
            // } else
            // {
            //     // go to goal
            //     this.transform.position = jobs.get(0).transform.position;
            // }
        }
    }

    // Checked
    void UpdatePlayerLoc() {
        float mindist = 100000.0f;
        Vector3 minpos = nodes[0].transform.position;
        vertex minvert = nodes[0];

        foreach (vertex node in nodes) {
            float dist = Vector3.Distance(player.transform.position, node.transform.position);
            if (dist <= mindist) {
                mindist = dist;
                minpos = node.transform.position;
            }
        }
        if (minpos != player_loc) {
            print(minpos);
            player_moved = true;
        }
        player_loc = minpos;
    }

    void RebuildJobs() {
        jobs = new List<vertex>();
        jobs = Navigate(player_loc);
    }

    List<vertex> Navigate(Vector3 goalPosition) {
        bool known = false;
        foreach (vertex v in explored) {
            if (v.transform.position == goalPosition) {
                known = true;
                break;
            }
        }
        if (!known) {
            foreach (vertex v in open) {
                if (v.transform.position == goalPosition) {
                    known = true;
                    break;
                }
            }
        }
        if (known) {
            // print("known");
            return NavigateKnown(goalPosition);
        } else {
            // print("unknown");
            return NavigateUnknown(goalPosition);
        }
    }

    List<vertex> NavigateUnknown(Vector3 goalPosition) {
        float mindist = 100000.0f;
        // print("openlen"+open.Count);
        Vector3 clo = open[0].transform.position;

        foreach (vertex node in open) {
            float dist = Vector3.Distance(goalPosition, node.transform.position);
            if (dist <= mindist) {
                mindist = dist;
                clo = node.transform.position;
            }
        }
        // print("closest open: "+clo);
        return NavigateKnown(clo);
    }

    List<vertex> NavigateKnown(Vector3 goalPosition) {
        Vector3 frog_pos = this.transform.position;
        if (frog_pos.Equals(goalPosition)) {
            print("already here");
            List<vertex> ret = new List<vertex>();
            ret.Add(frog_loc);
            return ret;
        }
        Queue<vertex> next = new Queue<vertex>();
        vertex curr = frog_loc;

        foreach (vertex v in nodes) {
            v.UpdateH(goalPosition);
        }
        curr.setC(0.0f);
        // print("frog"+ frog_pos);
        // print("curr"+ curr.transform.position);
        // print("goal"+ goalPosition);
        int i = 0;
        while (!curr.transform.position.Equals(goalPosition) && i < 10000) {
            // float minval = 100000.0f;
            // vertex minvert = curr;
            foreach (vertex v in curr.neighbors) {
                // print("v"+ v.transform.position);
                if (curr.c + Vector3.Distance(curr.transform.position, v.transform.position) <= v.c) {
                    v.setC(curr.c + Vector3.Distance(curr.transform.position, v.transform.position));
                    v.prev = curr;
                    bool vin = false;
                    foreach (vertex key in next)
                    {
                        if (key.Equals(v)) {
                            vin = true;
                            break;
                        }
                    } 
                    if (!vin) {
                        next.Enqueue(v);
                    }
                }
            }
            curr = next.Dequeue();
            i++;
        }
        
        List<vertex> ans = new List<vertex>();
        if (curr.Equals(frog_loc)) {
            print("wtf");
        }
        while (!(curr.Equals(frog_loc))) {
            ans.Add(curr);
            curr = curr.prev;
        }
        ans.Reverse();
        return ans;
    }
}
