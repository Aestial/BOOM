using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RaycastHitEvent : UnityEvent<RaycastHit> {}
[Serializable]
public class CollisionEvent : UnityEvent<Collision> {}
[Serializable]
public class ColliderEvent : UnityEvent<Collider> {}
[Serializable]
public class Vector3Event : UnityEvent<Vector3> {}
[Serializable]
public class StringEvent : UnityEvent<string> {}
[Serializable]
public class FloatEvent : UnityEvent<float> {}
[Serializable]
public class BoolEvent : UnityEvent<bool> {}