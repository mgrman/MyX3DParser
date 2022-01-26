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
    public class RoutingTests
    {
        private readonly ITestOutputHelper output;


        public RoutingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_Route()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <TouchSensor DEF='Clicker' description='click to animate'/>
    <TimeSensor DEF='TimeSource' cycleInterval='2.0'/>
    <ROUTE fromField='touchTime' fromNode='Clicker' toField='startTime' toNode='TimeSource'/>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, x3dContext);

           var clicker= x3dContext.GetUSE("Clicker") as TouchSensor;
            var timeSource = x3dContext.GetUSE("TimeSource") as TimeSensor;

            var time1 = 123f;
            clicker.touchTime.Value = time1;
            Assert.Equal(time1, timeSource.startTime.Value);

            var time2 = 0;
            clicker.touchTime.Value = time2;
            Assert.Equal(time2, timeSource.startTime.Value);

            var time3 = 5000.5f;
            clicker.touchTime.Value = time3;
            Assert.Equal(time3, timeSource.startTime.Value);
        }

        [Fact]
        public void Test_ChainedRoute()
        {
            var x3dText = @"
<X3D>
  <Scene>
      <TouchSensor DEF='Clicker' description='click to animate'/>
      <TimeSensor DEF='TimeSource' cycleInterval='2.0'/>
      <TimeSensor DEF='TimeSource2' cycleInterval='2.0'/>

    <ROUTE fromField='touchTime' fromNode='Clicker' toField='startTime' toNode='TimeSource'/>
    <ROUTE fromField='startTime' fromNode='TimeSource' toField='startTime' toNode='TimeSource2'/>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, x3dContext);

            var clicker = x3dContext.GetUSE("Clicker") as TouchSensor;
            var timeSource2 = x3dContext.GetUSE("TimeSource2") as TimeSensor;

            var time1 = 123f;
            clicker.touchTime.Value = time1;
            Assert.Equal(time1, timeSource2.startTime.Value);

            var time2 = 0;
            clicker.touchTime.Value = time2;
            Assert.Equal(time2, timeSource2.startTime.Value);

            var time3 = 5000.5f;
            clicker.touchTime.Value = time3;
            Assert.Equal(time3, timeSource2.startTime.Value);
        }

        [Fact]
        public void Test_ChangeEventOnRoute()
        {
            var x3dText = @"
<X3D>
  <Scene>
    <TouchSensor DEF='Clicker' description='click to animate'/>
    <TimeSensor DEF='TimeSource' cycleInterval='2.0'/>
    <ROUTE fromField='touchTime' fromNode='Clicker' toField='startTime' toNode='TimeSource'/>
  </Scene>
</X3D>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(x3dText);
            var x3dContext = new X3DContext();
            var x3d = Parser.Parse_X3D(xmlDoc.DocumentElement, x3dContext);

            var clicker = x3dContext.GetUSE("Clicker") as TouchSensor;
            var timeSource = x3dContext.GetUSE("TimeSource") as TimeSensor;
            var values = new List<float>();
            timeSource.startTime.OnChange += o => values.Add((o as SFTime).Value);

            var time1 = 123f;
            clicker.touchTime.Value = time1;
            Assert.True(values.SequenceEqual(new[] { time1 }));

            var time2 = 0;
            clicker.touchTime.Value = time2;
            Assert.True(values.SequenceEqual(new[] { time1, time2 }));

            var time3 = 5000.5f;
            clicker.touchTime.Value = time3;
            Assert.True(values.SequenceEqual(new[] { time1, time2, time3 }));
        }
    }
}