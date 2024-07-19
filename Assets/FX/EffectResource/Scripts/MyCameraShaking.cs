using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MyCameraShaking : MonoBehaviour
{
    public Camera MyCamera;
    public Vector3 strength = new Vector3(0.1f, 0.1f, 0);
    public float delay;
    public float duration;
    public int vibrato = 10;
    public float randomness = 90;
    public bool snapping = false;
    public bool fadeOut = true;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        ShakeCamera();
    }

    void ShakeCamera()
    {
        MyCamera.transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut);
    }
}