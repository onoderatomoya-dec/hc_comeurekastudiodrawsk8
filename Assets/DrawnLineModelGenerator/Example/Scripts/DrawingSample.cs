using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DLMG.Sample
{
    public class DrawingSample : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;
        [SerializeField] private Material _lineMaterial = null;
        [SerializeField] private Material _fixedLineMaterial = null;
        [SerializeField] private GameObject _skateStartObject;
        [SerializeField] private GameObject _skateObject;
        [SerializeField] private Player _player;

        private bool _writing = false;
        private bool _separatedDrawingLine;
        private Vector3 _lastPoint;
        private GameObject _lineObject;
        private DrawnLineModelGenerateDomain _drawnLineModelGenerateDomain;
        private int _drawCount = 0;
        private List<Vector3> _rootPosition = new List<Vector3>();

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Update()
        {
            DrawLineCheck();
            FixLineCheck();
        }

        void DrawLineCheck()
        {
            if (!GameManager._isStart.Value)
            {
                return;
            }
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                return;
            }

            var inputPosition = Input.mousePosition;
            inputPosition.z = 16.2f;
            var worldPosition = _camera.ScreenToWorldPoint(inputPosition);
            if (!_writing)
            {
                _lineObject = new GameObject("Line");
                _drawnLineModelGenerateDomain = new DrawnLineModelGenerateDomain(0.2f, 0.6f);
                _lineObject.AddComponent<MeshFilter>().sharedMesh = _drawnLineModelGenerateDomain.Mesh;
                var material = Input.GetMouseButton(0) ? _lineMaterial : _fixedLineMaterial;
                _lineObject.AddComponent<MeshRenderer>().sharedMaterial = material;
                
                // 初めの1度でスケボーの位置(x.y)を設定する
                worldPosition = new Vector3(_skateStartObject.transform.position.x,_skateStartObject.transform.position.y,worldPosition.z);
                
                _lastPoint = worldPosition;
                _writing = true;
            }

            if (Vector3.Distance(_lastPoint, worldPosition) < 0.1f && _drawCount >= 2)
            {
                return;
            }

            var delta = worldPosition - _lastPoint;
            var length = delta.magnitude;
            if (Physics2D.Raycast(_lastPoint, delta.normalized, length))
            {
                _separatedDrawingLine = true;
                return;
            }

            if (!_separatedDrawingLine)
            {
                _rootPosition.Add(worldPosition);
                _drawnLineModelGenerateDomain.AppendPoint(worldPosition);
                _lastPoint = worldPosition;
                _drawCount++;
                return;
            }

            if (length >= 0.2f)
            {
                return;
            }

            _drawnLineModelGenerateDomain.AppendPoint(worldPosition);
            _lastPoint = worldPosition;
            _separatedDrawingLine = false;
        }

        void FixLineCheck()
        {
            if (!_writing)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                // var rb = _lineObject.AddComponent<Rigidbody2D>();
                var rb = _lineObject.AddComponent<Rigidbody>();
                rb.mass = _drawnLineModelGenerateDomain.PointCount * 0.25f;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                rb.isKinematic = true;
                _lineObject.AddComponent<LineDestroyer>();
                //var collider = _lineObject.AddComponent<PolygonCollider2D>();
                var collider = _lineObject.AddComponent<MeshCollider>();
                //collider.SetPath(0, _drawnLineModelGenerateDomain.GetPolygonColliderPath());
                _writing = false;
                _drawCount = 0;
                
                // 移動開始
                this.CallAfter(0.15f, delegate
                {
                    _player.GetGeneral().SetBools("isRun");
                    Vector3[] roots = _rootPosition.ToArray();
                    _skateObject.transform.DOPath(
                            path       : roots, //移動する座標をオブジェクトから抽出
                            duration   : 1.0f,                              //移動時間
                            pathType   : PathType.Linear                  //移動するパスの種類
                        )
                        .SetEase(Ease.Linear)
                        .SetLookAt(0.05f, Vector3.forward) //0.05秒後に通過する場所を見るように
                        .OnWaypointChange(pointNo =>
                        {
                            // 終了処理
                            if (pointNo > roots.Length)
                            {
                            }
                        });
                });
            }

            if (Input.GetMouseButtonUp(1))
            {
                // var collider = _lineObject.AddComponent<PolygonCollider2D>();
                var collider = _lineObject.AddComponent<MeshCollider>();
                // collider.convex = true;
                //collider.SetPath(0, _drawnLineModelGenerateDomain.GetPolygonColliderPath());
                _writing = false;
            }
        }
    }
}
