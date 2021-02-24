using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftData : MonoBehaviour
{
    public static float Friction = 0.5f;
    public static float MinmumSpeedTolerance = 0.05f;
    public float Acceleration;
    public float Deceleration;
    public float TurnSpeed;
    public float MaxSpeed;
    public int MaxHealth;
}
