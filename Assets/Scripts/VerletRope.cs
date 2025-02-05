using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Serialization;

public struct VerletNode
{
    public Vector3 Position;
    public Vector3 PrevoiusPosition;
}

public class VerletRope : MonoBehaviour
{
    // Points A et B
    [SerializeField] private Transform m_PointA;
    [SerializeField] private Transform m_PointB;

    private VerletNode[] m_VerletNodes;
    [SerializeField] private float m_RopeLength;
    [SerializeField] private int m_NumberOfNodes = 10; // Moins de nœuds pour un câble rigide
    [SerializeField] private int m_ConstraintIterationCount = 1; // Moins d'itérations pour rigidité
    [SerializeField] private Vector3 m_Gravity;
    private float m_DistanceBetweenNodes;
    [SerializeField] private float m_RopeRadius;
    [SerializeField] private int m_SubSteps = 2; // Moins de sous-steps pour optimiser
    [SerializeField] private float m_RigidityFactor = 1.0f; // Nouveau facteur de rigidité


    private RopeRenderer m_RopeRenderer;

    private void Awake()
    {
        m_VerletNodes = new VerletNode[(int)(m_NumberOfNodes)];
        m_DistanceBetweenNodes = m_RopeLength / m_NumberOfNodes;

        for (int i = 0; i < m_VerletNodes.Length; i++)
        {
            m_VerletNodes[i].Position = Vector3.Lerp(m_PointA.position, m_PointB.position, (float)i / (m_NumberOfNodes - 1));
            m_VerletNodes[i].PrevoiusPosition = m_VerletNodes[i].Position;
        }
    }

    private void Start()
    {
        m_RopeRenderer = GetComponent<RopeRenderer>();
    }

    private void FixedUpdate()
    {
        for (int step = 0; step < m_SubSteps; step++)
        {
            CalculateNewPositions(Time.fixedDeltaTime / m_SubSteps);
            for (int i = 0; i < m_ConstraintIterationCount; i++)
            {
                FixNodeDistances();
                if (i % 2 == 0)
                    ApplyCollision();
            }
        }

        m_RopeRenderer.RenderRope(m_VerletNodes, m_RopeRadius);
    }

    private void CalculateNewPositions(float deltaTime)
    {
        Vector3 gravityStep = m_Gravity * (deltaTime * deltaTime);

        for (int i = 1; i < m_VerletNodes.Length - 1; i++) // Ignorer Point A et Point B
        {
            var currNode = m_VerletNodes[i];
            var newPreviousPosition = currNode.Position;
            var newPosition = (2 * currNode.Position) - currNode.PrevoiusPosition + gravityStep;

            m_VerletNodes[i].PrevoiusPosition = newPreviousPosition;
            m_VerletNodes[i].Position = newPosition;
        }
    }

    private void FixNodeDistances()
    {
        // Fixer Point A et Point B
        m_VerletNodes[0].Position = m_PointA.position;
        m_VerletNodes[m_VerletNodes.Length - 1].Position = m_PointB.position;

        for (int i = 0; i < m_VerletNodes.Length - 1; i++)
        {
            var n1 = m_VerletNodes[i];
            var n2 = m_VerletNodes[i + 1];

            var d1 = n1.Position - n2.Position;
            var d2 = d1.magnitude;
            var d3 = (d2 - m_DistanceBetweenNodes) / d2;

            if (i > 0) // Ne pas déplacer le Point A
                m_VerletNodes[i].Position -= (d1 * (0.5f * d3 * m_RigidityFactor));

            if (i < m_VerletNodes.Length - 2) // Ne pas déplacer le Point B
                m_VerletNodes[i + 1].Position += (d1 * (0.5f * d3 * m_RigidityFactor));
        }
    }

    private void ApplyCollision()
    {
        for (int i = 1; i < m_VerletNodes.Length - 1; i++) // Ignorer Point A et Point B
        {
            ResolveCollision(ref m_VerletNodes[i]);
        }
    }

    private void ResolveCollision(ref VerletNode node)
    {
        var colliders = Physics.OverlapSphere(node.Position, m_RopeRadius);

        foreach (var col in colliders)
        {
            if (col.isTrigger) continue;

            Vector3 closestPoint = col.ClosestPoint(node.Position);
            float distance = Vector3.Distance(node.Position, closestPoint);

            if (distance < m_RopeRadius)
            {
                Vector3 penetrationNormal = (node.Position - closestPoint).normalized;
                float penetrationDepth = m_RopeRadius - distance;

                node.Position += penetrationNormal * penetrationDepth * 1.01f;
            }
        }
    }

    public int GetNodeCount()
    {
        return m_VerletNodes.Length;
    }
}