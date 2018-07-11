using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;

namespace Workflow.K2Service.Cores {
    public class Serializer
    {
        private Serializer()
        { }

        #region Deserialize()...
        /// <summary>
        /// Deserialises the Content property string into an object of the passed-in type.
        /// </summary>
        /// <param name="type">The type of the object to be deserialised.</param>
        /// <returns>An object of the type passed in.</returns>
        /// <remarks>
        ///		<p>This is a factory for messages that have been serialized as xml strings.</p>
        /// </remarks>
        public static object Deserialise(Type type, string content)
        {
            object outObject;
            XmlSerializer serializer = new XmlSerializer(type);
            outObject = serializer.Deserialize(new XmlTextReader(new StringReader(content)));
            return outObject;
        }
        #endregion

        #region Serialize()...
        /// <summary>
        /// Serialises the passed-in object to a string and stores it in the Content property.
        /// </summary>
        /// <param name="obj">The message object to be serialised.</param>
        public static string Serialize(object obj)
        {
            StringWriter writer = new StringWriter();
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(writer, obj);
            string serializedMsg = writer.ToString();
            writer.Close();
            serializedMsg = serializedMsg.Replace("utf-16", "utf-8");
            return serializedMsg;
        }
        #endregion
    }
}

