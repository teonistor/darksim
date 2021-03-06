﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData{

    //Array with rooms:
    public static int[][][] rooms = new int[][][] {
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0},
        },
        new int[][] {
            new int[] {0, 0, 0, 0},
            new int[] {0, 1, 1, 0},
            new int[] {0, 0, 0, 0},
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 2, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0}

        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 2, 1, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 1, 0, 0},
            new int[] {0, 0, 2, 1, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {1, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 1, 0, 0},
            new int[] {0, 1, 0, 0, 0, 0},
            new int[] {0, 1, 2, 0, 0, 1},
            new int[] {0, 1, 1, 1, 1, 1},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 1, 0, 0},
            new int[] {0, 1, 0, 1, 1, 0},
            new int[] {0, 1, 2, 0, 0, 0},
            new int[] {0, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {1, 1, 0, 2, 1, 0},
            new int[] {0, 0, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 1, 0, 1, 0},
            new int[] {0, 1, 0, 2, 1, 1, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 1, 1, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 0, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 1, 0, 0, 0, 1, 0},
            new int[] {0, 1, 2, 1, 0, 0, 0, 1, 0},
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
            new int[] {0, 1, 0, 0, 2, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 0, 0, 1, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 2, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 2, 1, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 1, 1, 0, 0, 0},
            new int[] {0, 1, 1, 1, 1, 1, 1, 0, 0},
            new int[] {0, 1, 1, 1, 1, 1, 1, 0, 0},
            new int[] {0, 0, 0, 2, 1, 1, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 0, 0, 0, 1, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 2, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 1, 0, 0, 1, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 0, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 2, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 1, 1, 1, 0},
            new int[] {0, 1, 0, 0, 1, 1, 1, 1, 0},
            new int[] {0, 1, 0, 0, 1, 1, 0, 0, 0},
            new int[] {0, 1, 0, 0, 1, 1, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 1, 1, 0, 0, 0, 1, 1, 0},
            new int[] {0, 1, 1, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 1, 0, 0, 0, 2, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 1, 1, 0},
            new int[] {0, 1, 0, 0, 0, 0, 0, 0, 0},
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0}
        },
        new int[][] {
            new int[] {0, 0, 0},
            new int[] {0, 1, 0},
            new int[] {0, 1, 0},
            new int[] {0, 1, 0},
            new int[] {0, 1, 0},
            new int[] {0, 1, 0},
            new int[] {0, 0, 0},
        }
    };

    //1->wall; 2->enemy; 3->key; 4->exit; 5->light; 6->speed; 7->player

    public static int[][,] tutorials = new int[][,] {
        new int[,] {
            {1,1,1,1,1,1,1},
            {1,0,0,0,0,0,1},
            {1,0,0,7,0,0,1},
            {1,0,0,0,0,0,1},
            {1,0,0,0,0,0,1},
            {1,0,0,0,0,0,1},
            {1,0,0,0,0,0,1},
            {1,0,0,4,0,0,1},
            {1,1,1,0,1,1,1}
        },
        new int[,] {
            {1,1,1,1,1,1,1,1},
            {1,0,0,0,0,1,1,1},
            {1,0,0,7,0,1,1,1},
            {1,0,0,0,0,1,1,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,4,0,1,1,1},
            {1,1,1,0,1,1,1,1}
        },
        new int[,] {
            {1,1,1,1,1,1,1,1},
            {0,0,1,0,7,1,1,1},
            {0,3,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {0,0,1,0,0,1,1,1},
            {0,3,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {0,0,1,0,0,1,1,1},
            {0,3,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {0,0,1,0,0,1,1,1},
            {0,3,1,0,0,1,1,1},
            {1,1,1,0,0,1,1,1},
            {1,1,1,0,2,1,1,1},
            {0,0,1,0,0,1,1,1},
            {3,0,0,0,0,1,1,1},
            {1,0,0,0,0,1,1,1},
            {1,0,0,0,0,1,1,1},
            {1,2,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,0,4,0,0,1},
            {1,1,1,1,0,1,1,1},
        },
        new int[,] {
            {1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,0,7,1,1,1,0,0,0},
            {1,1,1,0,0,1,1,1,0,3,0},
            {1,1,1,6,5,1,1,1,0,0,0},
            {1,1,1,6,5,1,1,1,0,3,0},
            {1,1,1,6,5,1,1,1,0,0,0},
            {1,1,1,6,5,1,1,1,0,3,0},
            {1,1,1,6,5,1,1,1,0,0,0},
            {1,1,1,6,5,1,1,1,0,3,0},
            {1,1,1,6,5,1,1,1,0,0,0},
            {1,1,1,6,5,1,1,1,0,3,0},
            {1,1,1,6,5,1,1,1,0,0,0},
            {1,1,1,6,5,1,1,1,0,3,0},
            {1,1,1,0,0,1,1,1,0,0,0},
            {1,1,1,2,2,1,1,1,0,3,0},
            {1,1,1,0,0,0,1,1,0,0,0},
            {1,1,1,0,4,0,1,1,1,1,1},
            {1,1,1,1,0,1,1,1,1,1,1},
        },new int[,] {
            {1,1,1,1,1,1,1},
            {1,2,2,2,2,2,1},
            {1,2,0,2,0,2,1},
            {1,2,2,7,2,2,1},
            {1,2,0,2,0,2,1},
            {1,2,0,2,0,2,1},
            {1,2,2,2,2,2,1},
            {1,0,0,4,0,0,1},
            {1,1,1,0,1,1,1}
        }
    };
}
