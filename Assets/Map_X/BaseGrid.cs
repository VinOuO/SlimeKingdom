using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseGrid : IGridShape
{

    /// <summary>
    /// 面片
    /// </summary>
    private Mesh _mesh;

    public Mesh mesh
    {
        get
        {
            return _mesh;
        }
    }

    /// <summary>
    /// 位置
    /// </summary>
    private Vector3 _pos;
    public Vector3 pos
    {
        get
        {
            return _pos;
        }
    }

    /// <summary>
    /// 一個網格的基數
    /// </summary>
    protected int _coefficient;

    /// <summary>
    /// 頂點數
    /// </summary>
    protected Vector3[] _vertexes;
    /// <summary>
    /// 三角形索引  
    /// </summary>
    protected int[] _triangles;
    /// <summary>
    /// mesh 長度的段數和寬度的段數 
    /// </summary>
    protected Vector2 _segment;

    protected BaseGrid()
    {
        this._init();
    }

    /// <summary>
    /// 創建網格
    /// 網格類型，網格位置
    /// </summary>
    public static T Create<T>(Vector3 pos) where T : BaseGrid, new()
    {
        T mapGrid = new T();
        mapGrid._pos = pos;
        mapGrid._createMesh();
        return mapGrid;

    }

    protected virtual void _init()
    {
        this._coefficient = Map.mIns.coefficient;
        this._mesh = null;
        this._vertexes = null;
        this._triangles = null;
        this._segment = new Vector2((this._coefficient - 1), (this._coefficient - 1));
    }

    private void _createMesh()
    {
        this.CaculateVertexes();
        this.CaculateTriangles();

        if (this._vertexes == null || this._triangles == null)
        {
            this._mesh = null;
            return;
        }

        if (mesh == null)
        {
            this._mesh = new Mesh();
        }
        mesh.vertices = this._vertexes;
        mesh.triangles = this._triangles;
    }

    public virtual void Release(bool disposing)
    {
        this._vertexes = null;
        this._triangles = null;
        GameObject.Destroy(_mesh);
    }

    protected abstract void CaculateVertexes();

    protected abstract void CaculateTriangles();
}

