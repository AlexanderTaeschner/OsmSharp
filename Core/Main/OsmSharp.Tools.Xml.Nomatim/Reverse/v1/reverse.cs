﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace OsmSharp.Tools.Xml.Nomatim.Reverse.v1
{

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 


	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class reversegeocode
	{

		private reversegeocodeResult[] resultField;

		private reversegeocodeAddressparts[] addresspartsField;

		private string timestampField;

		private string attributionField;

		private string querystringField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("result", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
		public reversegeocodeResult[] result
		{
			get { return this.resultField; }
			set { this.resultField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("addressparts", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public reversegeocodeAddressparts[] addressparts
		{
			get { return this.addresspartsField; }
			set { this.addresspartsField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string timestamp
		{
			get { return this.timestampField; }
			set { this.timestampField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string attribution
		{
			get { return this.attributionField; }
			set { this.attributionField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string querystring
		{
			get { return this.querystringField; }
			set { this.querystringField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class reversegeocodeResult
	{

		private string place_idField;

		private string osm_typeField;

		private string osm_idField;

		private string latField;

		private string lonField;

		private string valueField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string place_id
		{
			get { return this.place_idField; }
			set { this.place_idField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string osm_type
		{
			get { return this.osm_typeField; }
			set { this.osm_typeField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string osm_id
		{
			get { return this.osm_idField; }
			set { this.osm_idField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string lat
		{
			get { return this.latField; }
			set { this.latField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string lon
		{
			get { return this.lonField; }
			set { this.lonField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class reversegeocodeAddressparts
	{

		private string house_numberField;

		private string roadField;

		private string suburbField;

		private string cityField;

		private string countyField;

		private string state_districtField;

		private string stateField;

		private string postcodeField;

		private string countryField;

		private string country_codeField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string house_number
		{
			get { return this.house_numberField; }
			set { this.house_numberField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string road
		{
			get { return this.roadField; }
			set { this.roadField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string suburb
		{
			get { return this.suburbField; }
			set { this.suburbField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string city
		{
			get { return this.cityField; }
			set { this.cityField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string county
		{
			get { return this.countyField; }
			set { this.countyField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string state_district
		{
			get { return this.state_districtField; }
			set { this.state_districtField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string state
		{
			get { return this.stateField; }
			set { this.stateField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string postcode
		{
			get { return this.postcodeField; }
			set { this.postcodeField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string country
		{
			get { return this.countryField; }
			set { this.countryField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string country_code
		{
			get { return this.country_codeField; }
			set { this.country_codeField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class NewDataSet
	{

		private reversegeocode[] itemsField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("reversegeocode")]
		public reversegeocode[] Items
		{
			get { return this.itemsField; }
			set { this.itemsField = value; }
		}
	}
}