using UnityEngine;

namespace Client {
    struct DeformableObject {
        public MeshFilter MeshFilter;
        public Mesh Mesh;
        public Vector3[] Vertices;
    }
}