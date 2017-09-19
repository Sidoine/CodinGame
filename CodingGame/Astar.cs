using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingGame
{
    public abstract class Astar<T>
        where T : IEquatable<T>
    {
        // Est-ce qu'on est dans les limites ?
        protected abstract bool IsInRange(T pos);
        // Est-ce que cette position est valide ?
        protected abstract bool IsPassable(T pos);
        // Si true, on s'arrête ici même si ce n'est pas la cible
        protected abstract bool IsAlternateTarget(T pos);
        // Heuristique pour déterminer si on s'approche de la cible (plus petit est le meilleur)
        protected abstract int Heuristique(T pos, T target);
        // La distance à la cible
        protected abstract int Distance(T pos, T target);
        // Tous les voisins
        protected abstract IEnumerable<T> Neighbors(T pos);
        
        public class NodeData
        {
            public int FScore;
            public int GScore;
            public NodeData CameFrom;
            public T Pos;
        }

        public T GetNext(T objective, T start)
        {
            var currentData = GetPath(objective, start);
            if (currentData == null)
                return start;
            while (currentData.CameFrom != null && !currentData.CameFrom.Pos.Equals(start))
                currentData = currentData.CameFrom;
            return currentData.Pos;
        }

        public NodeData GetPath(T objectif, T depart)
        {
            var closedSet = new HashSet<T>();
            var openSet = new Dictionary<T, NodeData>();
            var departData = new NodeData { FScore = Heuristique(depart, objectif), GScore = 0, Pos = depart };
            openSet[depart] = departData;
            
            while (openSet.Count > 0)
            {
                var minG = openSet.Values.Min(x => x.GScore);
                var currentData = openSet.Values.First(x => x.GScore == minG);
                var current = currentData.Pos;
                if (current.Equals(objectif) || IsAlternateTarget(current))
                {
                    return currentData;
                }
                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in Neighbors(current))
                {
                    if (!IsInRange(neighbor) || !IsPassable(neighbor)) continue;
                    if (closedSet.Contains(neighbor)) continue;
                    var g = currentData.GScore + Distance(current, neighbor); // 1
                    NodeData ndata;
                    if (!openSet.ContainsKey(neighbor))
                    {
                        ndata = new NodeData { Pos = neighbor };
                        openSet[neighbor] = ndata;
                    }
                    else
                    {
                        ndata = openSet[neighbor];
                        if (g > ndata.GScore) continue;
                    }
                    ndata.CameFrom = currentData;
                    ndata.GScore = g;
                    ndata.FScore = g + Heuristique(neighbor, objectif);
                }
            }
            return null;
        }
    }
}
