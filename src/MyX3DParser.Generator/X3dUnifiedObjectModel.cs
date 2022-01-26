#nullable disable

namespace MyX3DParser.Model {


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class X3dUnifiedObjectModel
    {

        private X3dUnifiedObjectModelSimpleType[] simpleTypeEnumerationsField;

        private X3dUnifiedObjectModelFieldType[] fieldTypesField;

        private X3dUnifiedObjectModelAbstractObjectType[] abstractObjectTypesField;

        private X3dUnifiedObjectModelAbstractNodeType[] abstractNodeTypesField;

        private X3dUnifiedObjectModelConcreteNode[] concreteNodesField;

        private X3dUnifiedObjectModelStatement[] statementsField;

        private decimal versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SimpleType", IsNullable = false)]
        public X3dUnifiedObjectModelSimpleType[] SimpleTypeEnumerations
        {
            get
            {
                return this.simpleTypeEnumerationsField;
            }
            set
            {
                this.simpleTypeEnumerationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FieldType", IsNullable = false)]
        public X3dUnifiedObjectModelFieldType[] FieldTypes
        {
            get
            {
                return this.fieldTypesField;
            }
            set
            {
                this.fieldTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AbstractObjectType", IsNullable = false)]
        public X3dUnifiedObjectModelAbstractObjectType[] AbstractObjectTypes
        {
            get
            {
                return this.abstractObjectTypesField;
            }
            set
            {
                this.abstractObjectTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AbstractNodeType", IsNullable = false)]
        public X3dUnifiedObjectModelAbstractNodeType[] AbstractNodeTypes
        {
            get
            {
                return this.abstractNodeTypesField;
            }
            set
            {
                this.abstractNodeTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ConcreteNode", IsNullable = false)]
        public X3dUnifiedObjectModelConcreteNode[] ConcreteNodes
        {
            get
            {
                return this.concreteNodesField;
            }
            set
            {
                this.concreteNodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Statement", IsNullable = false)]
        public X3dUnifiedObjectModelStatement[] Statements
        {
            get
            {
                return this.statementsField;
            }
            set
            {
                this.statementsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelSimpleType
    {

        private X3dUnifiedObjectModelSimpleTypeEnumeration[] enumerationField;

        private string nameField;

        private string baseTypeField;

        private string appinfoField;

        private string documentationField;

        private string defaultValueField;

        private string regexField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelSimpleTypeEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string defaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string regex
        {
            get
            {
                return this.regexField;
            }
            set
            {
                this.regexField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelSimpleTypeEnumeration
    {

        private string valueField;

        private string appinfoField;

        private string documentationField;

        private byte indexField;

        private bool indexFieldSpecified;

        private string descriptionField;

        private string aliasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indexSpecified
        {
            get
            {
                return this.indexFieldSpecified;
            }
            set
            {
                this.indexFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelFieldType
    {

        private X3dUnifiedObjectModelFieldTypeInterfaceDefinition interfaceDefinitionField;

        private string typeField;

        private byte tupleSizeField;

        private bool isArrayField;

        private string defaultValueField;

        private string regexField;

        /// <remarks/>
        public X3dUnifiedObjectModelFieldTypeInterfaceDefinition InterfaceDefinition
        {
            get
            {
                return this.interfaceDefinitionField;
            }
            set
            {
                this.interfaceDefinitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte tupleSize
        {
            get
            {
                return this.tupleSizeField;
            }
            set
            {
                this.tupleSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isArray
        {
            get
            {
                return this.isArrayField;
            }
            set
            {
                this.isArrayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string defaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string regex
        {
            get
            {
                return this.regexField;
            }
            set
            {
                this.regexField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelFieldTypeInterfaceDefinition
    {

        private X3dUnifiedObjectModelFieldTypeInterfaceDefinitionInheritance inheritanceField;

        private string specificationUrlField;

        private string appinfoField;

        /// <remarks/>
        public X3dUnifiedObjectModelFieldTypeInterfaceDefinitionInheritance Inheritance
        {
            get
            {
                return this.inheritanceField;
            }
            set
            {
                this.inheritanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string specificationUrl
        {
            get
            {
                return this.specificationUrlField;
            }
            set
            {
                this.specificationUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelFieldTypeInterfaceDefinitionInheritance
    {

        private string baseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectType
    {

        private X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinition interfaceDefinitionField;

        private string nameField;

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinition InterfaceDefinition
        {
            get
            {
                return this.interfaceDefinitionField;
            }
            set
            {
                this.interfaceDefinitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinition
    {

        private X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionComponentInfo componentInfoField;

        private X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionField[] fieldField;

        private string specificationUrlField;

        private string appinfoField;

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string specificationUrl
        {
            get
            {
                return this.specificationUrlField;
            }
            set
            {
                this.specificationUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionField
    {

        private X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldComponentInfo componentInfoField;

        private X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldEnumeration[] enumerationField;

        private string nameField;

        private string typeField;

        private string accessTypeField;

        private string defaultField;

        private string baseTypeField;

        private byte minInclusiveField;

        private bool minInclusiveFieldSpecified;

        private byte maxInclusiveField;

        private bool maxInclusiveFieldSpecified;

        private bool additionalEnumerationValuesAllowedField;

        private bool additionalEnumerationValuesAllowedFieldSpecified;

        private string simpleTypeField;

        private string inheritedFromField;

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string accessType
        {
            get
            {
                return this.accessTypeField;
            }
            set
            {
                this.accessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minInclusive
        {
            get
            {
                return this.minInclusiveField;
            }
            set
            {
                this.minInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minInclusiveSpecified
        {
            get
            {
                return this.minInclusiveFieldSpecified;
            }
            set
            {
                this.minInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte maxInclusive
        {
            get
            {
                return this.maxInclusiveField;
            }
            set
            {
                this.maxInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxInclusiveSpecified
        {
            get
            {
                return this.maxInclusiveFieldSpecified;
            }
            set
            {
                this.maxInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool additionalEnumerationValuesAllowed
        {
            get
            {
                return this.additionalEnumerationValuesAllowedField;
            }
            set
            {
                this.additionalEnumerationValuesAllowedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool additionalEnumerationValuesAllowedSpecified
        {
            get
            {
                return this.additionalEnumerationValuesAllowedFieldSpecified;
            }
            set
            {
                this.additionalEnumerationValuesAllowedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string simpleType
        {
            get
            {
                return this.simpleTypeField;
            }
            set
            {
                this.simpleTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string inheritedFrom
        {
            get
            {
                return this.inheritedFromField;
            }
            set
            {
                this.inheritedFromField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractObjectTypeInterfaceDefinitionFieldEnumeration
    {

        private string valueField;

        private string appinfoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeType
    {

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinition interfaceDefinitionField;

        private string nameField;

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinition InterfaceDefinition
        {
            get
            {
                return this.interfaceDefinitionField;
            }
            set
            {
                this.interfaceDefinitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinition
    {

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionComponentInfo componentInfoField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionInheritance inheritanceField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionAdditionalInheritance[] additionalInheritanceField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionField[] fieldField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModel contentModelField;

        private string specificationUrlField;

        private string appinfoField;

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionInheritance Inheritance
        {
            get
            {
                return this.inheritanceField;
            }
            set
            {
                this.inheritanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdditionalInheritance")]
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionAdditionalInheritance[] AdditionalInheritance
        {
            get
            {
                return this.additionalInheritanceField;
            }
            set
            {
                this.additionalInheritanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModel ContentModel
        {
            get
            {
                return this.contentModelField;
            }
            set
            {
                this.contentModelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string specificationUrl
        {
            get
            {
                return this.specificationUrlField;
            }
            set
            {
                this.specificationUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionInheritance
    {

        private string baseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionAdditionalInheritance
    {

        private string baseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionField
    {

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldEnumeration[] enumerationField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldComponentInfo componentInfoField;

        private string nameField;

        private string typeField;

        private string accessTypeField;

        private string defaultField;

        private string acceptableNodeTypesField;

        private string inheritedFromField;

        private string baseTypeField;

        private sbyte minInclusiveField;

        private bool minInclusiveFieldSpecified;

        private decimal maxInclusiveField;

        private bool maxInclusiveFieldSpecified;

        private string descriptionField;

        private byte minExclusiveField;

        private bool minExclusiveFieldSpecified;

        private bool additionalEnumerationValuesAllowedField;

        private bool additionalEnumerationValuesAllowedFieldSpecified;

        private string simpleTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string accessType
        {
            get
            {
                return this.accessTypeField;
            }
            set
            {
                this.accessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acceptableNodeTypes
        {
            get
            {
                return this.acceptableNodeTypesField;
            }
            set
            {
                this.acceptableNodeTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string inheritedFrom
        {
            get
            {
                return this.inheritedFromField;
            }
            set
            {
                this.inheritedFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte minInclusive
        {
            get
            {
                return this.minInclusiveField;
            }
            set
            {
                this.minInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minInclusiveSpecified
        {
            get
            {
                return this.minInclusiveFieldSpecified;
            }
            set
            {
                this.minInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal maxInclusive
        {
            get
            {
                return this.maxInclusiveField;
            }
            set
            {
                this.maxInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxInclusiveSpecified
        {
            get
            {
                return this.maxInclusiveFieldSpecified;
            }
            set
            {
                this.maxInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minExclusive
        {
            get
            {
                return this.minExclusiveField;
            }
            set
            {
                this.minExclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minExclusiveSpecified
        {
            get
            {
                return this.minExclusiveFieldSpecified;
            }
            set
            {
                this.minExclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool additionalEnumerationValuesAllowed
        {
            get
            {
                return this.additionalEnumerationValuesAllowedField;
            }
            set
            {
                this.additionalEnumerationValuesAllowedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool additionalEnumerationValuesAllowedSpecified
        {
            get
            {
                return this.additionalEnumerationValuesAllowedFieldSpecified;
            }
            set
            {
                this.additionalEnumerationValuesAllowedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string simpleType
        {
            get
            {
                return this.simpleTypeField;
            }
            set
            {
                this.simpleTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldEnumeration
    {

        private string valueField;

        private string appinfoField;

        private string documentationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionFieldComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModel
    {

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelGroupContentModel[] groupContentModelField;

        private X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelNodeContentModel[] nodeContentModelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GroupContentModel")]
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelGroupContentModel[] GroupContentModel
        {
            get
            {
                return this.groupContentModelField;
            }
            set
            {
                this.groupContentModelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NodeContentModel")]
        public X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelNodeContentModel[] NodeContentModel
        {
            get
            {
                return this.nodeContentModelField;
            }
            set
            {
                this.nodeContentModelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelGroupContentModel
    {

        private string nameField;

        private byte minOccursField;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minOccurs
        {
            get
            {
                return this.minOccursField;
            }
            set
            {
                this.minOccursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelAbstractNodeTypeInterfaceDefinitionContentModelNodeContentModel
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNode
    {

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinition interfaceDefinitionField;

        private string nameField;

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinition InterfaceDefinition
        {
            get
            {
                return this.interfaceDefinitionField;
            }
            set
            {
                this.interfaceDefinitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinition
    {

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionComponentInfo componentInfoField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionInheritance inheritanceField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionAdditionalInheritance[] additionalInheritanceField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionField[] fieldField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerField containerFieldField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModel contentModelField;

        private string specificationUrlField;

        private string appinfoField;

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionInheritance Inheritance
        {
            get
            {
                return this.inheritanceField;
            }
            set
            {
                this.inheritanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdditionalInheritance")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionAdditionalInheritance[] AdditionalInheritance
        {
            get
            {
                return this.additionalInheritanceField;
            }
            set
            {
                this.additionalInheritanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerField containerField
        {
            get
            {
                return this.containerFieldField;
            }
            set
            {
                this.containerFieldField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModel ContentModel
        {
            get
            {
                return this.contentModelField;
            }
            set
            {
                this.contentModelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string specificationUrl
        {
            get
            {
                return this.specificationUrlField;
            }
            set
            {
                this.specificationUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionInheritance
    {

        private string baseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionAdditionalInheritance
    {

        private string baseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionField
    {

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldEnumeration[] enumerationField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldComponentInfo[] componentInfoField;

        private string nameField;

        private string typeField;

        private string accessTypeField;

        private string acceptableNodeTypesField;

        private string inheritedFromField;

        private string defaultField;

        private string baseTypeField;

        private decimal minExclusiveField;

        private bool minExclusiveFieldSpecified;

        private decimal maxExclusiveField;

        private bool maxExclusiveFieldSpecified;

        private bool additionalEnumerationValuesAllowedField;

        private bool additionalEnumerationValuesAllowedFieldSpecified;

        private string simpleTypeField;

        private sbyte minInclusiveField;

        private bool minInclusiveFieldSpecified;

        private decimal maxInclusiveField;

        private bool maxInclusiveFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("componentInfo")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldComponentInfo[] componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string accessType
        {
            get
            {
                return this.accessTypeField;
            }
            set
            {
                this.accessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acceptableNodeTypes
        {
            get
            {
                return this.acceptableNodeTypesField;
            }
            set
            {
                this.acceptableNodeTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string inheritedFrom
        {
            get
            {
                return this.inheritedFromField;
            }
            set
            {
                this.inheritedFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal minExclusive
        {
            get
            {
                return this.minExclusiveField;
            }
            set
            {
                this.minExclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minExclusiveSpecified
        {
            get
            {
                return this.minExclusiveFieldSpecified;
            }
            set
            {
                this.minExclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal maxExclusive
        {
            get
            {
                return this.maxExclusiveField;
            }
            set
            {
                this.maxExclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxExclusiveSpecified
        {
            get
            {
                return this.maxExclusiveFieldSpecified;
            }
            set
            {
                this.maxExclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool additionalEnumerationValuesAllowed
        {
            get
            {
                return this.additionalEnumerationValuesAllowedField;
            }
            set
            {
                this.additionalEnumerationValuesAllowedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool additionalEnumerationValuesAllowedSpecified
        {
            get
            {
                return this.additionalEnumerationValuesAllowedFieldSpecified;
            }
            set
            {
                this.additionalEnumerationValuesAllowedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string simpleType
        {
            get
            {
                return this.simpleTypeField;
            }
            set
            {
                this.simpleTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte minInclusive
        {
            get
            {
                return this.minInclusiveField;
            }
            set
            {
                this.minInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minInclusiveSpecified
        {
            get
            {
                return this.minInclusiveFieldSpecified;
            }
            set
            {
                this.minInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal maxInclusive
        {
            get
            {
                return this.maxInclusiveField;
            }
            set
            {
                this.maxInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxInclusiveSpecified
        {
            get
            {
                return this.maxInclusiveFieldSpecified;
            }
            set
            {
                this.maxInclusiveFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldEnumeration
    {

        private string valueField;

        private string appinfoField;

        private byte indexField;

        private bool indexFieldSpecified;

        private string descriptionField;

        private string documentationField;

        private string aliasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indexSpecified
        {
            get
            {
                return this.indexFieldSpecified;
            }
            set
            {
                this.indexFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionFieldComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerField
    {

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerFieldEnumeration[] enumerationField;

        private string defaultField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerFieldEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContainerFieldEnumeration
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModel
    {

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelFieldDeclaration fieldDeclarationField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelGroupContentModel[] groupContentModelField;

        private object sourceTextField;

        private X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelNodeContentModel[] nodeContentModelField;

        /// <remarks/>
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelFieldDeclaration FieldDeclaration
        {
            get
            {
                return this.fieldDeclarationField;
            }
            set
            {
                this.fieldDeclarationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GroupContentModel")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelGroupContentModel[] GroupContentModel
        {
            get
            {
                return this.groupContentModelField;
            }
            set
            {
                this.groupContentModelField = value;
            }
        }

        /// <remarks/>
        public object SourceText
        {
            get
            {
                return this.sourceTextField;
            }
            set
            {
                this.sourceTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NodeContentModel")]
        public X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelNodeContentModel[] NodeContentModel
        {
            get
            {
                return this.nodeContentModelField;
            }
            set
            {
                this.nodeContentModelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelFieldDeclaration
    {

        private byte minOccursField;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minOccurs
        {
            get
            {
                return this.minOccursField;
            }
            set
            {
                this.minOccursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelGroupContentModel
    {

        private string nameField;

        private byte minOccursField;

        private bool minOccursFieldSpecified;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minOccurs
        {
            get
            {
                return this.minOccursField;
            }
            set
            {
                this.minOccursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minOccursSpecified
        {
            get
            {
                return this.minOccursFieldSpecified;
            }
            set
            {
                this.minOccursFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelConcreteNodeInterfaceDefinitionContentModelNodeContentModel
    {

        private string nameField;

        private byte minOccursField;

        private bool minOccursFieldSpecified;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minOccurs
        {
            get
            {
                return this.minOccursField;
            }
            set
            {
                this.minOccursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minOccursSpecified
        {
            get
            {
                return this.minOccursFieldSpecified;
            }
            set
            {
                this.minOccursFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatement
    {

        private X3dUnifiedObjectModelStatementInterfaceDefinition interfaceDefinitionField;

        private string nameField;

        /// <remarks/>
        public X3dUnifiedObjectModelStatementInterfaceDefinition InterfaceDefinition
        {
            get
            {
                return this.interfaceDefinitionField;
            }
            set
            {
                this.interfaceDefinitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinition
    {

        private X3dUnifiedObjectModelStatementInterfaceDefinitionComponentInfo componentInfoField;

        private X3dUnifiedObjectModelStatementInterfaceDefinitionField[] fieldField;

        private X3dUnifiedObjectModelStatementInterfaceDefinitionContentModel contentModelField;

        private string specificationUrlField;

        private string appinfoField;

        /// <remarks/>
        public X3dUnifiedObjectModelStatementInterfaceDefinitionComponentInfo componentInfo
        {
            get
            {
                return this.componentInfoField;
            }
            set
            {
                this.componentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public X3dUnifiedObjectModelStatementInterfaceDefinitionField[] field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        public X3dUnifiedObjectModelStatementInterfaceDefinitionContentModel ContentModel
        {
            get
            {
                return this.contentModelField;
            }
            set
            {
                this.contentModelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string specificationUrl
        {
            get
            {
                return this.specificationUrlField;
            }
            set
            {
                this.specificationUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionComponentInfo
    {

        private string nameField;

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionField
    {

        private X3dUnifiedObjectModelStatementInterfaceDefinitionFieldEnumeration[] enumerationField;

        private string nameField;

        private string typeField;

        private string accessTypeField;

        private string defaultField;

        private byte minInclusiveField;

        private bool minInclusiveFieldSpecified;

        private byte maxInclusiveField;

        private bool maxInclusiveFieldSpecified;

        private string useField;

        private bool additionalEnumerationValuesAllowedField;

        private bool additionalEnumerationValuesAllowedFieldSpecified;

        private string simpleTypeField;

        private string baseTypeField;

        private string acceptableNodeTypesField;

        private byte minExclusiveField;

        private bool minExclusiveFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("enumeration")]
        public X3dUnifiedObjectModelStatementInterfaceDefinitionFieldEnumeration[] enumeration
        {
            get
            {
                return this.enumerationField;
            }
            set
            {
                this.enumerationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string accessType
        {
            get
            {
                return this.accessTypeField;
            }
            set
            {
                this.accessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minInclusive
        {
            get
            {
                return this.minInclusiveField;
            }
            set
            {
                this.minInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minInclusiveSpecified
        {
            get
            {
                return this.minInclusiveFieldSpecified;
            }
            set
            {
                this.minInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte maxInclusive
        {
            get
            {
                return this.maxInclusiveField;
            }
            set
            {
                this.maxInclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxInclusiveSpecified
        {
            get
            {
                return this.maxInclusiveFieldSpecified;
            }
            set
            {
                this.maxInclusiveFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string use
        {
            get
            {
                return this.useField;
            }
            set
            {
                this.useField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool additionalEnumerationValuesAllowed
        {
            get
            {
                return this.additionalEnumerationValuesAllowedField;
            }
            set
            {
                this.additionalEnumerationValuesAllowedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool additionalEnumerationValuesAllowedSpecified
        {
            get
            {
                return this.additionalEnumerationValuesAllowedFieldSpecified;
            }
            set
            {
                this.additionalEnumerationValuesAllowedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string simpleType
        {
            get
            {
                return this.simpleTypeField;
            }
            set
            {
                this.simpleTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string baseType
        {
            get
            {
                return this.baseTypeField;
            }
            set
            {
                this.baseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acceptableNodeTypes
        {
            get
            {
                return this.acceptableNodeTypesField;
            }
            set
            {
                this.acceptableNodeTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minExclusive
        {
            get
            {
                return this.minExclusiveField;
            }
            set
            {
                this.minExclusiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minExclusiveSpecified
        {
            get
            {
                return this.minExclusiveFieldSpecified;
            }
            set
            {
                this.minExclusiveFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionFieldEnumeration
    {

        private string valueField;

        private string appinfoField;

        private string documentationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string appinfo
        {
            get
            {
                return this.appinfoField;
            }
            set
            {
                this.appinfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionContentModel
    {

        private X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelGroupContentModel[] groupContentModelField;

        private X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelStatementContentModel[] statementContentModelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GroupContentModel")]
        public X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelGroupContentModel[] GroupContentModel
        {
            get
            {
                return this.groupContentModelField;
            }
            set
            {
                this.groupContentModelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StatementContentModel")]
        public X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelStatementContentModel[] StatementContentModel
        {
            get
            {
                return this.statementContentModelField;
            }
            set
            {
                this.statementContentModelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelGroupContentModel
    {

        private string nameField;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class X3dUnifiedObjectModelStatementInterfaceDefinitionContentModelStatementContentModel
    {

        private string nameField;

        private byte minOccursField;

        private bool minOccursFieldSpecified;

        private string maxOccursField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minOccurs
        {
            get
            {
                return this.minOccursField;
            }
            set
            {
                this.minOccursField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool minOccursSpecified
        {
            get
            {
                return this.minOccursFieldSpecified;
            }
            set
            {
                this.minOccursFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string maxOccurs
        {
            get
            {
                return this.maxOccursField;
            }
            set
            {
                this.maxOccursField = value;
            }
        }
    }



}