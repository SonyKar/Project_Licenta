﻿using UnityEngine;

namespace Building
{
    public class Phantom : MonoBehaviour
    {
        [SerializeField] private Material okMaterial;
        [SerializeField] private Material errorMaterial;
        
        private Transform _transform;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _transform = transform;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            Vector3 newPos = Controller.RayToMouse().point;
            newPos.y = _transform.localScale.y / 2;
            _transform.position = newPos;
            
            _meshRenderer.material = BuildingManager.Instance.CanSpawnBuilding(_transform.position) ? okMaterial : errorMaterial;
        }
    }
}
