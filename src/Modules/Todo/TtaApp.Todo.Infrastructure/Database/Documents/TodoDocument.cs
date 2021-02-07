using System;
using Convey.Types;

namespace TtaApp.Todo.Infrastructure.Database.Documents
{
    internal class TodoDocument : IIdentifiable<Guid>
    {
        public TodoDocument(
            Guid id, 
            string name, 
            bool done,
            int version
        )
        {
            Id = id;
            Name = name;
            Done = done;
            Version = version;
        }

        public Guid Id
        {
            get;
        }

        public string Name
        {
            get; 
        }

        public bool Done
        {
            get;
        }

        public int Version
        {
            get;
        }
    }
}
