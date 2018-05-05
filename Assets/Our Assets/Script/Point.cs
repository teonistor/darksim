using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : System.IComparable {

    public Point orig;
    public int x, y;
    public float h;
    public float d;

    public Point(int x, int y, float dist) {
        this.x = x;
        this.y = y;
        this.h = dist;
    }

    public Point(int x, int y, float dist, Point orig) {
        this.x = x;
        this.y = y;
        this.h = dist;
        this.d = orig.d + 1;
        this.orig = orig;
    }

    public int CompareTo(object obj) {
        Point pnt = (Point)obj;
        return (int)((this.h + this.d)  - (pnt.h + pnt.d));
    }

}
