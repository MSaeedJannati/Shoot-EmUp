using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GizmoFactory;

public class Gizmo : MonoBehaviour
{
    #region Variables
    [SerializeField] Color Colour;
    [SerializeField] shape Shape;
    [SerializeField] renderType RenderType;
    [SerializeField] float Radius;
    Transform mTransform;
    bool hasTransform;
    //Shape mShape;
    #endregion
    #region monobehaviuor CallBacks
    private void OnDrawGizmos()
    {
        Gizmos.color = Colour;
        CheckForTransform();
        drawGizmo();
    }

    #endregion
    #region Functions
    public void CheckForTransform()
    {
        //if (mShape!=null)
        //    return;

        //mShape = ShapeFactory.CreateObject(Shape, RenderType, GetComponent<Transform>(), Radius);
        if (hasTransform)
            return;
        mTransform = GetComponent<Transform>();
        hasTransform = false;
    }
    public void drawGizmo()
    {

        switch (Shape)
        {
            case shape.SPHERE:
                if (RenderType == renderType.WIRE_FRAME)
                {
                    Gizmos.DrawWireSphere(mTransform.position, Radius);
                }
                else
                {
                    Gizmos.DrawSphere(mTransform.position, Radius);
                }
                break;
            case shape.CUBE:
                if (RenderType == renderType.WIRE_FRAME)
                {
                    Gizmos.DrawWireCube(mTransform.position, Radius * Vector3.one);
                }
                else
                {
                    Gizmos.DrawCube(mTransform.position, Radius * Vector3.one);
                }
                break;
        }
        //mShape.Draw();
    }
    #endregion

    #region Classes
    #endregion
}
#region GizmoTypeFactory
namespace GizmoFactory
{
    public class Shape
    {
        public Transform mTransform;
        public float Radius;
        public Shape( Transform MTransform, float radius)
        {
            mTransform = MTransform;
            Radius = radius;
        }
        public virtual void Draw() { }
    }
    public class Sphere : Shape
    {
        public Sphere( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
    }

    public class WireFrameSphere : Sphere
    {
        public WireFrameSphere( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
        public override void Draw()
        {
            Gizmos.DrawWireSphere(mTransform.position, Radius);
        }
    }
    public class ShadedSphere : Sphere
    {
        public ShadedSphere( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
        public override void Draw()
        {
            Gizmos.DrawSphere(mTransform.position, Radius);
        }
    }
    public class Cube : Shape
    {
        public Cube( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
    }
    public class WireFrameCube : Sphere
    {
        public WireFrameCube( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
        public override void Draw()
        {
            Gizmos.DrawWireCube(mTransform.position, Radius * Vector3.one);
        }
    }
    public class ShadedCube : Sphere
    {
        public ShadedCube( Transform MTransform, float radius) : base( MTransform, radius)
        {

        }
        public override void Draw()
        {
            Gizmos.DrawCube(mTransform.position, Radius * Vector3.one);
        }
    }
    public static class ShapeFactory
    {
        public static Shape CreateObject(shape Shape, renderType RenderType,  Transform transform, float radius)
        {

            switch (Shape)
            {
                case shape.SPHERE:
                    if (RenderType == renderType.WIRE_FRAME)
                    {
                        return new WireFrameSphere( transform, radius);
                    }
                    else
                    {
                        return new ShadedSphere( transform, radius);
                    }
                    break;
                case shape.CUBE:
                    if (RenderType == renderType.WIRE_FRAME)
                    {
                        return new WireFrameCube( transform, radius);
                    }
                    else
                    {
                        return new ShadedCube( transform, radius);
                    }
                    break;
                default:
                    return null;
                    break;
            }
        }
    }

    #region Enums
    public enum shape
    {
        SPHERE,
        CUBE
    }
    public enum renderType
    {
        WIRE_FRAME,
        SHADED
    }
    #endregion
}
#endregion