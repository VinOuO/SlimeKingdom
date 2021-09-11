using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGridShape : MonoBehaviour {

    public interface IGridShapee
    {
        /// <summary>
        /// 面片
        /// </summary>
        Mesh mesh { get; }
        /// <summary>
        /// 位置
        /// </summary>
        Vector3 pos { get; }
    }


}
