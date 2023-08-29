using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestOverMobile.SaveSystem
{
    public class CardsSerialization : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            List<PlayerCard> cards = (List<PlayerCard>)obj;
            info.AddValue("cards", cards);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            List<PlayerCard> cards = (List<PlayerCard>)obj;
            cards = (List<PlayerCard>)info.GetValue("cards", typeof(List<PlayerCard>));
            obj = cards;
            return obj;
        }
    }
}