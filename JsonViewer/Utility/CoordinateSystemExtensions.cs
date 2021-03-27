using System;
using System.Windows.Media.Media3D;

namespace JsonViewer.Utility
{
    public static class CoordinateSystemExtensions
    {
        /// <summary>
        /// Get the transmation matrix to its parent coordination system
        /// For instance, we have a point on frame, then we ask for a matrix to transform
        /// this point to the coordination system on its owner shape, we can use this method.
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system3 d.</param>
        /// <returns></returns>
        public static Matrix3D TransformationMatrixTo(this CoordinateSystem coordinateSystem)
        {
            var xVector = new Vector3D(coordinateSystem.Xaxis.X, coordinateSystem.Xaxis.Y, coordinateSystem.Xaxis.Z);
            var yVector = new Vector3D(coordinateSystem.Yaxis.X, coordinateSystem.Yaxis.Y, coordinateSystem.Yaxis.Z);
            var zVector = Vector3D.CrossProduct(xVector, yVector);
            zVector.Normalize();

            return new Matrix3D
            {
                M11 = xVector.X,
                M12 = xVector.Y,
                M13 = xVector.Z,

                M21 = yVector.X,
                M22 = yVector.Y,
                M23 = yVector.Z,

                M31 = zVector.X,
                M32 = zVector.Y,
                M33 = zVector.Z,

                OffsetX = coordinateSystem.Origin.X,
                OffsetY = coordinateSystem.Origin.Y,
                OffsetZ = coordinateSystem.Origin.Z,

                M14 = 0,
                M24 = 0,
                M34 = 0,
                M44 = 1
            };
        }

        /// <summary>
        /// Get the transmation matrix from its parent coordination system to this
        /// For instance, we have a point on shape, then we ask for a matrix to transform
        /// this point to the coordination system on a frame that is in the shape, we can use this method.
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system3 d.</param>
        /// <returns></returns>
        public static Matrix3D TransformationMatrixFrom(this CoordinateSystem coordinateSystem)
        {
            var matrix = coordinateSystem.TransformationMatrixTo();
            matrix.Invert();
            return matrix;
        }

        /// <summary>
        /// Get transmation matrix from fromCs to toCs, always make sure fromCs and toCs are in the same parent coordination system
        /// For instance, both frame1 and frame2 are in the same shape, now we have a point on frame1, then we ask for a matrix to transform
        /// this point to the coordination system on frame2, we can use this method.
        /// </summary>
        /// <param name="toCs">To cs.</param>
        /// <param name="fromCs">From cs.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">toCs cannot be null</exception>
        public static Matrix3D TransformationMatrixFrom(this CoordinateSystem toCs, CoordinateSystem fromCs)
        {
            if (toCs == null) throw new NullReferenceException("toCs cannot be null");
            if (fromCs == null) return toCs.TransformationMatrixFrom();
            return fromCs.TransformationMatrixTo() * toCs.TransformationMatrixFrom();
        }

        /// <summary>
        /// Get transmation matrix from fromCs to toCs, always make sure fromCs and toCs are in the same parent coordination system
        /// For instance, both frame1 and frame2 are in the same shape, now we have a point on frame1, then we ask for a matrix to transform
        /// this point to the coordination system on frame2, we can use this method.
        /// </summary>
        /// <param name="fromCs">From cs.</param>
        /// <param name="toCs">To cs.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">fromCs cannot be null</exception>
        public static Matrix3D TransformationMatrixTo(this CoordinateSystem fromCs, CoordinateSystem toCs)
        {
            if (fromCs == null) throw new NullReferenceException("fromCs cannot be null");
            if (toCs == null) return fromCs.TransformationMatrixTo();
            return toCs.TransformationMatrixFrom(fromCs);
        }
    }
}