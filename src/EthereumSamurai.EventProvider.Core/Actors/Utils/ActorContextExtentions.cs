namespace EthereumSamurai.EventProvider.Core.Actors.Utils
{
    using Akka.Actor;

    internal static class ActorContextExtentions
    {
        public static ICanTell ActorSelection(this IUntypedActorContext context, ActorMetadata metadata)
        {
            return context.ActorSelection(metadata.Path);
        }
    }
}