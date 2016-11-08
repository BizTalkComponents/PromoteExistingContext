using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;

namespace BizTalkComponents.PipelineComponents.PromoteContextProperty.UnitTest
{
    [TestClass]
    public class PromoteContextPropertyComponentTest
    {
        [TestMethod]
        public void Execute_ContextPropertyExists_PropertyPromoted()
        {
            // arrange
            var pipeline = PipelineFactory.CreateEmptySendPipeline();

            var component = new PromoteContextPropertyComponent
            {
                PropertyName = "name",
                PropertySchema = "schema",
            };

            pipeline.AddComponent(component, PipelineStage.PreAssemble);

            var message = MessageHelper.Create("<test></test>");
            message.Context.Write("name", "schema", "banan");

            // act
            var result = pipeline.Execute(message);

            // assert
            Assert.AreEqual("banan", result.Context.Read("name", "schema"));
            Assert.IsTrue(result.Context.IsPromoted("name", "schema"));
        }
    }
}
