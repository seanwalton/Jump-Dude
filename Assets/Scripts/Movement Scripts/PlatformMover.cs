using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float yMoveDistance;
    [SerializeField] private float xMoveDistance;
    [SerializeField] private float timeToMove;
    private Transform tr;
    private Sequence ySequence;
    private Sequence xSequence;

    public bool movingX { private set; get; }
    public bool movingY { private set; get; }

    private Vector3 startPos;

    private void Awake()
    {
        tr = transform;
        movingX = false;
        movingY = false;

        startPos = tr.position;
    }



    public void StartXMotion()
    {
        if (movingX) return;

        movingX = true;
        tr.DOMoveX(startPos.x + xMoveDistance, timeToMove / 4f).SetEase(Ease.InOutCubic)
            .OnComplete(StartXSequence);
    }
    public void StartYMotion()
    {
        if (movingY) return;
        movingY = true;
        tr.DOMoveY(startPos.y + yMoveDistance, timeToMove / 4f).SetEase(Ease.InOutCubic)
            .OnComplete(StartYSequence);
    }

    private void StartXSequence()
    {
        xSequence = DOTween.Sequence();

        xSequence.Append(tr.DOMoveX(startPos.x - xMoveDistance, timeToMove / 2f).
            SetEase(Ease.InOutCubic));


        xSequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void StartYSequence()
    {
        ySequence = DOTween.Sequence();

        ySequence.Append(tr.DOMoveY(startPos.y - yMoveDistance, timeToMove / 2f).
            SetEase(Ease.InOutCubic));       
        

        ySequence.SetLoops(-1, LoopType.Yoyo);
    }

    
}
