using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
    [System.Serializable]
    public struct TilePosition
    {
        public int X;
        public int Y;

        public enum Direction { North, East, South, West }

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(TilePosition pos1, TilePosition pos2)
        {
            if (pos1.X == pos2.X && pos1.Y == pos2.Y)
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(TilePosition pos1, TilePosition pos2)
        {
            if (pos1.X != pos2.X || pos1.Y != pos2.Y)
            {
                return true;
            }
            return false;
        }

        public static TilePosition operator +(TilePosition pos1, TilePosition pos2)
        {
            return new TilePosition(pos1.X + pos2.X, pos1.Y + pos2.Y);
        }
        public static TilePosition operator -(TilePosition pos1, TilePosition pos2)
        {
            return new TilePosition(pos1.X - pos2.X, pos1.Y - pos2.Y);
        }
        public static TilePosition operator *(TilePosition pos1, TilePosition pos2)
        {
            return new TilePosition(pos1.X * pos2.X, pos1.Y * pos2.Y);
        }
        public static TilePosition operator *(TilePosition pos1, int factor)
        {
            return new TilePosition(pos1.X * factor, pos1.Y * factor);
        }
        public static TilePosition? GetDirection(TilePosition start, TilePosition target)
        {
            if (start.X != target.X && start.Y != target.Y)
            {
                return null;
            }
            else return (start - target).Normalize();
        }
        public TilePosition Normalize()
        {
            return new TilePosition(Mathf.Clamp(X, -1, 1), Mathf.Clamp(Y, -1, 1));
        }
        public static TilePosition GetDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return new TilePosition(0, 1);
                case Direction.East:
                    return new TilePosition(1, 0);
                case Direction.South:
                    return new TilePosition(0, -1);
                case Direction.West:
                    return new TilePosition(-1, 0);
                default:
                    return new TilePosition(0, 0);
            }
        }

        public static int Distance(TilePosition pos1, TilePosition pos2)
        {
            return (Mathf.Abs(pos1.X - pos2.X) + Mathf.Abs(pos1.Y - pos2.Y));
        }
        public static List<TilePosition> GetLine(TilePosition start, TilePosition target, int length)
        {
            List<TilePosition> positions = new List<TilePosition>();
            TilePosition? dir = GetDirection(start, target);
            if (dir == null)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    positions.Add(start + dir.Value * i);
                }
            }
            return positions;
        }

        public static List<TilePosition> GetAdjacent(Tile center)
        {
            List<TilePosition> neighbours = new List<TilePosition>();
            for (int x = 0 - 1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    neighbours.Add(new TilePosition(x + center.X, y + center.Y));
                }
            }
            return neighbours;
        }
        public static List<TilePosition> GetConnected(Tile center)
        {
            List<TilePosition> neighbours = new List<TilePosition>();
            neighbours.Add(new TilePosition(center.X - 1, center.Y));
            neighbours.Add(new TilePosition(center.X + 1, center.Y));
            neighbours.Add(new TilePosition(center.X, center.Y - 1));
            neighbours.Add(new TilePosition(center.X, center.Y + 1));
            return neighbours;
        }

        public override bool Equals(object obj)
        {
            return obj is TilePosition position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return X + ", " + Y;
        }
    }
}