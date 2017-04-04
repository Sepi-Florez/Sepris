using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    // The Grid
    public static Grid thisGrid;

    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w,h];

    public static bool InsideBorder(Vector3 pos) {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0 && (int)pos.y <= h - 1);

    }

    // Deletes Row Y
    void Awake() {
        thisGrid = this;
    }
    public static void DeleteRow(int y) {
        for (int x = 0; x < w; ++x) {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    // Moves entire row Y down
    public static void DecreaseRow(int y) {
        for (int x = 0; x < w; ++x) {
            if (grid[x, y] != null) {
                // Move one towards bottom in the Array
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }
    }
    // Moves all rows above Y down
    public static void DecreaseRowsAbove(int y) {
        for (int i = y; i < h; ++i)
            DecreaseRow(i);
    }
    // Checks if row Y is full
    public static bool IsRowFull(int y) {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }
    // Check whole field for full rows
    public static void DeleteFullRows() {
        int i = 0;
        for (int y = 0; y < h; ++y) {
            if (IsRowFull(y)) {
                i++;
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
        if (i != 0) {
            UIManager.thisManager.CallUpdateScore(i * 100 * i, i);
        }
    }
    public void Restart() {
        grid = new Transform[w, h];
    }

}

