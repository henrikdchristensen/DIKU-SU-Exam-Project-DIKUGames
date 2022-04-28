namespace Breakout.Input {


    /// <summary>
    /// An interface that describes an objects ability to convert chars to InputType.
    /// </summary>
    public interface ICharToInputType {

        /// <summary>
        /// The method that will convert a given char to a given InputType.
        /// </summary>
        /// <param name="key">A given keyboard key as a char.</param>
        /// <returns>The resulting InputType enum.</returns>
        InputType Convert(char key);
    }

}