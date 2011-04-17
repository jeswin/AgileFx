<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="5e23cd82-1d47-4904-92bd-59576494a990" Description="Create Models for the AgileFx ORM Framework" Name="AgileModeler" DisplayName="AgileModeler" Namespace="AgileFx.AgileModeler" ProductName="AgileModeler" CompanyName="AgileHead" PackageGuid="17c24251-0810-4e51-8cf8-c24f0dd3cab4" PackageNamespace="" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="852e53ee-a50b-43af-8a50-f4cbdfac45f2" Description="" Name="NamedElement" DisplayName="Named Element" InheritanceModifier="Abstract" Namespace="AgileFx.AgileModeler">
      <Properties>
        <DomainProperty Id="a3163d4c-5cf7-40da-835b-cd89b92e4a4f" Description="" Name="Name" DisplayName="Name" DefaultValue="" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="ae832652-32a1-4e2b-98a3-bdbbe7ef93c9" Description="" Name="ModelRoot" DisplayName="Model Root" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="f70a8c52-27f9-4db1-a17e-42f47ddc3099" Description="Description for AgileFx.AgileModeler.ModelRoot.Namespace" Name="Namespace" DisplayName="Namespace">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d803f3f3-2dd2-4b33-b646-9812cccbdd04" Description="Description for AgileFx.AgileModeler.ModelRoot.Connection String" Name="ConnectionString" DisplayName="Connection String">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="48ba7fda-e15a-4bc5-99a3-0f2a1e0fc16e" Description="Description for AgileFx.AgileModeler.ModelRoot.Data Context Name" Name="DataContextName" DisplayName="Data Context Name" DefaultValue="Entities">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelType" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ModelRootHasTypes.Types</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="48711efd-ce6b-4b47-ae89-2c4e99cd0d2b" Description="" Name="ModelClass" DisplayName="Model Class" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="ModelType" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="0431eb98-e31d-4755-a911-4d4deab94e26" Description="Description for AgileFx.AgileModeler.ModelClass.Table Name" Name="TableName" DisplayName="Table Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7792b646-c1ae-48fd-87d0-9404b85de990" Description="Comma separated list of base class and implemented interfaces." Name="DerivesOrImplements" DisplayName="Derives Or Implements">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ModelField" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasFields.Fields</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="NavigationProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ClassHasNavigationProperties.NavigationProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="2f788ecd-ffbe-481c-8821-7e15721b2434" Description="A field of a class." Name="ModelFieldBase" DisplayName="Model Field Base" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="ec4dfa5f-587a-4d30-ab82-4625c2988eab" Description="Description for AgileFx.AgileModeler.ModelFieldBase.Getter" Name="Getter" DisplayName="Getter" DefaultValue="Public">
          <Type>
            <DomainEnumerationMoniker Name="AccessModifier" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="615b2d24-2843-433d-8f2b-62f877ba9805" Description="Description for AgileFx.AgileModeler.ModelFieldBase.Setter" Name="Setter" DisplayName="Setter" DefaultValue="Public">
          <Type>
            <DomainEnumerationMoniker Name="AccessModifier" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="521dfae9-0f47-4c24-9e2f-7902019e6a12" Description="Description for AgileFx.AgileModeler.ModelFieldBase.Is Edited" Name="IsEdited" DisplayName="Is Edited" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="4468e5d5-db30-4ac0-9e9c-2d3b342c809f" Description="" Name="ModelType" DisplayName="Model Type" InheritanceModifier="Abstract" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="ClassModelElement" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="162c3685-b1dd-4e93-b20f-22ab0264c84c" Description="Element with a Description" Name="ClassModelElement" DisplayName="Class Model Element" InheritanceModifier="Abstract" Namespace="AgileFx.AgileModeler">
      <Notes>Abstract base of all elements that have a Description property.</Notes>
      <BaseClass>
        <DomainClassMoniker Name="NamedElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="8133f562-0dfe-4965-9216-f2bfd0266b64" Description="This is a Description." Name="Description" DisplayName="Description" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="af2f396d-c36d-4b8e-adf8-595b12ac9f9c" Description="Description for AgileFx.AgileModeler.NavigationProperty" Name="NavigationProperty" DisplayName="Navigational Property" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="ModelFieldBase" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="519b2b72-00d6-41db-a5e3-d94ed8f3f299" Description="Description for AgileFx.AgileModeler.NavigationProperty.Multiplicity" Name="Multiplicity" DisplayName="Multiplicity">
          <Type>
            <DomainEnumerationMoniker Name="Multiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b4a8f4f3-3e3d-4f4d-9b3b-f6f386c84e5f" Description="Description for AgileFx.AgileModeler.NavigationProperty.Association" Name="Association" DisplayName="Association" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0ba32189-c16f-43f1-91e1-e5d3038364e4" Description="Description for AgileFx.AgileModeler.NavigationProperty.Type" Name="Type" DisplayName="Type" DefaultValue="" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="87e94410-8464-479e-bda8-ad32acd626f9" Description="Description for AgileFx.AgileModeler.NavigationProperty.Is Foreignkey" Name="IsForeignkey" DisplayName="Is Foreignkey">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d3ad967f-7022-43ee-b454-c160f385aa3f" Description="Description for AgileFx.AgileModeler.NavigationProperty.Foreignkey Column" Name="ForeignkeyColumn" DisplayName="Foreignkey Column">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e7b1b890-e658-4364-9bfc-30a5259550e3" Description="Description for AgileFx.AgileModeler.ModelField" Name="ModelField" DisplayName="Model Field" Namespace="AgileFx.AgileModeler">
      <BaseClass>
        <DomainClassMoniker Name="ModelFieldBase" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="03fa7acc-038b-49a1-a795-4f0d0406f576" Description="Description for AgileFx.AgileModeler.ModelField.Is Primary Key" Name="IsPrimaryKey" DisplayName="Is Primary Key" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5375c3ea-db9c-4864-a573-93f3e0cbb348" Description="Description for AgileFx.AgileModeler.ModelField.Database Column Name" Name="DatabaseColumnName" DisplayName="Database Column Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="21898c20-33a4-4f4d-925e-006b80c1060b" Description="Description for AgileFx.AgileModeler.ModelField.Default Value" Name="DefaultValue" DisplayName="Default Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d31f628c-b7c1-42a5-8839-17fc67f7eb01" Description="Description for AgileFx.AgileModeler.ModelField.Nullable" Name="Nullable" DisplayName="Nullable" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cf92162f-78a7-41ec-bb40-92b8f9bf14d2" Description="Description for AgileFx.AgileModeler.ModelField.Type" Name="Type" DisplayName="Type" DefaultValue="String">
          <Type>
            <DomainEnumerationMoniker Name="BuiltInTypes" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0d14dcc1-a68a-4069-a0c2-acc08f45f042" Description="Description for AgileFx.AgileModeler.ModelField.Is Db Generated" Name="IsDbGenerated" DisplayName="Is Db Generated" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="18e6e2e8-2125-4d63-990b-c335acde480d" Description="Description for AgileFx.AgileModeler.ModelField.Is Fixed Length" Name="IsFixedLength" DisplayName="Is Fixed Length" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e3220c43-1d6b-44e9-b078-9c0b9a6afbcd" Description="Description for AgileFx.AgileModeler.ModelField.Is Unicode" Name="IsUnicode" DisplayName="Is Unicode" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="77ee6a48-f01f-4af0-8275-1e0f0cde3736" Description="Description for AgileFx.AgileModeler.ModelField.Max Length" Name="MaxLength" DisplayName="Max Length" DefaultValue="50">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5a4af3c0-7987-456e-af87-bde46dc72c1a" Description="Description for AgileFx.AgileModeler.ModelField.Is Identity" Name="IsIdentity" DisplayName="Is Identity" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d445c7e0-1764-4e64-add4-aa3baa2cc539" Description="Description for AgileFx.AgileModeler.ModelField.Is Unique" Name="IsUnique" DisplayName="Is Unique" DefaultValue="False">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5bea24c6-34bc-4ef1-979e-888b74b14b23" Description="Description for AgileFx.AgileModeler.ModelField.Update Check" Name="UpdateCheck" DisplayName="Update Check" DefaultValue="Always">
          <Type>
            <DomainEnumerationMoniker Name="ConcurrencyCheckFrequency" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="617ad4d6-9c6d-4b78-a982-0f514754891c" Description="Associations between Classes." Name="Association" DisplayName="Association" Namespace="AgileFx.AgileModeler" AllowsDuplicates="true">
      <Notes>This is the relationship of the several kinds of association between Classes.
      It defines the Properties that are attached to association.</Notes>
      <Properties>
        <DomainProperty Id="6ce1cc03-5152-4077-8174-07c20afdb8d6" Description="" Name="End1Multiplicity" DisplayName="End1 Multiplicity" DefaultValue="One" Category="End1">
          <Type>
            <DomainEnumerationMoniker Name="Multiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="583c0091-7733-4c22-aa95-e4a7acea914a" Description="" Name="End1RoleName" DisplayName="End1 Role Name" DefaultValue="" Category="End1" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ef7f0bb1-58bf-4fdc-9097-f7f4b9b871d3" Description="" Name="End2Multiplicity" DisplayName="End2 Multiplicity" DefaultValue="ZeroMany" Category="End2">
          <Type>
            <DomainEnumerationMoniker Name="Multiplicity" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="4f4aad05-856b-4fa6-a1b8-1c86a9fbe306" Description="" Name="End2RoleName" DisplayName="End2 Role Name" DefaultValue="" Category="End2" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="99bee160-951d-4f80-9d86-c92ab8236343" Description="Description for AgileFx.AgileModeler.Association.End1 Navigation Property" Name="End1NavigationProperty" DisplayName="End1 Navigation Property" Category="End1">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="18567a08-d1aa-4f01-9ce6-bbd247b73dd1" Description="Description for AgileFx.AgileModeler.Association.End2 Navigation Property" Name="End2NavigationProperty" DisplayName="End2 Navigation Property" Category="End2">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="81ffb129-b8d3-4f28-8cc8-6d657d63c1df" Description="Description for AgileFx.AgileModeler.Association.Name" Name="Name" DisplayName="Name" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="31007fe1-fd24-4064-9e30-c7c2e9cbc3aa" Description="Description for AgileFx.AgileModeler.Association.Many To Many Mapping Table" Name="ManyToManyMappingTable" DisplayName="Many To Many Mapping Table" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="cbb2ef24-8b94-4f36-b6e7-99fc524cd5d8" Description="Description for AgileFx.AgileModeler.Association.End1 Many To Many Mapping Column" Name="End1ManyToManyMappingColumn" DisplayName="End1 Many To Many Mapping Column" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c724fdad-3210-43f7-a903-93a889525c7b" Description="Description for AgileFx.AgileModeler.Association.End2 Many To Many Mapping Column" Name="End2ManyToManyMappingColumn" DisplayName="End2 Many To Many Mapping Column" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="065954aa-03f6-471a-9217-35cc3818910d" Description="Description for AgileFx.AgileModeler.Association.End1 Multiplicity Display" Name="End1MultiplicityDisplay" DisplayName="End1 Multiplicity Display" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7ccaabfb-ddfe-40b4-9bb5-985f89b27bbb" Description="Description for AgileFx.AgileModeler.Association.End2 Multiplicity Display" Name="End2MultiplicityDisplay" DisplayName="End2 Multiplicity Display" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="009a5c71-2903-4dd7-a277-6a7d7f087d55" Description="Description for AgileFx.AgileModeler.Association.Is Edited" Name="IsEdited" DisplayName="Is Edited" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3501ae43-6bbc-4c42-adbd-c0c0e557adf2" Description="Description for AgileFx.AgileModeler.Association.End1 Many To Many Navigation Property" Name="End1ManyToManyNavigationProperty" DisplayName="End1 Many To Many Navigation Property" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b6bbb146-07ff-49d0-851a-e9506d54a8f6" Description="Description for AgileFx.AgileModeler.Association.End2 Many To Many Navigation Property" Name="End2ManyToManyNavigationProperty" DisplayName="End2 Many To Many Navigation Property" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f5d000e0-e321-49f1-801d-a5256d1c82b2" Description="Description for AgileFx.AgileModeler.Association.End1 Many To Many Field Name" Name="End1ManyToManyFieldName" DisplayName="End1 Many To Many Field Name" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="350cf9b8-82da-4b2f-8e86-94c800e8848f" Description="Description for AgileFx.AgileModeler.Association.End2 Many To Many Field Name" Name="End2ManyToManyFieldName" DisplayName="End2 Many To Many Field Name" Category="Many To Many Mapping Table">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="a6488cc7-8eae-4373-9897-99288f9e506c" Description="" Name="Source" DisplayName="Source" PropertyName="Targets" PropertyDisplayName="Targets">
          <Notes>The Targets property on a ModelClass will include all the elements targeted by every kind of Association.</Notes>
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9e237f24-7f7f-4b76-b49c-4161a75f1a64" Description="" Name="Target" DisplayName="Target" PropertyName="Sources" PropertyDisplayName="Sources">
          <Notes>The Sources property on a ModelClass will include all the elements sourced by every kind of Association.</Notes>
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="15ca8a10-fff6-4d43-a147-fdbdb9b12649" Description="Inheritance between Classes." Name="Inheritance" DisplayName="Inheritance" Namespace="AgileFx.AgileModeler">
      <Properties>
        <DomainProperty Id="ffc72a21-4822-42c3-9ef6-c77d9d29854c" Description="" Name="Discriminator" DisplayName="Discriminator" DefaultValue="">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="767eec2d-6f10-4a7d-a978-25a19ad507a5" Description="Description for AgileFx.AgileModeler.Inheritance.Derived Class Primary Key Column" Name="DerivedClassPrimaryKeyColumn" DisplayName="Derived Class Primary Key Column">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="48adf7ba-e5ec-40ad-977d-6f183a9a4121" Description="Description for AgileFx.AgileModeler.Inheritance.Base Class Primary Key Column" Name="BaseClassPrimaryKeyColumn" DisplayName="Base Class Primary Key Column">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="2297342a-9cb8-45cb-ab4a-7d4ba47f1afc" Description="" Name="Superclass" DisplayName="Superclass" PropertyName="Subclasses" PropertyDisplayName="Subclasses">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5919e774-7010-40ae-a158-7c8ec97fc9f7" Description="" Name="Subclass" DisplayName="Subclass" PropertyName="Baseclass" Multiplicity="ZeroOne" PropertyDisplayName="Baseclass">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="2dd2b409-d601-4e22-9b4c-231bd6a8b71c" Description="" Name="ModelRootHasTypes" DisplayName="Model Root Has Types" Namespace="AgileFx.AgileModeler" IsEmbedding="true">
      <Source>
        <DomainRole Id="db59a534-0484-4ec8-b833-72a4c9f8b43b" Description="" Name="ModelRoot" DisplayName="Model Root" PropertyName="Types" PropertyDisplayName="Types">
          <RolePlayer>
            <DomainClassMoniker Name="ModelRoot" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="0fef9cb5-b9be-4011-8d18-bee88c7aa316" Description="" Name="Type" DisplayName="Type" PropertyName="ModelRoot" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="">
          <RolePlayer>
            <DomainClassMoniker Name="ModelType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b0ae575c-cabf-4817-94de-fd17f918fdb7" Description="Description for AgileFx.AgileModeler.ClassHasFields" Name="ClassHasFields" DisplayName="Class Has Fields" Namespace="AgileFx.AgileModeler" IsEmbedding="true">
      <Source>
        <DomainRole Id="a74903da-4150-4018-a3a5-64987424639b" Description="Description for AgileFx.AgileModeler.ClassHasFields.ModelClass" Name="ModelClass" DisplayName="Model Class" PropertyName="Fields" PropertyDisplayName="Fields">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c3bb9dd2-c54e-49c4-9612-e8c0d8083ed9" Description="Description for AgileFx.AgileModeler.ClassHasFields.ModelField" Name="ModelField" DisplayName="Model Field" PropertyName="ModelClass" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="ModelField" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="42a28aa5-46f3-49cb-9b7a-e3b76048fbc9" Description="Description for AgileFx.AgileModeler.ClassHasNavigationProperties" Name="ClassHasNavigationProperties" DisplayName="Class Has Navigational Properties" Namespace="AgileFx.AgileModeler" IsEmbedding="true">
      <Source>
        <DomainRole Id="2a57841a-2046-44e3-b3de-d15b6e9c4d38" Description="Description for AgileFx.AgileModeler.ClassHasNavigationProperties.ModelClass" Name="ModelClass" DisplayName="Model Class" PropertyName="NavigationProperties" PropertyDisplayName="Navigational Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ModelClass" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="d9046450-60cb-416a-8a1f-85445d3dfca0" Description="Description for AgileFx.AgileModeler.ClassHasNavigationProperties.NavigationProperty" Name="NavigationProperty" DisplayName="Navigational Property" PropertyName="ModelClass" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Model Class">
          <RolePlayer>
            <DomainClassMoniker Name="NavigationProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="AccessModifier" Namespace="AgileFx.AgileModeler" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Public" Value="0" />
        <EnumerationLiteral Description="" Name="Private" Value="2" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.AccessModifier.Protected" Name="Protected" Value="1" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.AccessModifier.Internal" Name="Internal" Value="3" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="TypeAccessModifier" Namespace="AgileFx.AgileModeler" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="Public" Value="0" />
        <EnumerationLiteral Description="" Name="Private" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="InheritanceModifier" Namespace="AgileFx.AgileModeler" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="None" Value="0" />
        <EnumerationLiteral Description="" Name="Abstract" Value="1" />
        <EnumerationLiteral Description="" Name="Sealed" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="Multiplicity" Namespace="AgileFx.AgileModeler" Description="">
      <Literals>
        <EnumerationLiteral Description="" Name="ZeroMany" Value="0" />
        <EnumerationLiteral Description="" Name="One" Value="1" />
        <EnumerationLiteral Description="" Name="ZeroOne" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="BuiltInTypes" Namespace="AgileFx.AgileModeler" Description="Description for AgileFx.AgileModeler.BuiltInTypes">
      <Literals>
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Boolean" Name="Boolean" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Byte" Name="Byte" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.DateTime" Name="DateTime" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Decimal" Name="Decimal" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Double" Name="Double" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Guid" Name="Guid" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Int16" Name="Int16" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Int64" Name="Int64" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Int32" Name="Int32" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Single" Name="Single" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.String" Name="String" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Binary" Name="Binary" Value="" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.BuiltInTypes.Timestamp" Name="Timestamp" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ConcurrencyCheckFrequency" Namespace="AgileFx.AgileModeler" Description="Description for AgileFx.AgileModeler.ConcurrencyCheckFrequency">
      <Literals>
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.ConcurrencyCheckFrequency.Always" Name="Always" Value="0" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.ConcurrencyCheckFrequency.WhenChanged" Name="WhenChanged" Value="1" />
        <EnumerationLiteral Description="Description for AgileFx.AgileModeler.ConcurrencyCheckFrequency.Never" Name="Never" Value="2" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <CompartmentShape Id="1c78ff8e-3d0f-420e-9522-aa7fcef3bbff" Description="" Name="ClassShape" DisplayName="Class Shape" Namespace="AgileFx.AgileModeler" GeneratesDoubleDerived="true" FixedTooltipText="Class Shape" FillColor="212, 219, 212" InitialHeight="0.3" OutlineThickness="0.01" FillGradientMode="Vertical" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Name" DisplayName="Name" DefaultText="Name" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapse" DisplayName="Expand Collapse" />
      </ShapeHasDecorators>
      <Compartment TitleFillColor="229, 237, 229" Name="PropertiesCompartment" Title="Properties" />
      <Compartment TitleFillColor="229, 237, 229" Name="NavigationPropertiesCompartment" Title="NavigationProperties" />
    </CompartmentShape>
  </Shapes>
  <Connectors>
    <Connector Id="3de372fe-1134-46c3-9fab-56238c3e330d" Description="" Name="AssociationConnector" DisplayName="Association Connector" Namespace="AgileFx.AgileModeler" GeneratesDoubleDerived="true" TooltipType="Variable" FixedTooltipText="Association Connector" Color="113, 111, 110" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyDiamond" Thickness="0.01">
      <ConnectorHasDecorators Position="TargetBottom" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="TargetMultiplicity" DisplayName="Target Multiplicity" DefaultText="TargetMultiplicity" FontStyle="Bold" FontSize="7" />
      </ConnectorHasDecorators>
      <ConnectorHasDecorators Position="SourceBottom" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="SourceMultiplicity" DisplayName="Source Multiplicity" DefaultText="SourceMultiplicity" FontStyle="Bold" FontSize="7" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="190a66c0-31ac-49c3-96c4-eaaa4fcd8896" Description="" Name="InheritanceConnector" DisplayName="Inheritance Connector" Namespace="AgileFx.AgileModeler" FixedTooltipText="Inheritance Connector" Color="113, 111, 110" SourceEndStyle="HollowArrow" Thickness="0.01" />
  </Connectors>
  <XmlSerializationBehavior Name="AgileModelSerializationBehavior" Namespace="AgileFx.AgileModeler">
    <ClassData>
      <XmlClassData TypeName="NamedElement" MonikerAttributeName="" MonikerElementName="namedElementMoniker" ElementName="namedElement" MonikerTypeName="NamedElementMoniker">
        <DomainClassMoniker Name="NamedElement" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="NamedElement/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Association" MonikerAttributeName="" SerializeId="true" MonikerElementName="associationMoniker" ElementName="association" MonikerTypeName="AssociationMoniker">
        <DomainRelationshipMoniker Name="Association" />
        <ElementData>
          <XmlPropertyData XmlName="end1Multiplicity">
            <DomainPropertyMoniker Name="Association/End1Multiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1RoleName">
            <DomainPropertyMoniker Name="Association/End1RoleName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2Multiplicity">
            <DomainPropertyMoniker Name="Association/End2Multiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2RoleName">
            <DomainPropertyMoniker Name="Association/End2RoleName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1NavigationProperty">
            <DomainPropertyMoniker Name="Association/End1NavigationProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2NavigationProperty">
            <DomainPropertyMoniker Name="Association/End2NavigationProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="Association/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="manyToManyMappingTable">
            <DomainPropertyMoniker Name="Association/ManyToManyMappingTable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1ManyToManyMappingColumn">
            <DomainPropertyMoniker Name="Association/End1ManyToManyMappingColumn" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2ManyToManyMappingColumn">
            <DomainPropertyMoniker Name="Association/End2ManyToManyMappingColumn" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1MultiplicityDisplay" Representation="Ignore">
            <DomainPropertyMoniker Name="Association/End1MultiplicityDisplay" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2MultiplicityDisplay" Representation="Ignore">
            <DomainPropertyMoniker Name="Association/End2MultiplicityDisplay" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isEdited">
            <DomainPropertyMoniker Name="Association/IsEdited" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1ManyToManyNavigationProperty">
            <DomainPropertyMoniker Name="Association/End1ManyToManyNavigationProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2ManyToManyNavigationProperty">
            <DomainPropertyMoniker Name="Association/End2ManyToManyNavigationProperty" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end1ManyToManyFieldName">
            <DomainPropertyMoniker Name="Association/End1ManyToManyFieldName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="end2ManyToManyFieldName">
            <DomainPropertyMoniker Name="Association/End2ManyToManyFieldName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="Inheritance" MonikerAttributeName="" MonikerElementName="inheritanceMoniker" ElementName="inheritance" MonikerTypeName="InheritanceMoniker">
        <DomainRelationshipMoniker Name="Inheritance" />
        <ElementData>
          <XmlPropertyData XmlName="discriminator">
            <DomainPropertyMoniker Name="Inheritance/Discriminator" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="derivedClassPrimaryKeyColumn">
            <DomainPropertyMoniker Name="Inheritance/DerivedClassPrimaryKeyColumn" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="baseClassPrimaryKeyColumn">
            <DomainPropertyMoniker Name="Inheritance/BaseClassPrimaryKeyColumn" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelRootHasTypes" MonikerAttributeName="" MonikerElementName="modelRootHasTypesMoniker" ElementName="modelRootHasTypes" MonikerTypeName="ModelRootHasTypesMoniker">
        <DomainRelationshipMoniker Name="ModelRootHasTypes" />
      </XmlClassData>
      <XmlClassData TypeName="ModelRoot" MonikerAttributeName="" MonikerElementName="modelRootMoniker" ElementName="modelRoot" MonikerTypeName="ModelRootMoniker">
        <DomainClassMoniker Name="ModelRoot" />
        <ElementData>
          <XmlRelationshipData RoleElementName="types">
            <DomainRelationshipMoniker Name="ModelRootHasTypes" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="namespace">
            <DomainPropertyMoniker Name="ModelRoot/Namespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="connectionString">
            <DomainPropertyMoniker Name="ModelRoot/ConnectionString" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="dataContextName">
            <DomainPropertyMoniker Name="ModelRoot/DataContextName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelClass" MonikerAttributeName="" MonikerElementName="modelClassMoniker" ElementName="modelClass" MonikerTypeName="ModelClassMoniker">
        <DomainClassMoniker Name="ModelClass" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subclasses">
            <DomainRelationshipMoniker Name="Inheritance" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targets">
            <DomainRelationshipMoniker Name="Association" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="tableName">
            <DomainPropertyMoniker Name="ModelClass/TableName" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="fields">
            <DomainRelationshipMoniker Name="ClassHasFields" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="navigationProperties">
            <DomainRelationshipMoniker Name="ClassHasNavigationProperties" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="derivesOrImplements">
            <DomainPropertyMoniker Name="ModelClass/DerivesOrImplements" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelFieldBase" MonikerAttributeName="" MonikerElementName="modelFieldBaseMoniker" ElementName="modelFieldBase" MonikerTypeName="ModelFieldBaseMoniker">
        <DomainClassMoniker Name="ModelFieldBase" />
        <ElementData>
          <XmlPropertyData XmlName="getter">
            <DomainPropertyMoniker Name="ModelFieldBase/Getter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="setter">
            <DomainPropertyMoniker Name="ModelFieldBase/Setter" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isEdited">
            <DomainPropertyMoniker Name="ModelFieldBase/IsEdited" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelType" MonikerAttributeName="" MonikerElementName="modelTypeMoniker" ElementName="modelType" MonikerTypeName="ModelTypeMoniker">
        <DomainClassMoniker Name="ModelType" />
      </XmlClassData>
      <XmlClassData TypeName="ClassModelElement" MonikerAttributeName="" MonikerElementName="classModelElementMoniker" ElementName="classModelElement" MonikerTypeName="ClassModelElementMoniker">
        <DomainClassMoniker Name="ClassModelElement" />
        <ElementData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="ClassModelElement/Description" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassShape" MonikerAttributeName="" MonikerElementName="classShapeMoniker" ElementName="classShape" MonikerTypeName="ClassShapeMoniker">
        <CompartmentShapeMoniker Name="ClassShape" />
      </XmlClassData>
      <XmlClassData TypeName="AssociationConnector" MonikerAttributeName="" MonikerElementName="associationConnectorMoniker" ElementName="associationConnector" MonikerTypeName="AssociationConnectorMoniker">
        <ConnectorMoniker Name="AssociationConnector" />
      </XmlClassData>
      <XmlClassData TypeName="InheritanceConnector" MonikerAttributeName="" MonikerElementName="inheritanceConnectorMoniker" ElementName="inheritanceConnector" MonikerTypeName="InheritanceConnectorMoniker">
        <ConnectorMoniker Name="InheritanceConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ClassDiagram" MonikerAttributeName="" MonikerElementName="classDiagramMoniker" ElementName="classDiagram" MonikerTypeName="ClassDiagramMoniker">
        <DiagramMoniker Name="ClassDiagram" />
        <ElementData>
          <XmlPropertyData XmlName="initialized">
            <DomainPropertyMoniker Name="ClassDiagram/Initialized" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="NavigationProperty" MonikerAttributeName="" MonikerElementName="navigationPropertyMoniker" ElementName="navigationProperty" MonikerTypeName="NavigationPropertyMoniker">
        <DomainClassMoniker Name="NavigationProperty" />
        <ElementData>
          <XmlPropertyData XmlName="multiplicity">
            <DomainPropertyMoniker Name="NavigationProperty/Multiplicity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="association">
            <DomainPropertyMoniker Name="NavigationProperty/Association" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="NavigationProperty/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isForeignkey">
            <DomainPropertyMoniker Name="NavigationProperty/IsForeignkey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="foreignkeyColumn">
            <DomainPropertyMoniker Name="NavigationProperty/ForeignkeyColumn" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelField" MonikerAttributeName="" MonikerElementName="modelFieldMoniker" ElementName="modelField" MonikerTypeName="ModelFieldMoniker">
        <DomainClassMoniker Name="ModelField" />
        <ElementData>
          <XmlPropertyData XmlName="isPrimaryKey">
            <DomainPropertyMoniker Name="ModelField/IsPrimaryKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseColumnName">
            <DomainPropertyMoniker Name="ModelField/DatabaseColumnName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="ModelField/DefaultValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="nullable">
            <DomainPropertyMoniker Name="ModelField/Nullable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="ModelField/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDbGenerated">
            <DomainPropertyMoniker Name="ModelField/IsDbGenerated" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isFixedLength">
            <DomainPropertyMoniker Name="ModelField/IsFixedLength" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUnicode">
            <DomainPropertyMoniker Name="ModelField/IsUnicode" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="maxLength">
            <DomainPropertyMoniker Name="ModelField/MaxLength" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isIdentity">
            <DomainPropertyMoniker Name="ModelField/IsIdentity" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isUnique">
            <DomainPropertyMoniker Name="ModelField/IsUnique" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="updateCheck">
            <DomainPropertyMoniker Name="ModelField/UpdateCheck" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ClassHasFields" MonikerAttributeName="" MonikerElementName="classHasFieldsMoniker" ElementName="classHasFields" MonikerTypeName="ClassHasFieldsMoniker">
        <DomainRelationshipMoniker Name="ClassHasFields" />
      </XmlClassData>
      <XmlClassData TypeName="ClassHasNavigationProperties" MonikerAttributeName="" MonikerElementName="classHasNavigationPropertiesMoniker" ElementName="classHasNavigationProperties" MonikerTypeName="ClassHasNavigationPropertiesMoniker">
        <DomainRelationshipMoniker Name="ClassHasNavigationProperties" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="AgileModelExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="InheritanceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="Inheritance" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="AssociationBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="Association" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ModelClass" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="e2e9aa89-1e94-4c06-b265-dbd4435d90ff" Description="" Name="ClassDiagram" DisplayName="Class Diagram" Namespace="AgileFx.AgileModeler">
    <Properties>
      <DomainProperty Id="7d6c7705-0206-4fee-96d6-1eb7caa1d54c" Description="Description for AgileFx.AgileModeler.ClassDiagram.Initialized" Name="Initialized" DisplayName="Initialized" IsBrowsable="false">
        <Type>
          <ExternalTypeMoniker Name="/System/Boolean" />
        </Type>
      </DomainProperty>
    </Properties>
    <Class>
      <DomainClassMoniker Name="ModelRoot" />
    </Class>
    <ShapeMaps>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ModelClass" />
        <ParentElementPath>
          <DomainPath>ModelRootHasTypes.ModelRoot/!ModelRoot</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ClassShape/Name" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="ClassShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="ClassShape/PropertiesCompartment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasFields.Fields/!ModelField</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ClassShape/NavigationPropertiesCompartment" />
          <ElementsDisplayed>
            <DomainPath>ClassHasNavigationProperties.NavigationProperties/!NavigationProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="NamedElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="InheritanceConnector" />
        <DomainRelationshipMoniker Name="Inheritance" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="AssociationConnector" />
        <DomainRelationshipMoniker Name="Association" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/SourceMultiplicity" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/End1MultiplicityDisplay" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="AssociationConnector/TargetMultiplicity" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Association/End2MultiplicityDisplay" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer FileExtension="models" EditorGuid="c6695f5c-9f44-4b0d-af6f-04138f6a1546">
    <RootClass>
      <DomainClassMoniker Name="ModelRoot" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="AgileModelSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Class Diagrams">
      <ElementTool Name="ModelClass" ToolboxIcon="Resources\ClassTool.bmp" Caption="Class" Tooltip="Create a Class" HelpKeyword="ModelClassF1Keyword">
        <DomainClassMoniker Name="ModelClass" />
      </ElementTool>
      <ConnectionTool Name="Association" ToolboxIcon="Resources\AssociationTool.bmp" Caption="Association" Tooltip="Create an Association" HelpKeyword="AssociationF1Keyword">
        <ConnectionBuilderMoniker Name="AgileModeler/AssociationBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="Inheritance" ToolboxIcon="resources\generalizationtool.bmp" Caption="Inheritance" Tooltip="Create an Inheritance link" HelpKeyword="InheritanceF1Keyword" ReversesDirection="true">
        <ConnectionBuilderMoniker Name="AgileModeler/InheritanceBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="true" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="ClassDiagram" />
  </Designer>
  <Explorer ExplorerGuid="fd722530-1580-4d4a-8805-2ea0b1e90456" Title="">
    <ExplorerBehaviorMoniker Name="AgileModeler/AgileModelExplorer" />
  </Explorer>
</Dsl>