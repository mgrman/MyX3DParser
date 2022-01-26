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
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using MyX3DParser.Generated.Model.Nodes;

namespace MyX3DParser.Tests
{
    public class IsConnectionTests
    {
        private readonly ITestOutputHelper output;


        public IsConnectionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_InputOutput()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <ProtoDeclare name='TwoColorTable'>
      <ProtoInterface>
        <field accessType='inputOutput' name='legColor' type='SFColor' value='.8 .4 .7'/>
      </ProtoInterface>
      <ProtoBody>
        <Shape DEF='Leg'>
          <Appearance>
            <Material DEF='LegMaterial' diffuseColor='1.0 0.0 0.0'>
              <IS>
                <connect nodeField='diffuseColor' protoField='legColor'/>
              </IS>
            </Material>
          </Appearance>
          <Cylinder height='1.0' radius='0.1'/>
        </Shape>
        <ColorInterpolator DEF='InnerAnim' />
      </ProtoBody>
    </ProtoDeclare>
    <ProtoInstance name='TwoColorTable' DEF='Table'>
      <fieldValue name='legColor' value='1 0 0'/>
    </ProtoInstance>
    <ColorInterpolator DEF='ColorAnim' />
    <ROUTE fromField='value_changed' fromNode='ColorAnim' toField='legColor' toNode='Table'/>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement!, x3dContext);

            var colorAnim = x3dContext.GetUSE("ColorAnim") as ColorInterpolator;
            var table = x3dContext.GetUSE("Table") as ProtoInstance;
            var legMaterial = table!.ChildContext.GetUSE("LegMaterial") as Material;


            var color1 = new Generated.Model.DataTypes.Color(0.5f, 0.6f, 0.7f);
            colorAnim!.value_changed.Value = color1;
            Assert.Equal(color1, (table.GetInputField("legColor") as SFColor)!.Value);
            Assert.Equal(color1, legMaterial!.diffuseColor.Value);

            var color2 = new Generated.Model.DataTypes.Color(0.7f,0.8f,0.9f);
            colorAnim.value_changed.Value = color2;
            Assert.Equal(color2, (table.GetInputField("legColor") as SFColor)!.Value);
            Assert.Equal(color2, legMaterial.diffuseColor.Value);
        }

        [Fact]
        public void Test_OutputOnly()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <ProtoDeclare name='dampingSetup'>
      <ProtoInterface>
        <field accessType='outputOnly' name='stopTime' type='SFTime' value='0'/>
      </ProtoInterface>
      <ProtoBody>
     
        <Collision DEF='collider' >
          <IS>
            <connect nodeField='collideTime' protoField='stopTime'/>
          </IS>
        </Collision>
      </ProtoBody>
    </ProtoDeclare>
    <ProtoInstance name='dampingSetup'  DEF='bbb'>
    <fieldValue name='speed' value='2'/>
    </ProtoInstance>
    <ColorDamper DEF='DAMPER'/>
    <ROUTE fromField='stopTime' fromNode='bbb' toField='tau' toNode='DAMPER'/>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement!, x3dContext);

            var damper = x3dContext.GetUSE("DAMPER") as ColorDamper;
            var protoInstance = x3dContext.GetUSE("bbb") as ProtoInstance;
            var collider = protoInstance!.ChildContext.GetUSE("collider") as Collision;


            var time1 = 0f;
            collider.collideTime.Value = time1;
            Assert.Equal(time1, (protoInstance.GetOutputField("stopTime") as SFTime)!.Value);
            Assert.Equal(time1, damper!.tau.Value);

            var time2 = 10f;
            collider.collideTime.Value = time2;
            Assert.Equal(time2, (protoInstance.GetOutputField("stopTime") as SFTime)!.Value);
            Assert.Equal(time2, damper!.tau.Value);
        }
    }
}