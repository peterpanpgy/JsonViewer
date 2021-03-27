using Newtonsoft.Json.Linq;

namespace JsonViewer.Model
{
    public static class NodeConverter
    {
        public static RootNode ConvertFromJObject(JObject jObject, string rootName)
        {
            var rootNode = new RootNode
            {
                Name = rootName,
            };
            Convert(jObject, rootNode);
            rootNode.FillSchema();
            return rootNode;
        }

        private static void Convert(JToken jToken, ObjectNode objectNode)
        {
            if (jToken is JObject jObject)
            {
                foreach (var jProperty in jObject.Properties())
                {
                    ConvertNode(jProperty.Value, jProperty.Name, objectNode);
                }
            }
            else if (jToken is JArray jArray)
            {
                for (var i = 0; i < jArray.Count; ++i)
                {
                    ConvertNode(jArray[i], i.ToString(), objectNode);
                }
            }
        }

        private static void ConvertNode(JToken value, string name, ObjectNode objectNode)
        {
            if (value is JValue)
            {
                if (value.Type == JTokenType.Null)
                {
                    objectNode.Nodes.Add(new NullNode{ Name = name, Parent = objectNode });
                }
                else
                {
                    objectNode.Nodes.Add(new ValueNode
                    {
                        Name = name,
                        Value = value.ToString(),
                        Parent = objectNode
                    });
                }
            }
            else if (value is JObject)
            {
                var subNode = new ObjectNode { Name = name, Parent = objectNode };
                objectNode.Nodes.Add(subNode);
                Convert(value, subNode);
            }
            else if (value is JArray)
            {
                var subNode = new ArrayNode { Name = name, Parent = objectNode };
                objectNode.Nodes.Add(subNode);
                Convert(value, subNode);
            }
        }
    }
}
