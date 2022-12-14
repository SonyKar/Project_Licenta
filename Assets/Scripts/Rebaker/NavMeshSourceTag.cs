using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Tagging component for use with the LocalNavMeshBuilder
// Supports mesh-filter and terrain - can be extended to physics and/or primitives
namespace Rebaker
{
    [DefaultExecutionOrder(-200)]
    public class NavMeshSourceTag : MonoBehaviour
    {
        // Global containers for all active mesh/terrain tags
        private readonly static List<MeshFilter> MMeshes = new List<MeshFilter>();
        private readonly static List<Terrain> MTerrains = new List<Terrain>();

        void OnEnable()
        {
            var m = GetComponent<MeshFilter>();
            if (m != null)
            {
                MMeshes.Add(m);
            }

            var t = GetComponent<Terrain>();
            if (t != null)
            {
                MTerrains.Add(t);
            }
        }

        void OnDisable()
        {
            var m = GetComponent<MeshFilter>();
            if (m != null)
            {
                MMeshes.Remove(m);
            }

            var t = GetComponent<Terrain>();
            if (t != null)
            {
                MTerrains.Remove(t);
            }
        }

        // Collect all the navmesh build sources for enabled objects tagged by this component
        public static void Collect(ref List<NavMeshBuildSource> sources)
        {
            sources.Clear();

            foreach (MeshFilter mf in MMeshes)
            {
                if (mf == null) continue;

                Mesh m = mf.sharedMesh;
                if (m == null) continue;

                NavMeshBuildSource s = new NavMeshBuildSource
                {
                    shape = NavMeshBuildSourceShape.Mesh,
                    sourceObject = m,
                    transform = mf.transform.localToWorldMatrix,
                    area = 0
                };
                sources.Add(s);
            }

            foreach (Terrain t in MTerrains)
            {
                if (t == null) continue;

                NavMeshBuildSource s = new NavMeshBuildSource
                {
                    shape = NavMeshBuildSourceShape.Terrain,
                    sourceObject = t.terrainData,
                    // Terrain system only supports translation - so we pass translation only to back-end
                    transform = Matrix4x4.TRS(t.transform.position, Quaternion.identity, Vector3.one),
                    area = 0
                };
                sources.Add(s);
            }
        }
    }
}