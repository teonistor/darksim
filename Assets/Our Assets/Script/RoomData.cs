﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData{

    //Array with rooms:
    public static int[][][] rooms = new int[][][] {
        new int[][] {
            new int[] {1, 1, 1, 1, 1, 1},
            new int[] {1, 1, 1, 1, 1, 1},
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0}

        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {1, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 1, 0, 0},
            new int[] {0, 1, 0, 0, 0, 0},
            new int[] {0, 1, 0, 0, 0, 1},
            new int[] {0, 1, 1, 1, 1, 1},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 1, 0, 0},
            new int[] {0, 1, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {1, 1, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 1, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 1, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 1, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 1, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 0, 0, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        }
    };


}