// reference - http://wiki.unity3d.com/index.php/Isometric

using UnityEngine;
using System.Collections;

/// <summary>
/// Class for isometric transitions
/// Author Marvin Neurath
/// version 21.11.2014
/// </summary>
public class Isometric
{
    public static Vector3 NORTH = new Vector3( 1, 0.5f, 0 );
    public static Vector3 WEST = new Vector3( -1, 0.5f, 0 );
    public static Vector3 SOUTH = new Vector3( -1, -0.5f, 0 );
    public static Vector3 EAST = new Vector3( 1, -0.5f, 0 );

    /// <summary>
    /// Converts a 2d view(e.g. top-view) to isometric view 
    /// </summary>
    /// <param name="_point">input vector</param>
    /// <returns>transformed vector</returns>
    public static Vector2 TwoDToIso( Vector2 _point )
    {
        var tempPt = new Vector2( 0, 0 );
        tempPt.x = ( _point.x - _point.y );
        tempPt.y = ( _point.x + _point.y ) / 2;
        return tempPt;
    }
    /// <summary>
    /// overload method
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    /// <returns></returns>
    public static Vector2 TwoDToIso( int _x, int _y )
    {
        var tempPt = new Vector2( 0, 0 );
        tempPt.x = (float) ( _x - _y );
        tempPt.y = (float) ( _x + _y ) / 2;
        return tempPt;
    }

    /// <summary>
    /// Converts a vector from isometric view into a 2d view
    /// </summary>
    /// <param name="_point"></param>
    /// <returns></returns>
    public static Vector2 IsoTo2D( Vector2 _point )
    {
        var tempPt = new Vector2( 0, 0 );
        tempPt.x = ( _point.x - _point.y );
        tempPt.x = ( 2 * _point.y + _point.x ) / 2;
        tempPt.y = ( 2 * _point.y - _point.x ) / 2;
        return tempPt;
    }

    /// <summary>
    /// overload method
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    /// <returns></returns>
    internal static Vector3 IsoTo2D( int _x, int _y )
    {
        var tempPt = new Vector2( 0, 0 );
        tempPt.x = (float) ( 2 * _y + _x ) / 2;
        tempPt.y = (float) ( 2 * _y - _x ) / 2;
        return tempPt;
    }
}