using System.Runtime.Serialization;

namespace TestOverMobile.SaveSystem
{
    public class ScoreSerialization : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            int score = (int)obj;
            info.AddValue("score", score);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            int score = (int)obj;
            score = (int)info.GetValue("score", typeof(int));
            obj = score;
            return obj;
        }
    }
}
