using System;
using System.Collections;
using System.Collections.Generic;
using _GameFiles.Scripts.Controllers;
using _GameFiles.Scripts.EventArgs;
using _GameFiles.Scripts.ScriptableObjects;
using _GameFiles.Scripts.Utilities;
using EventDrivenFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers
{
    public class DrawingManager : BaseManager
    {
        [SerializeField] private DrawingData drawingData;
        [SerializeField] private MeshCollider drawingArea;
        [SerializeField] private Camera drawingCam;

        private Coroutine _drawCoroutine;
        
        private DrawingController _currentLine;
        private DrawingController _drawingPrefab;
        private DrawingController _drawing;
        
        private Gradient _gradientColor;
        
        private float _pointsDistance;
        private float _lineWidth;

        private bool _isEnabled;
        private bool IsDrawArea =>
            drawingArea.bounds.Contains(
                drawingCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11)));
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case StartDrawEventArgs _:
                    DrawEnabled();
                    break;
                case EndDrawEventArgs _:
                    DrawDisabled();
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _drawingPrefab = drawingData.DrawingPrefab;
            _gradientColor = drawingData.GradientColor;
            _pointsDistance = drawingData.PointsDistance;
            _lineWidth = drawingData.LineWidth;
            
            _currentLine = Instantiate(_drawingPrefab, transform);
            _currentLine.SetLineColor(_gradientColor);
            _currentLine.SetPointsDistance(_pointsDistance);
            _currentLine.SetLineWidth(_lineWidth);
            _currentLine.gameObject.SetActive(false);

        }

        private void Update()
        {
            if (_isEnabled)
            {
                Draw();
            }
        }

        private void DrawEnabled()
        {
            if (!IsDrawArea) return;
            
            _currentLine.ResetDrawing();
            _currentLine.transform.position = Vector3.zero;
            _currentLine.transform.localScale = Vector3.one;
            _isEnabled = true;
            _currentLine.gameObject.SetActive(true);
        }

        private void DrawDisabled()
        {
            if (_currentLine.PointsCount >=4)
            {
                CreateDraw();
            }
            else
            {
                _currentLine.ResetDrawing();
                _currentLine.gameObject.SetActive(false);
            }
            _isEnabled = false;
        }

        private void Draw()
        {
            if (!IsDrawArea) return;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                10));
           _currentLine.AddPoint(mousePos);
        }

        private void CreateDraw()
        {
            _drawing = _currentLine;
            
            Vector3 middlePoint = new Vector3(-5, 10, 25);
            Vector3 offset = _drawing.transform.position - _drawing.transform.TransformPoint(_drawing.LineRenderer.bounds.center);
            _drawing.transform.position = middlePoint + offset;
            _drawing.transform.localScale *= 2;
            Broadcast(new DrawingIsCreatedEventArgs(_drawing.GetMinPoint()));
        }
    }
}