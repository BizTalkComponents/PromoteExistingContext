/*
Steps to complete:
1. Rename the component (MyPipelineComponent
2. Relace "[CLASS ID - REPLACE WITH GUID]" in this file with a guid (no angle brackets).
3. In the IBaseComponent region, update the Name and the Description properties
4. Add your logic in the Execute method (IComponent region)

Good luck
*/

namespace BizTalkComponents.PipelineComponents.PromoteContextProperty
{
    using System;
    using System.Resources;
    using System.Drawing;
    using System.Collections;
    using System.Reflection;
    using System.ComponentModel;
    using System.Text;
    using System.IO;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("0B8B1234-BEB2-4F00-8414-37551D3C95C4")]
    public class PromoteContextPropertyComponent : IBaseComponent,
                    Microsoft.BizTalk.Component.Interop.IComponent,
                    Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
                    IComponentUI
    {
        [DisplayName("Name")]
        [Description("Name of context property. Ex: ReceiverPartyName")]
        public string PropertyName { get; set; }

        [DisplayName("Schema")]
        [Description("Schema of context property. Ex: http://schemas.microsoft.com/Edi/PropertySchema")]
        public string PropertySchema { get; set; }

        #region IBaseComponent

        /// <summary>
        /// Name of the component.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get { return "PromoteContextProperty"; }
        }

        /// <summary>
        /// Version of the component.
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get { return "1.0"; }
        }

        /// <summary>
        /// Description of the component.
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get { return "Promotes a context property."; }
        }

        #endregion

        #region IComponent

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message.</param>
        /// <returns>Processed input message with appended or prepended data.</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in pipeline component.
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
        {
            var property = inmsg.Context.Read(this.PropertyName, this.PropertySchema);

            if (property != null)
            {
                inmsg.Context.Promote(this.PropertyName, this.PropertySchema, property);
            }

            return inmsg;
        }
        #endregion

        #region IPersistPropertyBag

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("FA8C71C3-313A-4086-97DA-657DD1CB39C9");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            PropertyName = ReadPropertyBag<string>(pb, "PropertyName") ?? "";
            PropertySchema = ReadPropertyBag<string>(pb, "PropertySchema") ?? "";
        }

        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            object val = PropertyName;
            WritePropertyBag(pb, "PropertyName", val);

            val = PropertySchema;
            WritePropertyBag(pb, "PropertySchema", val);
        }

        /// <summary>
        /// Reads property value from property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <returns>Value of the property.</returns>
        private static T ReadPropertyBag<T>(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            try
            {
                object val = null;
                pb.Read(propName, out val, 0);

                return val == null ? default(T) : (T)val;
            }
            catch (ArgumentException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private static void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            pb.Write(propName, ref val);
        }

        #endregion

        #region IComponentUI

        /// <summary>
        /// Component icon to use in BizTalk Editor.
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return IntPtr.Zero;
            }

        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">Project system.</param>
        /// <returns>
        /// A list of error and/or warning messages encounter during validation
        /// of this component.
        /// </returns>
        public IEnumerator Validate(object obj)
        {
            IEnumerator enumerator = null;
            return enumerator;
        }

        #endregion
    }
}

