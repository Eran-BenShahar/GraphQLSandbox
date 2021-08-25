using HotChocolate.Types;
using System;

namespace GraphQLSandbox
{
    public sealed class User
    {
        public string Name { get; set; }

        public User()
        {
            Name = null!;
        }
    }

    public interface IDocument
    {
        DateTime CreationTime { get; set; }
    }

    public class DocumentType : InterfaceType<IDocument>
    {
        protected override void Configure(
            IInterfaceTypeDescriptor<IDocument> descriptor)
        {
            descriptor.Name("Document");
        }
    }

    public interface IMessage : IDocument
    {
        User From { get; set; }
        User To { get; set; }
    }

    public class MessageType : InterfaceType<IMessage>
    {
        protected override void Configure(
            IInterfaceTypeDescriptor<IMessage> descriptor)
        {
            descriptor
                .Name("Message")
                .Implements<DocumentType>();
        }
    }

    public class TextMessage : IMessage
    {
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }
        public User From { get; set; }
        public User To { get; set; }
    }

    public class TextMessageType : ObjectType<TextMessage>
    {
        protected override void Configure(
            IObjectTypeDescriptor<TextMessage> descriptor) =>
            descriptor
                .Name("TextMessage")
                .Implements<MessageType>();
    }

    public class Query
    {
        public IMessage[] GetMessages() =>
            new IMessage[]
            {
                new TextMessage
                {
                    Content = "Secret",
                    CreationTime = DateTime.UtcNow,
                    From = new User { Name = "Alice" },
                    To = new User { Name = "Bob" }
                }
            };
    }

    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(f => f.GetMessages());
        }
    }
}