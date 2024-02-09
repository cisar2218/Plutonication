namespace Plutonication
{
    /// <summary>
    /// The message format used for communcating with Plutonication server.
    /// </summary>
    public class PlutonicationMessage
    {
        public string? Room { get; set; }
        public object? Data { get; set; }
    }
}
