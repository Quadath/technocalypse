using System;
using System.Collections.Generic;
using UnityEngine;

public class AStar3D
{
    // 26 neighbor offsets (включає осьові, 2D- та 3D-діагоналі)
    private static readonly Vector3Int[] Neighbors = new Vector3Int[]
    {
        //Forward, left, right, back
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1),
        //Diag Y0
        new Vector3Int(1, 0, 1),
        new Vector3Int(-1, 0, 1),
        new Vector3Int(1, 0, -1),
        new Vector3Int(-1, 0, -1),
        //Y-1
        new Vector3Int(1, -1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(0, -1, 1),
        new Vector3Int(0, -1, -1),

        // new Vector3Int(1,0,0), new Vector3Int(-1,0,0),
        // new Vector3Int(0,1,0), new Vector3Int(0,-1,0),
        // new Vector3Int(0,0,1), new Vector3Int(0,0,-1),

        // new Vector3Int(1,1,0), new Vector3Int(1,-1,0), new Vector3Int(-1,1,0), new Vector3Int(-1,-1,0),
        // new Vector3Int(1,0,1), new Vector3Int(1,0,-1), new Vector3Int(-1,0,1), new Vector3Int(-1,0,-1),
        // new Vector3Int(0,1,1), new Vector3Int(0,1,-1), new Vector3Int(0,-1,1), new Vector3Int(0,-1,-1),

        // new Vector3Int(1,1,1), new Vector3Int(1,1,-1), new Vector3Int(1,-1,1), new Vector3Int(1,-1,-1),
        // new Vector3Int(-1,1,1), new Vector3Int(-1,1,-1), new Vector3Int(-1,-1,1), new Vector3Int(-1,-1,-1),
    };

    private class Node
    {
        public Vector3Int Pos;
        public Node Parent;
        public float G;
        public float F;
    }

    // Простий мін-кучний пріорітетний черга для Node за F (heap)
    private class PriorityQueue
    {
        private List<Node> heap = new List<Node>();
        public int Count => heap.Count;

        private void Swap(int i, int j)
        {
            var t = heap[i]; heap[i] = heap[j]; heap[j] = t;
        }

        public void Enqueue(Node n)
        {
            heap.Add(n);
            int i = heap.Count - 1;
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (heap[p].F <= heap[i].F) break;
                Swap(p, i);
                i = p;
            }
        }

        public Node Dequeue()
        {
            if (heap.Count == 0) return null;
            var root = heap[0];
            var last = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            if (heap.Count > 0)
            {
                heap[0] = last;
                int i = 0;
                while (true)
                {
                    int l = 2 * i + 1;
                    int r = 2 * i + 2;
                    int smallest = i;
                    if (l < heap.Count && heap[l].F < heap[smallest].F) smallest = l;
                    if (r < heap.Count && heap[r].F < heap[smallest].F) smallest = r;
                    if (smallest == i) break;
                    Swap(i, smallest);
                    i = smallest;
                }
            }
            return root;
        }
    }

    // API: знайти шлях з старту в ціль (вузли у Vector3Int)
    // isWalkable: функція яка перевіряє прохідність клітини у вашому блоці-світі
    // maxIterations: захист від нескінченних пошуків
    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int goal, Func<Vector3Int,bool> isWalkable, int maxIterations = 200000)
    {
        var open = new PriorityQueue();
        var openDict = new Dictionary<Vector3Int, Node>();
        var closed = new HashSet<Vector3Int>();

        Node startNode = new Node { Pos = start, Parent = null, G = 0f, F = Heuristic(start, goal) };
        open.Enqueue(startNode);
        openDict[start] = startNode;

        int iterations = 0;

        while (open.Count > 0 && iterations++ < maxIterations)
        {
            Node current = open.Dequeue();
            if (current == null) break;
            openDict.Remove(current.Pos);

            if (current.Pos == goal)
            {
                return ReconstructPath(current);
            }

            closed.Add(current.Pos);

            foreach (var offset in Neighbors)
            {
                Vector3Int neighborPos = current.Pos + offset;

                if (closed.Contains(neighborPos)) continue;
                if (!isWalkable(neighborPos)) continue;

                float stepCost = MovementCost(offset);
                float tentativeG = current.G + stepCost;

                if (openDict.TryGetValue(neighborPos, out Node existing))
                {
                    if (tentativeG < existing.G)
                    {
                        existing.G = tentativeG;
                        existing.Parent = current;
                        existing.F = tentativeG + Heuristic(neighborPos, goal);
                        // Note: heap key decreased but we don't have decrease-key; simple approach: re-enqueue
                        open.Enqueue(existing);
                    }
                }
                else
                {
                    Node neighborNode = new Node
                    {
                        Pos = neighborPos,
                        Parent = current,
                        G = tentativeG,
                        F = tentativeG + Heuristic(neighborPos, goal)
                    };
                    open.Enqueue(neighborNode);
                    openDict[neighborPos] = neighborNode;
                }
            }
        }

        // якщо не знайдено — повернути null або порожній список
        return null;
    }

    private static float Heuristic(Vector3Int a, Vector3Int b)
    {
        // Евклідова відстань (admissible)
        return Vector3IntDistance(a, b);
    }

    private static float MovementCost(Vector3Int offset)
    {
        int ax = Math.Abs(offset.x);
        int ay = Math.Abs(offset.y);
        int az = Math.Abs(offset.z);
        int sum = ax + ay + az;
        if (sum == 1) return 1f; // осьовий
        if (sum == 2) return Mathf.Sqrt(2f); // двовимірна діагональ
        if (sum == 3) return Mathf.Sqrt(3f); // просторовий діагональ
        return 1f;
    }

    private static float Vector3IntDistance(Vector3Int a, Vector3Int b)
    {
        return (new Vector3(a.x - b.x, a.y - b.y, a.z - b.z)).magnitude;
    }

    private static List<Vector3Int> ReconstructPath(Node node)
    {
        var path = new List<Vector3Int>();
        Node curr = node;
        while (curr != null)
        {
            path.Add(curr.Pos);
            curr = curr.Parent;
        }
        path.Reverse();
        return path;
    }
}