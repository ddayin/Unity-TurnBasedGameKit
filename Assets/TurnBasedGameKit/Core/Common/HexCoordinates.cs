using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private Vector3Int m_Offset;

    public HexCoordinates( Vector3Int _offset )
    {
        this.m_Offset = _offset;
    }

    public static HexCoordinates FromOffsetCoordinates( Vector3Int _offset )
    {
        return new HexCoordinates( new Vector3Int( _offset.x - _offset.z / 2, _offset.y, _offset.z ) );
    }

    public static Vector3 FromCoordinates3D( Vector3Int _coordinate )
    {
        Vector3 position = Vector3.zero;
        int x = _coordinate.x;
        int y = _coordinate.y;
        int z = _coordinate.z;

        position.x = ( x + z * 0.5f - z / 2 ) * ( HexMetrics.innerRadius * 2f );
        position.y = 0f;
        position.z = z * ( HexMetrics.outerRadius * 1.5f );

        return position;
    }

    public static Vector3 FromCoordinates2D( Vector3Int _coordinate )
    {
        Vector3 position = Vector3.zero;
        int x = _coordinate.x;
        int y = _coordinate.y;
        int z = _coordinate.z;

        position.x = ( x + y * 0.5f - y / 2 ) * ( HexMetrics.innerRadius * 2f );
        position.y = y * ( HexMetrics.outerRadius * 1.5f );
        position.z = 0;

        return position;
    }

    public static HexCoordinates FromPosition( Vector3 _position )
    {
        float x = _position.x / ( HexMetrics.innerRadius * 2f );
        float y = -x;

        float offset = _position.z / ( HexMetrics.outerRadius * 3f );
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt( x );
        int iY = Mathf.RoundToInt( y );
        int iZ = Mathf.RoundToInt( -x - y );

        if ( iX + iY + iZ != 0 )
        {
            float dX = Mathf.Abs( x - iX );
            float dY = Mathf.Abs( y - iY );
            float dZ = Mathf.Abs( -x - y - iZ );

            if ( dX > dY && dX > dZ )
            {
                iX = -iY - iZ;
            }
            else if ( dZ > dY )
            {
                iZ = -iX - iY;
            }
        }

        //return new HexagonCoordinates( iX, iZ );
        return new HexCoordinates( new Vector3Int( iX, iY, iZ ) );
    }

    //public override string ToString()
    //{
    //    return "(" +
    //        X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    //}

    //public string ToStringOnSeparateLines()
    //{
    //    return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    //}
}