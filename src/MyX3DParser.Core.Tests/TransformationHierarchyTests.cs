using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MyX3DParser.Generated.Model;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.DataTypes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using MyX3DParser.Generated.Model.Nodes;
using MyX3DParser.Generated;

namespace MyX3DParser.Tests
{
    public class TransformationHierarchyTests
    {
        private readonly ITestOutputHelper output;


        public TransformationHierarchyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test01()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <Group>
        <Shape DEF='shape'/>
    </Group>
    <Transform>
        <Shape USE='shape'/>
    </Transform>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement!, x3dContext);

            var shape = x3dContext.GetUSE("shape") as Shape;

            Assert.Collection(shape.MyPositions, o => Assert.Equal(Shared.SceneNodeData.Identity, o), o => Assert.Equal(Shared.SceneNodeData.Identity, o));
        }


        [Fact]
        public void SwitchTest()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <Switch whichChoice='-1'>
        <Shape DEF='shape'/>
    </Switch>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement!, x3dContext);

            var shape = x3dContext.GetUSE("shape") as Shape;

            Assert.Collection(shape.MyPositions, o => Assert.Equal(Shared.SceneNodeData.Deactivation, o));
        }

    }
}