namespace Middleware.Core
{
    using System;

    internal class PipeItem
    {
        public PipeItem(Type type)
        {
            Type = type;
            PipeItemType = PipeItemType.Type;
        }

        public PipeItem(object givenInstance)
        {
            GivenInstance = givenInstance;
            PipeItemType = PipeItemType.Instance;
        }


        public Type Type { get; private set; }

        public object GivenInstance { get; private set; }

        public PipeItemType PipeItemType { get; private set; }
    }
}