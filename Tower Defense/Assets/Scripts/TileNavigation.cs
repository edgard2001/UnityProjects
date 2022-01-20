using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNavigation : MonoBehaviour
{
    [SerializeField] private Tilemap _world;

    // Start is called before the first frame update
    void Start()
    {
        _world ??= GameObject.Find("Grid/Tilemap").GetComponent<Tilemap>();
    }

    public List<Vector3> GetPath(Vector3 origin, Vector3 target)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        int g = 0;
        int h = (_world.WorldToCell(target) - _world.WorldToCell(origin)).sqrMagnitude;
        Node startingNode = new Node(_world.WorldToCell(origin), g, h, null);
        openList.Add(startingNode);

        Node currentNode;
        bool foundPath = false;
        while (openList.Count > 0)
        {
            openList.Sort(new NodeComparerByF());

            currentNode = openList[0];
            closedList.Add(currentNode);
            openList.Remove(currentNode);

            if (currentNode.Position == _world.WorldToCell(target))
            {
                foundPath = true;
                break;
            }

            Vector3Int adjacentPosition;
            List<Vector3Int> directions = new List<Vector3Int>(8);
            directions.Add(Vector3Int.up);
            directions.Add(Vector3Int.down);
            directions.Add(Vector3Int.left);
            directions.Add(Vector3Int.right);

            foreach (Vector3Int direction in directions)
            {
                adjacentPosition = currentNode.Position + direction;

                if (!_world.GetTile(adjacentPosition).name.Equals("stone3")) continue;

                if (closedList.Find(x => x.Position == adjacentPosition) != null) continue;

                Node existingNode = openList.Find(x => x.Position == adjacentPosition);
                if (existingNode is null)
                {
                    g = currentNode.G + 10;
                    h = (_world.WorldToCell(target) - adjacentPosition).sqrMagnitude;
                    openList.Add(new Node(adjacentPosition, g, h, currentNode));

                }
                else
                {
                    existingNode.G = existingNode.G <= currentNode.G + 10 ? existingNode.G : currentNode.G + 10;
                }
            }

        }

        List<Vector3> path = new List<Vector3>();

        if (!foundPath) return path;

        currentNode = closedList[closedList.Count - 1];
        do
        {
            path.Insert(0, _world.GetCellCenterWorld(currentNode.Position));
            currentNode = currentNode.Parent;
        } while(currentNode.Parent != null);

        return path;
    }

    public class Node
    {
        public Node(Vector3Int position, int g, int h, Node parent)
        {
            Position = position;
            G = g;
            H = h;
            Parent = parent;
        }

        public Vector3Int Position;
        public int G;
        public int H;
        public int F
        {
            get { return G + H; }
        }
        public Node Parent;
    }

    public class NodeComparerByF : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            return x.F.CompareTo(y.F);
        }
    }



}
