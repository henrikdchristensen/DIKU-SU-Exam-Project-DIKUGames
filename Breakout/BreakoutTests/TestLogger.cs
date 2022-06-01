namespace BreakoutTests;

class TestLogger {

    /// <summary>
    /// Write out expected result and actual result
    /// </summary>
    public static string OnFailedTestMessage<T>(T expected, T result) {
        return $"Expected: {expected}, but got {result}";
    }

}