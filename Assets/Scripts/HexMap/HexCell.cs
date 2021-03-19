using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap
{
    public class HexCell : MonoBehaviour
    {
        public HexCoordinates coordinates;

        [SerializeField]
        HexCell[] neighbors;

        public Color color;

        public HexCell GetNeighbor(HexDirection direction)
        {
            return neighbors[(int)direction];
        }

        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            neighbors[(int)direction] = cell;
            cell.neighbors[(int)direction.Opposite()] = this;
        }
    }
}
