using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertex : MonoBehaviour
{
    public vertex[] neighbors;
    public float c;
    public float h;
    public vertex prev;
    public string Name;
    // Start is called before the first frame update
    void Start()
    {
        c = 100;
    }
    public void prnt() {
        print(Name);
    }
    public override bool Equals(System.Object obj) {
        var v = obj as vertex;
        if (v == null) {
            return false;
        }
        return this.transform.position == v.transform.position;

    }
    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
    public void UpdateH(Vector3 goalPosition) {
        h = Vector3.Distance(this.transform.position, goalPosition);
        c = 100.0f;
        prev = null;
    }
    public void setC(float cost) {
        c = cost;
    }
}
