namespace EthereumSamurai.EventProvider.Core.Actors
{
    /// <summary>
    ///    Metadata class. Nested/child actors can build path based on their parent(s) position in hierarchy.
    /// </summary>
    public class ActorMetadata
    {
        /// <summary>
        ///    Actor metadata constructor.
        /// </summary>
        /// <param name="name">
        ///    Actor's name.
        /// </param>
        /// <param name="parent">
        ///    Actor's parent.
        /// </param>
        public ActorMetadata(string name, ActorMetadata parent = null)
        {
            Name   = name;
            Parent = parent;
            Path   = $"{GetParentPath(parent)}/{Name}";
        }


        /// <summary>
        ///    Actor's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///    Actor's parent.
        /// </summary>
        public ActorMetadata Parent { get; }

        /// <summary>
        ///    Actor's path.
        /// </summary>
        public string Path { get; }


        /// <summary>
        ///    Returns path of the specified parent actor. If parent actor is not specified, we assume that actor is a top-level actor.
        /// </summary>
        /// <param name="parent">
        ///    Actor's parent metadata.
        /// </param>
        /// <returns>
        ///    Path to the specified parent actor.
        /// </returns>
        private static string GetParentPath(ActorMetadata parent)
        {
            return parent != null ? parent.Path : "/user";
        }
    }
}