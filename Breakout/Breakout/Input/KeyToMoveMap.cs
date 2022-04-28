namespace Breakout.Input;

using System.Collections.Generic;

/// <summary>
/// A struct that describes how chars will be converted into an InputTye.
/// </summary>
public struct KeyToMoveMap : ICharToInputType {
    
    private Dictionary<char, InputType> map = new Dictionary<char, InputType>();
    
    /// <summary>
    /// The constructor that directly takes all possible input chars and tells what InputType a 
    /// given char should map to.
    /// </summary>
    /// <param name="up">The char that maps to InputType.Up.</param>
    /// <param name="down">The char that maps to InputType.Down.</param>
    /// <param name="left">The char that maps to InputType.Left.</param>
    /// <param name="right">The char that maps to InputType.Right.</param>
    /// <param name="exit">The char that maps to InputType.Exit.</param>
    /// <param name="performMove">The char that maps to InputType.PerformMove.</param>
    public KeyToMoveMap(char up, char down, char left, char right, char exit, char performMove) {
        map.Add(up, InputType.Up);
        map.Add(down, InputType.Down);
        map.Add(left, InputType.Left);
        map.Add(right, InputType.Right);
        map.Add(exit, InputType.Exit);
        map.Add(performMove, InputType.PerformMove);
    }

    /// <summary>
    /// The method used to convert a char to an InputType.
    /// </summary>
    /// <param name="key">The char that will be converted.</param>
    /// <returns>
    /// If the key is mapped to an InputType then it will return the InputType else it will return
    /// InputType.Undefined.
    /// </returns>
    public InputType Convert(char key) => map.GetValueOrDefault(key, InputType.Undefined);
}