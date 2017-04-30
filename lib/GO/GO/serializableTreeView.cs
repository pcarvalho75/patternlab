using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace GO
{

    [Serializable]
    public class SerializableTreeView : TreeView, ISerializable
    {
        public SerializableTreeView()
            : base()
        { }

        public SerializableTreeView(SerializationInfo info, StreamingContext context)
            : this()
        {

            SerializationInfoEnumerator infoEnumerator = info.GetEnumerator();
            while (infoEnumerator.MoveNext())
            {

                TreeNode node = info.GetValue(infoEnumerator.Name, infoEnumerator.ObjectType) as TreeNode;
                if (node != null)
                {
                    this.Nodes.Add(node);
                }

            }

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (TreeNode node in this.Nodes)
            {
                info.AddValue(node.FullPath, node);
            }
        }

        /// <summary> 
        /// Serialize all the nodes of this tree to the stream provided, using the formatter provided. 
        /// </summary> 
        /// <param name="stream">The stream to serialize to.</param> 
        /// <param name="formatter">The formatter used to serialize.</param> 
        public void Serialize(Stream stream, IFormatter formatter)
        {

            formatter.Serialize(stream, this);

        }

        /// <summary> 
        /// Recreate this tree from a serialized version. 
        /// </summary> 
        /// <param name="stream">the stream that contains the serialized tree.</param> 
        /// <param name="formatter">the formatter used to desrialize the stream.</param> 
        public void Deserialize(Stream stream, IFormatter formatter, ImageList im)
        {

            // Clear our tree: 
            this.Nodes.Clear();

            SerializableTreeView temp = formatter.Deserialize(stream) as SerializableTreeView;

            if (temp != null)
            {

                // copy the nodes from the temp to our tree: 
                foreach (TreeNode node in temp.Nodes)
                {
                    this.Nodes.Add(node.Clone() as TreeNode);
                }

            }

        }

    }
}
