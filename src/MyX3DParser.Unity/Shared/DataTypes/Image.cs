using System;
using System.Collections.Generic;
using System.Linq;
using MyX3DParser.Utils;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated.Model.Statements;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;

namespace MyX3DParser.Generated.Model.DataTypes
{
    public static partial class Image
    {
        public static UnityEngine.Texture2D Parse(string value)
        {
            if(value== "0 0 0")
            {
                return new UnityEngine.Texture2D(0, 0);
            }

            // TODO https://www.web3d.org/specifications/java/javadoc/org/web3d/x3d/jsail/fields/SFImage.html#setValue(int,int,int,int%5B%5D)
            throw new NotImplementedException();
        }

        public static string ToX3DString(UnityEngine.Texture2D value)
        {
            if(value.width==0 && value.height == 0)
            {
                return "0 0 0";
            }
            throw new NotImplementedException();
        }
    }
}