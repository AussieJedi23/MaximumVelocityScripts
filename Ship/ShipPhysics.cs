using UnityEngine;

namespace FLFlight
{
    public class ShipPhysics : MonoBehaviour
    {
        [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
        public Vector3 linearForce = new Vector3(100.0f, 100.0f, 100.0f);

        [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
        public Vector3 angularForce = new Vector3(100.0f, 100.0f, 100.0f);

        [Range(0.0f, 1.0f)]
        [Tooltip("Multiplier for longitudinal thrust when reverse thrust is requested.")]
        public float reverseMultiplier = 1.0f;

        [Tooltip("Multiplier for all forces. Can be used to keep force numbers smaller and more readable.")]
        public float forceMultiplier = 100.0f;

        public Rigidbody Rigidbody { get; private set; }

        private Vector3 appliedLinearForce = Vector3.zero;
        private Vector3 appliedAngularForce = Vector3.zero;

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            if (Rigidbody == null)
            {
                Debug.LogWarning(name + ": ShipPhysics has no rigidbody.");
            }
        }

        void FixedUpdate()
        {
            if (Rigidbody != null)
            {
                Rigidbody.AddRelativeForce(appliedLinearForce * forceMultiplier, ForceMode.Force);
                Rigidbody.AddRelativeTorque(appliedAngularForce * forceMultiplier, ForceMode.Force);
            }
        }

        public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput)
        {
            appliedLinearForce = Vector3.Scale(linearForce, linearInput);
            appliedAngularForce = Vector3.Scale(angularForce, angularInput);
        }
    }
}
