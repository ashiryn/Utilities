namespace FluffyVoid.Utilities;

/// <summary>
///     Utility class used to store helpful math functions and operations
/// </summary>
public static class Math
{
    /// <summary>
    ///     Converts a 1D array index into a 2D array index
    /// </summary>
    /// <param name="index">The 1D index to convert</param>
    /// <param name="width">The width(x) of the array</param>
    /// <returns>The x and y indexers for a 2D array</returns>
    public static (int x, int y) Convert1Dto2D(int index, int width)
    {
        (int x, int y) result;
        result.x = index % width;
        result.y = index / width;
        return result;
    }
    /// <summary>
    ///     Converts a 1D array index into a 3D array index
    /// </summary>
    /// <param name="index">The 1D index to convert</param>
    /// <param name="width">The width(x) of the array</param>
    /// <param name="height">The height(y) of the array</param>
    /// <returns>The x, y and z indexers for a 3D array</returns>
    public static (int x, int y, int z) Convert1Dto3D(
        int index, int width, int height)
    {
        (int x, int y, int z) result;
        result.x = index % width;
        result.y = index / width % height;
        result.z = index / width / height;
        return result;
    }

    /// <summary>
    ///     Converts a 2D array index into a 1D array index
    /// </summary>
    /// <param name="x">The x index from the 2D array</param>
    /// <param name="y">The y index from the 2D array</param>
    /// <param name="width">The width(x) of the array</param>
    /// <returns>The 1D array index calculated from the 2D array information</returns>
    public static int Convert2Dto1D(int x, int y, int width)
    {
        return y * width + x;
    }
    /// <summary>
    ///     Converts a 3D array index into a 1D array index
    /// </summary>
    /// <param name="x">The x index from the 3D array</param>
    /// <param name="y">The y index from the 3D array</param>
    /// <param name="z">The z index from the 3D array</param>
    /// <param name="width">The width(x) of the array</param>
    /// <param name="height">The height(y) of the array</param>
    /// <returns>The 1D array index calculated from the 3D array information</returns>
    public static int Convert3Dto1D(int x, int y, int z, int width, int height)
    {
        return z * width * height + y * width + x;
    }
}