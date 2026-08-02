using System;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public event Action<int> OnScored;

    [SerializeField] private int _pointsPerScore = 1;

    private void OnTriggerEnter(Collider other)
    {
       

        Ball ball = other.GetComponent<Ball>();
        if (ball == null)
        {
            Debug.Log("Not a ball");
            return;
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
       

        if (rb.velocity.y < 0f)
        {
           
            OnScored?.Invoke(_pointsPerScore);
        }
    }
}